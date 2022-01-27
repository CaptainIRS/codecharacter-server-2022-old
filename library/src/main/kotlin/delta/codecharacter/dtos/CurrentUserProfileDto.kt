package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty
import javax.validation.constraints.Pattern

/**
 * Current user profile model
 * @param id
 * @param username
 * @param name
 * @param email
 * @param country
 * @param college
 * @param currentLevel
 * @param isAdmin
 */
data class CurrentUserProfileDto(

    @ApiModelProperty(example = "1", required = true, value = "")
    @field:JsonProperty("id", required = true) val id: kotlin.Int,

    @ApiModelProperty(example = "test", required = true, value = "")
    @field:JsonProperty("username", required = true) val username: kotlin.String,

    @ApiModelProperty(example = "Test", required = true, value = "")
    @field:JsonProperty("name", required = true) val name: kotlin.String,
    @get:Pattern(regexp = "[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,6}")
    @ApiModelProperty(example = "test@test.com", required = true, value = "")
    @field:JsonProperty("email", required = true) val email: kotlin.String,

    @ApiModelProperty(example = "IN", required = true, value = "")
    @field:JsonProperty("country", required = true) val country: kotlin.String,

    @ApiModelProperty(example = "Test", required = true, value = "")
    @field:JsonProperty("college", required = true) val college: kotlin.String,

    @ApiModelProperty(example = "1", required = true, value = "")
    @field:JsonProperty("currentLevel", required = true) val currentLevel: kotlin.Int,

    @ApiModelProperty(example = "null", required = true, value = "")
    @field:JsonProperty("isAdmin", required = true) val isAdmin: kotlin.Boolean = false
)
