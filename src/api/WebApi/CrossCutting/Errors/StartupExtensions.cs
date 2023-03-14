using System;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Domain.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.CrossCutting.Errors
{
    public static class StartupExtensions
    {
        public static IApplicationBuilder UseDatingAppExceptionHandler(this IApplicationBuilder appBuilder)
        {
            appBuilder.UseExceptionHandler(new ExceptionHandlerOptions
            {
                AllowStatusCode404Response = true,
                ExceptionHandler = WriteErrorResponse
            });

            return appBuilder;
        }

        private static Task WriteErrorResponse(HttpContext httpContext)
        {
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var originalException = exceptionDetails.Error;

            if (originalException == null)
                return Task.CompletedTask;

            httpContext.Response.ContentType = "application/problem+json";

            var problem = CreateProblemDetails(originalException, httpContext.TraceIdentifier);

            httpContext.Response.StatusCode = problem.Status!.Value;

            var stream = httpContext.Response.Body;

            return JsonSerializer.SerializeAsync(stream, problem);
        }

        private static ProblemDetails CreateProblemDetails(Exception originalException, string? traceIdentifier)
        {
            var problem = new ProblemDetails
            {
                Title = "An unexpected server error occured",
                Status = (int) HttpStatusCode.InternalServerError
            };

            if (originalException is ApiException apiException)
            {
                problem.Status = apiException.StatusCode;
                problem.Title = apiException.HasMessage ? apiException.Message : problem.Title;
                problem.Detail = apiException.HasDetails ? apiException.Details : null;
            }
            else if (originalException is DomainException domainException)
            {
                problem.Status = (int) HttpStatusCode.BadRequest;
                problem.Title = domainException.Message;
            }

            var traceId = Activity.Current?.Id ?? traceIdentifier;

            if (traceId != null)
            {
                problem.Extensions["traceId"] = traceId;
            }

            return problem;
        }
    }
}
