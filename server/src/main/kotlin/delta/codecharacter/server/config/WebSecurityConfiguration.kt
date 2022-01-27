package delta.codecharacter.server.config

import delta.codecharacter.server.auth.provider.MongoAuthenticationProvider
import delta.codecharacter.server.user.UserService
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.security.config.annotation.authentication.builders.AuthenticationManagerBuilder
import org.springframework.security.config.annotation.web.builders.HttpSecurity
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter
import org.springframework.security.config.web.servlet.invoke
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder


@Configuration
class WebSecurityConfiguration : WebSecurityConfigurerAdapter() {

    @Autowired
    private lateinit var mongoAuthenticationProvider: MongoAuthenticationProvider

    override fun configure(http: HttpSecurity?) {
        http {
            authorizeRequests {
                authorize("/**", permitAll)
            }
            csrf {
                disable()
            }
        }
    }

    override fun configure(auth: AuthenticationManagerBuilder?) {
        auth?.authenticationProvider(mongoAuthenticationProvider)
    }

    @Bean
    fun passwordEncoder() = BCryptPasswordEncoder()

    @Bean
    fun userDetailService() = UserService()
}