namespace backend.Dtos.SurveyDtos;

public class SurveyOptionWithUsers : OptionDto
{
    public List<string> UserIds { get; set; }
    public List<string> Usernames { get; set; }
}