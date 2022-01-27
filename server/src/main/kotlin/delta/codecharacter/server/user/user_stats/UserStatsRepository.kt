package delta.codecharacter.server.user.user_stats

import org.springframework.data.mongodb.repository.MongoRepository

interface UserStatsRepository : MongoRepository<UserStatsEntity, String>