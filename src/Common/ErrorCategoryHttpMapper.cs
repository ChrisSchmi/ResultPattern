namespace ResultPatternDemo.Common
{
    public static class ErrorCategoryHttpMapper
    {
        public static int ToStatusCode(ErrorCategory category) => category switch
        {
            ErrorCategory.Validation          => 400,
            ErrorCategory.Unauthorized        => 401,
            ErrorCategory.Forbidden           => 403,
            ErrorCategory.NotFound            => 404,
            ErrorCategory.Conflict            => 409,
            ErrorCategory.UnprocessableEntity => 422,
            ErrorCategory.Failure             => 500,
            _                                 => 500
        };
    }
}
