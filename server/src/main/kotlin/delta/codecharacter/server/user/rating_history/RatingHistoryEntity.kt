package delta.codecharacter.server.user.rating_history

import org.springframework.data.annotation.Id
import org.springframework.data.mongodb.core.mapping.Document
import java.time.Instant

@Document(collection = "ratingHistory")
data class RatingHistoryEntity(
    @Id
    val username: String,
    val rating: Float,
    val ratingDeviation: Float,
    val validFrom: Instant
)
