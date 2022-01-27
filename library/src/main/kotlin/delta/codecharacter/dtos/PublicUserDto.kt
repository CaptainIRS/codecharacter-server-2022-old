package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Public user model
 * @param userId
 * @param username
 * @param name
 * @param country
 * @param college
 * @param avatarId
 */
data class PublicUserDto(

    @ApiModelProperty(example = "1", required = true, value = "")
    @field:JsonProperty("userId", required = true) val userId: kotlin.Int,

    @ApiModelProperty(example = "test", required = true, value = "")
    @field:JsonProperty("username", required = true) val username: kotlin.String,

    @ApiModelProperty(example = "Test User", required = true, value = "")
    @field:JsonProperty("name", required = true) val name: kotlin.String,

    @ApiModelProperty(example = "IN", required = true, value = "")
    @field:JsonProperty("country", required = true) val country: kotlin.String,

    @ApiModelProperty(example = "Test", required = true, value = "")
    @field:JsonProperty("college", required = true) val college: kotlin.String,

    @ApiModelProperty(example = "1", required = true, value = "")
    @field:JsonProperty("avatarId", required = true) val avatarId: kotlin.Int
)
