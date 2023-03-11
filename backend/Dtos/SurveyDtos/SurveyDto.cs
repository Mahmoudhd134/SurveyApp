using backend.Dtos.CategoryDtos;

namespace backend.Dtos.SurveyDtos;

public class SurveyDto
{
    public int Id { get; set; }
    public bool IsTheMaker { get; set; }
    public string Question { get; set; }
    public bool IsAnswered { get; set; }
    public int AnswerId { get; set; }
    public int TotalAnswers { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<SurveyOptionWithUsers> OptionWithUsers { get; set; }
}