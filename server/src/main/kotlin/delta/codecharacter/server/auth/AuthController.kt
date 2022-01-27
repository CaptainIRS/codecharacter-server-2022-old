package delta.codecharacter.server.auth

import delta.codecharacter.core.AuthApi
import delta.codecharacter.dtos.ForgotPasswordRequestDto
import delta.codecharacter.dtos.PasswordLoginRequestDto
import delta.codecharacter.dtos.PasswordLoginResponseDto
import delta.codecharacter.dtos.ResetPasswordRequestDto
import org.springframework.http.ResponseEntity

class AuthController : AuthApi {
    override fun forgotPassword(forgotPasswordRequestDto: ForgotPasswordRequestDto): ResponseEntity<Unit> {
        println()
        return super.forgotPassword(forgotPasswordRequestDto)
    }

    override fun passwordLogin(passwordLoginRequestDto: PasswordLoginRequestDto): ResponseEntity<PasswordLoginResponseDto> {
        println()
        return super.passwordLogin(passwordLoginRequestDto)
    }

    override fun resetPassword(resetPasswordRequestDto: ResetPasswordRequestDto): ResponseEntity<Unit> {
        println()
        return super.resetPassword(resetPasswordRequestDto)
    }
}
