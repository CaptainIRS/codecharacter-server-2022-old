package delta.codecharacter.server.user.public_user

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document(collection = "publicUser")
data class PublicUserEntity(
    @Id
    val username: String,
    val name: String,
    val country: String,
    val college: String,
    val avatarId: Int
)
