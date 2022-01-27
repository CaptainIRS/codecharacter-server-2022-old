package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Update current user profile request
 * @param name
 * @param country
 * @param college
 * @param avatarId
 */
data class UpdateCurrentUserProfileDto(

    @ApiModelProperty(example = "Test", value = "")
    @field:JsonProperty("name") val name: kotlin.String? = null,

    @ApiModelProperty(example = "IN", value = "")
    @field:JsonProperty("country") val country: kotlin.String? = null,

    @ApiModelProperty(example = "Test", value = "")
    @field:JsonProperty("college") val college: kotlin.String? = null,

    @ApiModelProperty(example = "1", value = "")
    @field:JsonProperty("avatarId") val avatarId: kotlin.Int? = null
)
