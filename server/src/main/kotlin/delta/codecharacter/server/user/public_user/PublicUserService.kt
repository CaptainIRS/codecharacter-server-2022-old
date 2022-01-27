package delta.codecharacter.server.user.public_user

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service

@Service
class PublicUserService {
    @Autowired
    private lateinit var publicUserRepository: PublicUserRepository

    fun insertPublicUser(publicUserEntity: PublicUserEntity) =
        publicUserRepository.insert(publicUserEntity)
}
