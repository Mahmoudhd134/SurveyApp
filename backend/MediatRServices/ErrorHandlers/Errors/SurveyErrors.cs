namespace backend.MediatRServices.ErrorHandlers.Errors;

public static class SurveyErrors
{
    public static readonly Error SameSurveyFound = new("Survey.SameSurveyFound",
        "A survey with the same question has already add,chose another question");

    public static readonly Error WrongId = new("Survey.WrongId",
        "The id is wrong");

    public static readonly Error UnAuthorizedEdit = new("Survey.UnAuthorizedEdit",
        "Can not edit a survey you not created");

    public static readonly Error UnAuthorizedDelete = new("Survey.UnAuthorizedRemove",
        "Can not delete a survey you not created");

    public static readonly Error DuplicateAnswerError = new("Survey.DuplicateAnswer",
        "The user can not have two answers for the same survey");
}