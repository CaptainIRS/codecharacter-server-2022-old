package delta.codecharacter.server.user.public_user

import org.springframework.data.mongodb.repository.MongoRepository

interface PublicUserRepository : MongoRepository<PublicUserEntity, String>