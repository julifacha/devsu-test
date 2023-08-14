using DevsuTest.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using ValidationException = FluentValidation.ValidationException;

namespace DevsuTest.Core.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionHandlingMiddleware> logger;
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                await ValidationFailureBadRequestException(context, ex).ConfigureAwait(false);
            }
            catch (EntityNotFoundException)
            {
                NotFoundException(context);
            }
            catch (DbUpdateException ex)
            {
                await DbUpdateException(context, ex);
            }
            catch (Exception ex)
            {
                await InternalServerError(context, ex);
            }
        }

        private Task ValidationFailureBadRequestException(HttpContext context, ValidationException ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return context.Response.WriteAsync(JsonSerializer.Serialize(ex.Errors.Select(x => new { x.PropertyName, x.ErrorMessage })));
        }

        private void NotFoundException(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }

        private async Task DbUpdateException(HttpContext context, Exception ex)
        {
            this.logger.LogError(ex, ex.Source);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string errorMessage = $"Se produjo un error interno en el servidor. " +
                $"{ (ex.InnerException?.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint") ?? false
                    ? "No se pudo eliminar la entidad porque tiene registros relacionados" 
                    : ex.Message) }";

            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Error = errorMessage }));
        }

        private async Task InternalServerError(HttpContext context, Exception ex)
        {
            this.logger.LogError(ex, ex.Source);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { Error = $"Se produjo un error interno en el servidor. {ex.Message}" }));
        }
    }
}
