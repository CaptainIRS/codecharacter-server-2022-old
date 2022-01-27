package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty

/**
 * Game model
 * @param id
 * @param points1
 * @param points2
 * @param status
 * @param gameVerdict
 * @param map
 */
data class GameDto(

    @ApiModelProperty(example = "123e4567-e89b-12d3-a456-426614174000", required = true, value = "")
    @field:JsonProperty("id", required = true) val id: java.util.UUID,

    @ApiModelProperty(example = "100", required = true, value = "")
    @field:JsonProperty("points1", required = true) val points1: kotlin.Int,

    @ApiModelProperty(example = "90", required = true, value = "")
    @field:JsonProperty("points2", required = true) val points2: kotlin.Int,

    @ApiModelProperty(example = "IDLE", required = true, value = "")
    @field:JsonProperty("status", required = true) val status: GameDto.Status,

    @ApiModelProperty(example = "PLAYER1", required = true, value = "")
    @field:JsonProperty("gameVerdict", required = true) val gameVerdict: GameDto.GameVerdict,

    @ApiModelProperty(example = "0000\n0010\n0100\n1000\n", value = "")
    @field:JsonProperty("map") val map: kotlin.String? = null
) {

    /**
     *
     * Values: IDLE,EXECUTING,EXECUTED,EXECUTE_ERROR
     */
    enum class Status(val value: kotlin.String) {

        @JsonProperty("IDLE") IDLE("IDLE"),

        @JsonProperty("EXECUTING") EXECUTING("EXECUTING"),

        @JsonProperty("EXECUTED") EXECUTED("EXECUTED"),

        @JsonProperty("EXECUTE_ERROR") EXECUTE_ERROR("EXECUTE_ERROR");
    }

    /**
     *
     * Values: PLAYER1,PLAYER2,TIE
     */
    enum class GameVerdict(val value: kotlin.String) {

        @JsonProperty("PLAYER1") PLAYER1("PLAYER1"),

        @JsonProperty("PLAYER2") PLAYER2("PLAYER2"),

        @JsonProperty("TIE") TIE("TIE");
    }
}
