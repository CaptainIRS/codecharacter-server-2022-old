package delta.codecharacter.server.user

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.index.Indexed
import org.springframework.data.mongodb.core.mapping.Document
import org.springframework.security.core.GrantedAuthority
import org.springframework.security.core.authority.SimpleGrantedAuthority
import org.springframework.security.core.userdetails.UserDetails
import java.util.stream.Collectors


@Document(collection = "user")
data class UserEntity(
    @Id
    val email: String,
    @Indexed(unique = true)
    private val username: String,
    private val password: String,
    private val isEnabled: Boolean = false,
    private val isCredentialsNonExpired: Boolean,
    private val isAccountNonExpired: Boolean,
    private val isAccountNonLocked: Boolean,
    private val authorities: List<UserRole>
) : UserDetails {
    override fun getAuthorities(): Set<GrantedAuthority> {
        return authorities.stream().map { role ->
            SimpleGrantedAuthority(role.name)
        }.collect(Collectors.toSet())
    }

    override fun getPassword(): String = password
    override fun getUsername(): String = username
    override fun isAccountNonExpired(): Boolean = isAccountNonExpired
    override fun isAccountNonLocked(): Boolean = isAccountNonLocked
    override fun isCredentialsNonExpired(): Boolean = isCredentialsNonExpired
    override fun isEnabled(): Boolean = isEnabled
}
