package delta.codecharacter.server.user.rating_history

import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service

@Service
class RatingHistoryService {

    @Autowired
    private lateinit var ratingHistoryRepository: RatingHistoryRepository

    fun insertRatingHistory(ratingHistoryEntity: RatingHistoryEntity) =
        ratingHistoryRepository.insert(ratingHistoryEntity)
}