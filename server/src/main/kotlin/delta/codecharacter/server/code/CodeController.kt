package delta.codecharacter.server.code

import delta.codecharacter.core.CodeApi
import delta.codecharacter.dtos.CodeDto
import delta.codecharacter.dtos.CodeRevisionDto
import delta.codecharacter.dtos.CreateCodeRevisionRequestDto
import delta.codecharacter.dtos.UpdateLatestCodeRequestDto
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.RestController
import java.util.*

@RestController
class CodeController : CodeApi {
    override fun createCodeRevision(createCodeRevisionRequestDto: CreateCodeRevisionRequestDto): ResponseEntity<Unit> {
        return super.createCodeRevision(createCodeRevisionRequestDto)
    }

    override fun getCodeRevisionById(revisionId: UUID): ResponseEntity<CodeRevisionDto> {
        return super.getCodeRevisionById(revisionId)
    }

    override fun getCodeRevisions(): ResponseEntity<List<CodeRevisionDto>> {
        return super.getCodeRevisions()
    }

    override fun getLatestCode(): ResponseEntity<CodeDto> {
        return super.getLatestCode()
    }

    override fun updateLatestCode(updateLatestCodeRequestDto: UpdateLatestCodeRequestDto): ResponseEntity<Unit> {
        return super.updateLatestCode(updateLatestCodeRequestDto)
    }
}
