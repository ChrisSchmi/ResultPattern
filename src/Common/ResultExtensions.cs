using Microsoft.AspNetCore.Mvc;

namespace ResultPatternDemo.Common
{
    /// <summary>
    /// Mappt BaseResult-Instanzen direkt auf ASP.NET-Core-ActionResults,
    /// damit Controller kein IsSuccess/Error-Handling selbst schreiben müssen.
    /// </summary>
    public static class ResultExtensions
    {
        // Result MIT Value -> 200 + Payload im Erfolgsfall, sonst Fehlerstatus
        public static ActionResult<TValue> ToActionResult<TValue, TError>(
            this BaseResult<TValue, TError> result)
            where TError : IBaseError
        {
            if (result.IsSuccess)
                return new OkObjectResult(result.Value);

            return MapError(result.Error!);
        }

        // Result OHNE Value -> 204 NoContent im Erfolgsfall, sonst Fehlerstatus
        public static ActionResult ToActionResult<TError>(
            this BaseResult<TError> result)
            where TError : IBaseError
        {
            if (result.IsSuccess)
                return new NoContentResult();

            return MapError(result.Error!);
        }

        private static ObjectResult MapError(IBaseError error)
        {
            var statusCode = ErrorCategoryHttpMapper.ToStatusCode(error.Category);

            return new ObjectResult(new ProblemDetails
            {
                Title = error.Category.ToString(),
                Detail = error.Description,
                Status = statusCode
            })
            { StatusCode = statusCode };
        }
    }
}
