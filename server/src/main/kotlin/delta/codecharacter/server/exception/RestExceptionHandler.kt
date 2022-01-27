package delta.codecharacter.server.exception

import com.fasterxml.jackson.module.kotlin.MissingKotlinParameterException
import org.springframework.beans.ConversionNotSupportedException
import org.springframework.beans.TypeMismatchException
import org.springframework.core.Ordered
import org.springframework.core.annotation.Order
import org.springframework.http.HttpHeaders
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.http.converter.HttpMessageNotReadableException
import org.springframework.http.converter.HttpMessageNotWritableException
import org.springframework.validation.BindException
import org.springframework.web.HttpMediaTypeNotAcceptableException
import org.springframework.web.HttpMediaTypeNotSupportedException
import org.springframework.web.HttpRequestMethodNotSupportedException
import org.springframework.web.bind.MethodArgumentNotValidException
import org.springframework.web.bind.MissingPathVariableException
import org.springframework.web.bind.MissingServletRequestParameterException
import org.springframework.web.bind.ServletRequestBindingException
import org.springframework.web.bind.annotation.ControllerAdvice
import org.springframework.web.bind.annotation.ExceptionHandler
import org.springframework.web.context.request.WebRequest
import org.springframework.web.context.request.async.AsyncRequestTimeoutException
import org.springframework.web.multipart.support.MissingServletRequestPartException
import org.springframework.web.servlet.NoHandlerFoundException
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler
import javax.validation.ConstraintViolationException


@Order(Ordered.HIGHEST_PRECEDENCE)
@ControllerAdvice
class RestExceptionHandler : ResponseEntityExceptionHandler() {

    override fun handleHttpMessageNotReadable(
        ex: HttpMessageNotReadableException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        val cause = ex.cause
        return if (cause is MissingKotlinParameterException) {
            ResponseEntity.status(HttpStatus.BAD_REQUEST).body(
                mapOf(
                    "message" to "${cause.parameter.name} is missing"
                )
            )
        } else {
            ResponseEntity.status(HttpStatus.BAD_REQUEST).body(
                mapOf(
                    "message" to "Unknown error"
                )
            )
        }
    }

    override fun handleHttpRequestMethodNotSupported(
        ex: HttpRequestMethodNotSupportedException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleHttpRequestMethodNotSupported(ex, headers, status, request)
    }

    override fun handleHttpMediaTypeNotSupported(
        ex: HttpMediaTypeNotSupportedException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleHttpMediaTypeNotSupported(ex, headers, status, request)
    }

    override fun handleHttpMediaTypeNotAcceptable(
        ex: HttpMediaTypeNotAcceptableException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleHttpMediaTypeNotAcceptable(ex, headers, status, request)
    }

    override fun handleMissingPathVariable(
        ex: MissingPathVariableException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleMissingPathVariable(ex, headers, status, request)
    }

    override fun handleMissingServletRequestParameter(
        ex: MissingServletRequestParameterException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleMissingServletRequestParameter(ex, headers, status, request)
    }

    override fun handleServletRequestBindingException(
        ex: ServletRequestBindingException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleServletRequestBindingException(ex, headers, status, request)
    }

    override fun handleConversionNotSupported(
        ex: ConversionNotSupportedException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleConversionNotSupported(ex, headers, status, request)
    }

    override fun handleTypeMismatch(
        ex: TypeMismatchException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleTypeMismatch(ex, headers, status, request)
    }

    override fun handleHttpMessageNotWritable(
        ex: HttpMessageNotWritableException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleHttpMessageNotWritable(ex, headers, status, request)
    }

    override fun handleMethodArgumentNotValid(
        ex: MethodArgumentNotValidException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        return if (ex.bindingResult.fieldErrors.isNotEmpty()) {
            val fields = mutableListOf<String>()
            ex.bindingResult.fieldErrors.forEach { fieldError -> fields.add(fieldError.field) }
            ResponseEntity.status(HttpStatus.BAD_REQUEST).body(
                mapOf(
                    "message" to "Invalid ${fields.toSet().joinToString(", ")}"
                )
            )
        } else {
            ResponseEntity.status(HttpStatus.BAD_REQUEST).body(
                mapOf(
                    "message" to "Invalid fields"
                )
            )
        }
    }

    override fun handleMissingServletRequestPart(
        ex: MissingServletRequestPartException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleMissingServletRequestPart(ex, headers, status, request)
    }

    override fun handleBindException(
        ex: BindException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleBindException(ex, headers, status, request)
    }

    override fun handleNoHandlerFoundException(
        ex: NoHandlerFoundException,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleNoHandlerFoundException(ex, headers, status, request)
    }

    override fun handleAsyncRequestTimeoutException(
        ex: AsyncRequestTimeoutException,
        headers: HttpHeaders,
        status: HttpStatus,
        webRequest: WebRequest
    ): ResponseEntity<Any>? {
        println(ex)
        return super.handleAsyncRequestTimeoutException(ex, headers, status, webRequest)
    }

    override fun handleExceptionInternal(
        ex: Exception,
        body: Any?,
        headers: HttpHeaders,
        status: HttpStatus,
        request: WebRequest
    ): ResponseEntity<Any> {
        println(ex)
        return super.handleExceptionInternal(ex, body, headers, status, request)
    }

    @ExceptionHandler(ConstraintViolationException::class)
    protected fun handleConstraintViolation(
        ex: ConstraintViolationException
    ): ResponseEntity<Any> {
        println(ex)
        return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(
            mapOf(
                "message" to ex.constraintViolations.joinToString(", ")
            )
        )
    }

    @ExceptionHandler(CustomException::class)
    protected fun handleCustomException(
        ex: CustomException
    ): ResponseEntity<Any> {
        return ResponseEntity.status(ex.status).body(
            mapOf(
                "message" to ex.message
            )
        )
    }
}