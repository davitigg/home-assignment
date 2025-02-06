using Application.Common.Results;

namespace API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToHttpResponse(this Result result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok();
            }
            else
            {
                return MapErrorResponse(result.Error, result);
            }
        }

        public static IResult ToHttpResponse<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            else
            {
                return MapErrorResponse(result.Error, result);
            }
        }

        public static IResult MapErrorResponse(Error error, Result result)
        {
            return error.Code switch
            {
                ErrorTypeConstant.ValidationError => Results.BadRequest(result.Error),
                ErrorTypeConstant.BadRequest => Results.BadRequest(result.Error),
                ErrorTypeConstant.NotFound => Results.NotFound(result.Error),
                ErrorTypeConstant.Conflict => Results.Conflict(result.Error),
                _ => Results.Problem(detail: error.Description, statusCode: 500),
            };
        }
    }
}