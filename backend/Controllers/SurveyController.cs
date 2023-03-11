using backend.Dtos.SurveyDtos;
using backend.MediatRServices.Commands.SurveyAnswerCommands;
using backend.MediatRServices.Commands.SurveyCommands;
using backend.MediatRServices.Queries.SurveyQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

public class SurveyController : BaseController
{
    [Route("Page/{categoryId:int}/{pageIndex:int}/{pageSize:int}")]
    [HttpGet]
    public async Task<ActionResult> Page(int categoryId, int pageIndex, int pageSize) =>
        Return(await Mediator.Send(new GetSurveysForPageQuery(categoryId, UserId, pageIndex, pageSize)));

    [Route("Get/{surveyId:int}")]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Get(int surveyId) =>
        Return(await Mediator.Send(new GetSurveyQuery(surveyId, UserId)));

    [Route("GetQuestion/{surveyId:int}")]
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> GetQuestion(int surveyId) =>
        Return(await Mediator.Send(new GetSurveyQuestionQuery(surveyId)));

    [Route("Add")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Add([FromBody] AddSurveyDto addSurveyDto) =>
        Return(await Mediator.Send(new AddSurveyCommand(addSurveyDto, Username)));

    [Route("Edit")]
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit([FromBody] EditSurveyDto editSurveyDto) =>
        Return(await Mediator.Send(new EditSurveyCommand(editSurveyDto, UserId)));

    [Route("Remove/{id:int}")]
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Remove(int id) =>
        Return(await Mediator.Send(new DeleteSurveyCommand(id, UserId)));
}