namespace backend.Dtos.SurveyDtos;

public class SurveyForPageDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public bool IsAnswered { get; set; }
    public int AnswerId { get; set; }
    public int TotalAnswers { get; set; }
    public List<OptionDto> OptionDtos { get; set; }
}