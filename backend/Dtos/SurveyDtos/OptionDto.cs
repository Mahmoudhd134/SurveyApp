namespace backend.Dtos.SurveyDtos;

public class OptionDto
{
    public int Id { get; set; }
    public string Option { get; set; }
    public bool IsAnswered { get; set; }
    public int Count { get; set; }
}