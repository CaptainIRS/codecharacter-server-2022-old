package delta.codecharacter.server.user

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.security.core.userdetails.UserDetails
import org.springframework.security.core.userdetails.UserDetailsService
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder
import org.springframework.stereotype.Service

@Service
class UserService : UserDetailsService {

    @Autowired
    private lateinit var userRepository: UserRepository

    @Autowired
    private lateinit var passwordEncoder: BCryptPasswordEncoder

    fun insertUser(user: UserEntity) {
        val secureUser = user.copy(
            password = passwordEncoder.encode(user.password)
        )
        userRepository.insert(secureUser)
    }

    override fun loadUserByUsername(email: String?): UserDetails {
        return userRepository.findFirstByEmail(email)
    }
}