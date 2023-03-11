namespace backend.MediatRServices.ErrorHandlers.Errors;

public static class CategoryErrors
{
    public static readonly Error NameAlreadyExistsError = new("Category.NameIsUnAvailable",
        "The category name is already found,try another");
    
    public static readonly Error WrongId = new("Category.WrongId",
        "Can not find category with that id");

    public static readonly Error WrongName = new("Category.WrongName",
        "Can not find category with that name");
}