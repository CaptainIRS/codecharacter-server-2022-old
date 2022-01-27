package delta.codecharacter.server.user.rating_history

import org.springframework.data.mongodb.repository.MongoRepository

interface RatingHistoryRepository : MongoRepository<RatingHistoryEntity, String>