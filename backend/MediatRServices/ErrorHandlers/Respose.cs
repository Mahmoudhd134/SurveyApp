using backend.MediatRServices.ErrorHandlers.Errors;

namespace backend.MediatRServices.ErrorHandlers;

public class Response<TResult>
{
    public bool IsSuccess { get; set; }
    public TResult Data { get; set; }
    public Error Error { get; set; }

    public Response(bool isSuccess, TResult result, Error error)
    {
        if (isSuccess && error != DomainErrors.None)
            throw new InvalidOperationException();

        if (!isSuccess && error == DomainErrors.None)
            throw new InvalidOperationException();


        IsSuccess = isSuccess;
        Data = result;
        Error = error;
    }

    public static Response<TResult> Success(TResult result) =>
         new(true, result, DomainErrors.None);

    public static Response<TResult> Failure(Error error) =>
         new(false, default, error);


    public static implicit operator Response<TResult>(TResult value) => new(true, value, DomainErrors.None);
}