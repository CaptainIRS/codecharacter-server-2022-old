package delta.codecharacter.server.user.user_stats

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service

@Service
class UserStatsService {

    @Autowired
    private lateinit var userStatsRepository: UserStatsRepository

    fun insertUserStats(userStatsEntity: UserStatsEntity) =
        userStatsRepository.insert(userStatsEntity)
}
