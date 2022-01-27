package delta.codecharacter.server.auth.provider

import delta.codecharacter.server.user.UserRepository
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.security.authentication.AuthenticationProvider
import org.springframework.security.authentication.BadCredentialsException
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken
import org.springframework.security.core.Authentication
import org.springframework.stereotype.Component

@Component
class MongoAuthenticationProvider : AuthenticationProvider {

    @Autowired
    private lateinit var userRepository: UserRepository

    override fun authenticate(authentication: Authentication?): Authentication {
        if (authentication == null) {
            throw BadCredentialsException("")
        }
        val email = authentication.name
        val password = authentication.credentials.toString()

        val user = userRepository.findFirstByEmail(email)
        if (user.password != password) {
            throw BadCredentialsException("Invalid credentials")
        }

        return UsernamePasswordAuthenticationToken(
            email,
            password,
            user.authorities
        )
    }

    override fun supports(authentication: Class<*>?): Boolean {
        if (authentication == null) {
            return false
        }
        return authentication == UsernamePasswordAuthenticationToken::class
    }
}