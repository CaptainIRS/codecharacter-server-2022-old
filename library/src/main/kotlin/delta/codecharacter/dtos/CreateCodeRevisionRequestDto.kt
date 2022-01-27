package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Create code revision request
 * @param code
 */
data class CreateCodeRevisionRequestDto(

    @ApiModelProperty(example = "#include <iostream>", required = true, value = "")
    @field:JsonProperty("code", required = true) val code: kotlin.String
)
