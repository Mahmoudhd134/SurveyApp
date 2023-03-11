namespace backend.MediatRServices.ErrorHandlers.Errors;

public static class SurveyAnswerErrors
{
    public static readonly Error WrongId = new("Survey.WrongId",
        "The id is wrong");
    
    public static readonly Error UnAuthorizedDelete = new("SurveyAnswer.UnAuthorizedRemove",
        "Can not delete a survey answer you not make it");
}