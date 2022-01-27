package delta.codecharacter.server.code

import delta.codecharacter.server.user.UserEntity
import org.springframework.data.mongodb.repository.MongoRepository
import org.springframework.stereotype.Repository
import java.util.*

@Repository
interface CodeRepository : MongoRepository<CodeEntity, UUID> {
    fun getCodeEntityByUser(user: UserEntity)
}
