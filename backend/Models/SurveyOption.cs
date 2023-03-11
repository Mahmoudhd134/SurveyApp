using System.Collections;

namespace backend.Models;

public class SurveyOption
{
    public int Id { get; set; }
    public string Option { get; set; }
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }

    public List<SurveyAnswer> SurveyAnswers { get; set; } = new();
}