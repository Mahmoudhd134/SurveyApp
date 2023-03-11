namespace backend.MediatRServices.ErrorHandlers.Errors;

public static class SurveyCategoryErrors
{
    public static readonly Error UnAuthorizedAdd = new("SurveyCategory.UnAuthorizedAdd",
        "You can not add a category for a survey you don't create");

    public static readonly Error CategoryAlreadyExists = new("SurveyCategory.CategoryAlreadyExists",
        "The category is already exists");

    public static readonly Error UnAuthorizedRemove = new("SurveyCategory.UnAuthorizedRemove",
        "You can not remove a category for a survey you don't create");

    public static readonly Error SurveyDoseNotHaseCategory = new("SurveyCategory.SurveyDoseNotHaseCategory",
        "The survey does not has that category");
}