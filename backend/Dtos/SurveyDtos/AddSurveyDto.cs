namespace backend.Dtos.SurveyDtos;

public class AddSurveyDto
{
    public string Survey { get; set; }
    public IEnumerable<string> Options { get; set; }
    public IEnumerable<string> Categories { get; set; }
    
}