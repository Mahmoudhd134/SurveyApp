using backend.Models.IdentityModels;

namespace backend.Models;

public class SurveyAnswer
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public int SurveyId { get; set; }
    public Survey Survey { get; set; }
    public int SurveyOptionId { get; set; }
    public SurveyOption SurveyOption { get; set; }
}