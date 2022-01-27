package delta.codecharacter.dtos

import com.fasterxml.jackson.annotation.JsonProperty
import io.swagger.annotations.ApiModelProperty
import javax.validation.Valid
import javax.validation.constraints.Size

/**
 * Match model
 * @param id
 * @param games
 * @param matchMode
 * @param matchVerdict
 * @param createdAt
 * @param user1
 * @param user2
 */
data class MatchDto(

    @ApiModelProperty(example = "123e4567-e89b-12d3-a456-426614174000", required = true, value = "")
    @field:JsonProperty("id", required = true) val id: java.util.UUID,

    @field:Valid
    @ApiModelProperty(example = "null", required = true, value = "")
    @field:JsonProperty("games", required = true) val games: kotlin.collections.Set<GameDto>,

    @ApiModelProperty(example = "SELF", required = true, value = "")
    @field:JsonProperty("matchMode", required = true) val matchMode: MatchDto.MatchMode,

    @get:Size(min = 1)
    @ApiModelProperty(example = "TIE", required = true, value = "")
    @field:JsonProperty("matchVerdict", required = true) val matchVerdict: MatchDto.MatchVerdict,

    @ApiModelProperty(example = "2021-01-01T00:00Z", required = true, value = "")
    @field:JsonProperty("createdAt", required = true) val createdAt: java.time.OffsetDateTime,

    @field:Valid
    @ApiModelProperty(example = "null", required = true, value = "")
    @field:JsonProperty("user1", required = true) val user1: PublicUserDto,

    @field:Valid
    @ApiModelProperty(example = "null", required = true, value = "")
    @field:JsonProperty("user2", required = true) val user2: PublicUserDto
) {

    /**
     *
     * Values: SELF,AI,PREV_COMMIT,MANUAL,AUTO
     */
    enum class MatchMode(val value: kotlin.String) {

        @JsonProperty("SELF") SELF("SELF"),

        @JsonProperty("AI") AI("AI"),

        @JsonProperty("PREV_COMMIT") PREV_COMMIT("PREV_COMMIT"),

        @JsonProperty("MANUAL") MANUAL("MANUAL"),

        @JsonProperty("AUTO") AUTO("AUTO");
    }

    /**
     *
     * Values: PLAYER1,PLAYER2,TIE
     */
    enum class MatchVerdict(val value: kotlin.String) {

        @JsonProperty("PLAYER1") PLAYER1("PLAYER1"),

        @JsonProperty("PLAYER2") PLAYER2("PLAYER2"),

        @JsonProperty("TIE") TIE("TIE");
    }
}
