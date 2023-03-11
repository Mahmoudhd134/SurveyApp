namespace backend.Models;

public class SurveyCategory
{
    public int Id { get; set; }
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}