package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Update latest code request
 * @param code
 * @param lock
 */
data class UpdateLatestCodeRequestDto(

    @ApiModelProperty(example = "#include <iostream>", required = true, value = "")
    @field:JsonProperty("code", required = true) val code: kotlin.String,

    @ApiModelProperty(example = "null", value = "")
    @field:JsonProperty("lock") val lock: kotlin.Boolean? = false
)
