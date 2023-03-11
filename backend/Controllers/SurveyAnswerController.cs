using backend.MediatRServices.Commands.SurveyAnswerCommands;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class SurveyAnswerController : BaseController
{
    [Route("Add/{surveyId}/{optionId}")]
    [HttpGet]
    public async Task<ActionResult> Add(int surveyId, int optionId) =>
        Return(await Mediator.Send(new AddSurveyAnswerCommand(UserId, surveyId, optionId)));

    [Route("Remove/{answerId:int}")]
    [HttpDelete]
    public async Task<ActionResult> Remove(int answerId) =>
        Return(await Mediator.Send(new RemoveSurveyAnswerCommand(answerId, UserId)));
}