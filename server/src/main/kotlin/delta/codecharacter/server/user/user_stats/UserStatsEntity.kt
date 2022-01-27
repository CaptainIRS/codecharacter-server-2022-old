package delta.codecharacter.server.user.user_stats

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document

@Document(collection = "userStats")
data class UserStatsEntity(
    @Id
    val username: String,
    val rating: Float,
    val wins: Int,
    val losses: Int,
    val ties: Int
)
