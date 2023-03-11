namespace backend.MediatRServices.ErrorHandlers.Errors;

public static class DomainErrors
{
    public static readonly Error None = new Error(string.Empty, string.Empty);

    public static readonly Error UnKnownError = new Error("DomainError.UnknownError",
        "UnKnown error happens if you sea this please contact the support");
}