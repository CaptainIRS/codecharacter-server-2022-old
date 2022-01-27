package delta.codecharacter.server.code

import delta.codecharacter.server.user.UserEntity
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import java.time.OffsetDateTime

@Service
class CodeService {

    @Autowired
    private lateinit var codeRepository: CodeRepository
}
