using backend.MediatRServices.Commands.SurveyCategoryCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin")]
public class SurveyCategoryController:BaseController
{
    [Route("Add/{surveyId:int}/{categoryId:int}")]
    [HttpGet]
    public async Task<ActionResult> Add(int surveyId, int categoryId) =>
        Return(await Mediator.Send(new AddSurveyCategoryCommand(UserId, surveyId, categoryId)));
    
    [Route("AddByName/{surveyId:int}/{categoryName}")]
    [HttpGet]
    public async Task<ActionResult> Add(int surveyId,string categoryName) =>
        Return(await Mediator.Send(new AddSurveyCategoryWithNameCommand(UserId, surveyId, categoryName)));
    
    [Route("Remove/{surveyId:int}/{categoryId:int}")]
    [HttpDelete]
    public async Task<ActionResult> Remvoe(int surveyId, int categoryId) =>
        Return(await Mediator.Send(new RemoveSurveyCategoryCommand(UserId, surveyId, categoryId)));
}