using SecureFlight.Core;

namespace SecureFlight.Api.Utils;

public static class OperationResultExtensions
{
    public static int ToHttpStatusCode(this ErrorCode operationResult)
    {
        return operationResult switch
        {
            ErrorCode.BadRequest => 400,
            ErrorCode.NotFound => 404,
            _ => 500
        };
    }
}