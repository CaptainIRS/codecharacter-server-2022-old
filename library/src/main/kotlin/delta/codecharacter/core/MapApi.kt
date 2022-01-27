/**
 * NOTE: This class is auto generated by OpenAPI Generator (https://openapi-generator.tech) (5.3.1).
 * https://openapi-generator.tech
 * Do not edit the class manually.
*/
package delta.codecharacter.core

import delta.codecharacter.dtos.CreateMapRevisionRequestDto
import delta.codecharacter.dtos.GameMapDto
import delta.codecharacter.dtos.GameMapRevisionDto
import delta.codecharacter.dtos.GenericErrorDto
import delta.codecharacter.dtos.UpdateLatestMapRequestDto
import io.swagger.annotations.Api
import io.swagger.annotations.ApiOperation
import io.swagger.annotations.ApiParam
import io.swagger.annotations.ApiResponse
import io.swagger.annotations.ApiResponses
import io.swagger.annotations.Authorization
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.validation.annotation.Validated
import org.springframework.web.bind.annotation.*
import javax.validation.Valid
import kotlin.collections.List

@Validated
@Api(value = "Map", description = "The Map API")
@RequestMapping("\${api.base-path:}")
interface MapApi {

    @ApiOperation(
        value = "Create map revision",
        nickname = "createMapRevision",
        notes = "Create map revision",
        authorizations = [Authorization(value = "http-bearer")]
    )
    @ApiResponses(
        value = [ApiResponse(code = 204, message = "No Content"), ApiResponse(code = 400, message = "Bad Request", response = GenericErrorDto::class), ApiResponse(code = 401, message = "Unauthorized")]
    )
    @RequestMapping(
        method = [RequestMethod.POST],
        value = ["/user/map/revisions"],
        produces = ["application/json"],
        consumes = ["application/json"]
    )
    fun createMapRevision(
        @ApiParam(value = "", required = true) @Valid @RequestBody createMapRevisionRequestDto: CreateMapRevisionRequestDto
    ): ResponseEntity<Unit> {
        return ResponseEntity(HttpStatus.NOT_IMPLEMENTED)
    }

    @ApiOperation(
        value = "Get latest map",
        nickname = "getLatestMap",
        notes = "Get latest map",
        response = GameMapDto::class,
        authorizations = [Authorization(value = "http-bearer")]
    )
    @ApiResponses(
        value = [ApiResponse(code = 200, message = "OK", response = GameMapDto::class), ApiResponse(code = 401, message = "Unauthorized")]
    )
    @RequestMapping(
        method = [RequestMethod.GET],
        value = ["/user/map/latest"],
        produces = ["application/json"]
    )
    fun getLatestMap(): ResponseEntity<GameMapDto> {
        return ResponseEntity(HttpStatus.NOT_IMPLEMENTED)
    }

    @ApiOperation(
        value = "Get map revision by ID",
        nickname = "getMapRevisionById",
        notes = "Get map revision by ID",
        response = GameMapRevisionDto::class,
        authorizations = [Authorization(value = "http-bearer")]
    )
    @ApiResponses(
        value = [ApiResponse(code = 200, message = "OK", response = GameMapRevisionDto::class), ApiResponse(code = 401, message = "Unauthorized"), ApiResponse(code = 404, message = "Not Found")]
    )
    @RequestMapping(
        method = [RequestMethod.GET],
        value = ["/user/map/revisions/{revisionId}"],
        produces = ["application/json"]
    )
    fun getMapRevisionById(
        @ApiParam(value = "ID of the code revision", required = true) @PathVariable("revisionId") revisionId: java.util.UUID
    ): ResponseEntity<GameMapRevisionDto> {
        return ResponseEntity(HttpStatus.NOT_IMPLEMENTED)
    }

    @ApiOperation(
        value = "Get map revisions",
        nickname = "getMapRevisions",
        notes = "Get list of all map revision IDs",
        response = GameMapRevisionDto::class,
        responseContainer = "List",
        authorizations = [Authorization(value = "http-bearer")]
    )
    @ApiResponses(
        value = [ApiResponse(code = 200, message = "OK", response = GameMapRevisionDto::class, responseContainer = "List"), ApiResponse(code = 401, message = "Unauthorized")]
    )
    @RequestMapping(
        method = [RequestMethod.GET],
        value = ["/user/map/revisions"],
        produces = ["application/json"]
    )
    fun getMapRevisions(): ResponseEntity<List<GameMapRevisionDto>> {
        return ResponseEntity(HttpStatus.NOT_IMPLEMENTED)
    }

    @ApiOperation(
        value = "Update latest map",
        nickname = "updateLatestMap",
        notes = "Update latest map",
        authorizations = [Authorization(value = "http-bearer")]
    )
    @ApiResponses(
        value = [ApiResponse(code = 204, message = "No Content"), ApiResponse(code = 400, message = "Bad Request", response = GenericErrorDto::class), ApiResponse(code = 401, message = "Unauthorized")]
    )
    @RequestMapping(
        method = [RequestMethod.POST],
        value = ["/user/map/latest"],
        produces = ["application/json"],
        consumes = ["application/json"]
    )
    fun updateLatestMap(
        @ApiParam(value = "", required = true) @Valid @RequestBody updateLatestMapRequestDto: UpdateLatestMapRequestDto
    ): ResponseEntity<Unit> {
        return ResponseEntity(HttpStatus.NOT_IMPLEMENTED)
    }
}
