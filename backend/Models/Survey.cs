using System.Collections.ObjectModel;
using backend.Models.IdentityModels;

namespace backend.Models;

public class Survey
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public List<SurveyOption> SurveyOptions { get; set; } = new();
    public List<SurveyCategory> SurveyCategories { get; set; }= new();
    public List<SurveyAnswer> SurveyAnswers { get; set; }= new();
}