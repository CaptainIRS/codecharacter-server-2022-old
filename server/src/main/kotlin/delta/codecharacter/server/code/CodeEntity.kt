package delta.codecharacter.server.code

import delta.codecharacter.server.user.UserEntity
import java.time.OffsetDateTime

data class CodeEntity(
    val code: String,
    val language: String,
    val lastSavedAt: OffsetDateTime,
    val user: UserEntity,
)
