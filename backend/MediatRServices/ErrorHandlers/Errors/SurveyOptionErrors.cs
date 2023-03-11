namespace backend.MediatRServices.ErrorHandlers.Errors;

public static class SurveyOptionErrors
{
    public static readonly Error WrongId = new("SurveyOption.WrongId",
        "The id is wrong");
    
    public static readonly Error UnAuthorizedAdd = new("SurveyOption.UnAuthorizedAdd",
        "Can not add an option to a survey you not created");

    public static readonly Error TheOptionIsAlreadyThere = new("SurveyOption.RepeatedOption",
        "You try to add the same option twice");
    
    public static readonly Error UnAuthorizedEdit = new("SurveyOption.UnAuthorizedEdit",
        "Can not edit an option of a survey you not created");

    public static readonly Error UnAuthorizedDelete = new("SurveyOption.UnAuthorizedRemove",
        "Can not delete an option of a survey you not created");

    public static readonly Error WrongOption = new("SurveyOption.WrongOption",
        "The survey does not contain this option");
}