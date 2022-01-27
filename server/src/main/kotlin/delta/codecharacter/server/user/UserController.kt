package delta.codecharacter.server.user

import delta.codecharacter.core.UserApi
import delta.codecharacter.dtos.RegisterUserRequestDto
import delta.codecharacter.server.exception.CustomException
import delta.codecharacter.server.user.public_user.PublicUserEntity
import delta.codecharacter.server.user.public_user.PublicUserService
import delta.codecharacter.server.user.rating_history.RatingHistoryEntity
import delta.codecharacter.server.user.rating_history.RatingHistoryService
import delta.codecharacter.server.user.user_stats.UserStatsEntity
import delta.codecharacter.server.user.user_stats.UserStatsService
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.dao.DuplicateKeyException
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.validation.annotation.Validated
import org.springframework.web.bind.annotation.RestController
import java.time.Instant

@RestController
class UserController : UserApi {

    @Autowired
    private lateinit var userService: UserService

    @Autowired
    private lateinit var publicUserService: PublicUserService

    @Autowired
    private lateinit var ratingHistoryService: RatingHistoryService

    @Autowired
    private lateinit var userStatsService: UserStatsService


    override fun register(@Validated registerUserRequestDto: RegisterUserRequestDto): ResponseEntity<Unit> {
        if (registerUserRequestDto.password != registerUserRequestDto.passwordConfirmation) {
            throw CustomException(
                HttpStatus.BAD_REQUEST,
                "Password and password confirmation don't match"
            )
        }
        val user = UserEntity(
            username = registerUserRequestDto.username,
            password = registerUserRequestDto.password,
            email = registerUserRequestDto.email,
            isEnabled = false,
            isAccountNonExpired = true,
            isAccountNonLocked = true,
            isCredentialsNonExpired = true,
            authorities = listOf(UserRole.ROLE_USER)
        )
        val publicUser = PublicUserEntity(
            username = registerUserRequestDto.username,
            name = registerUserRequestDto.name,
            country = registerUserRequestDto.country,
            college = registerUserRequestDto.college,
            avatarId = registerUserRequestDto.avatarId
        )
        val ratingHistory = RatingHistoryEntity(
            username = registerUserRequestDto.username,
            rating = 0.0F,
            ratingDeviation = 0.0F,
            validFrom = Instant.now()
        )
        val userStats = UserStatsEntity(
            username = registerUserRequestDto.username,
            rating = 0.0F,
            wins = 0,
            losses = 0,
            ties = 0
        )

        try {
            userService.insertUser(user)
            publicUserService.insertPublicUser(publicUser)
            ratingHistoryService.insertRatingHistory(ratingHistory)
            userStatsService.insertUserStats(userStats)
        } catch (duplicateError: DuplicateKeyException) {
            throw CustomException(
                HttpStatus.BAD_REQUEST,
                "Username already exists"
            )
        }
        return ResponseEntity
            .status(HttpStatus.CREATED)
            .body(Unit)
    }
}
