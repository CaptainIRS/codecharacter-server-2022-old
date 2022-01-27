package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Code model
 * @param code
 * @param lastSavedAt
 */
data class CodeDto(

    @ApiModelProperty(example = "#include <iostream>", required = true, value = "")
    @field:JsonProperty("code", required = true) val code: kotlin.String,

    @ApiModelProperty(example = "2021-01-01T00:00Z", required = true, value = "")
    @field:JsonProperty("lastSavedAt", required = true) val lastSavedAt: java.time.OffsetDateTime
)
