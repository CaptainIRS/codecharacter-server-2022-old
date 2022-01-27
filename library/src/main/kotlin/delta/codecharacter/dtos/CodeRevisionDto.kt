package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Code revision model
 * @param id
 * @param code
 * @param parentRevision
 */
data class CodeRevisionDto(

    @ApiModelProperty(example = "123e4567-e89b-12d3-a456-426614174000", required = true, value = "")
    @field:JsonProperty("id", required = true) val id: java.util.UUID,

    @ApiModelProperty(example = "#include <iostream>", required = true, value = "")
    @field:JsonProperty("code", required = true) val code: kotlin.String,

    @ApiModelProperty(example = "null", required = true, value = "")
    @field:JsonProperty("parentRevision", required = true) val parentRevision: java.util.UUID
)
