package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Notification model
 * @param id
 * @param title
 * @param content
 * @param createdAt
 * @param read
 */
data class NotificationDto(

    @ApiModelProperty(example = "123e4567-e89b-12d3-a456-426614174000", required = true, value = "")
    @field:JsonProperty("id", required = true) val id: java.util.UUID,

    @ApiModelProperty(example = "Test notification", required = true, value = "")
    @field:JsonProperty("title", required = true) val title: kotlin.String,

    @ApiModelProperty(example = "Test", required = true, value = "")
    @field:JsonProperty("content", required = true) val content: kotlin.String,

    @ApiModelProperty(example = "2021-01-01T00:00Z", required = true, value = "")
    @field:JsonProperty("createdAt", required = true) val createdAt: java.time.OffsetDateTime,

    @ApiModelProperty(example = "null", required = true, value = "")
    @field:JsonProperty("read", required = true) val read: kotlin.Boolean = false
)
