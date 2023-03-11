using backend.Dtos.SurveyOptionDtos;
using backend.MediatRServices.Commands.SurveyOptionCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize(Roles = "Admin")]
public class SurveyOptionController : BaseController
{
    [Route("Add")]
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AddSurveyOptionDto addSurveyOptionDto) =>
        Return(await Mediator.Send(new AddSurveyOptionCommand(addSurveyOptionDto, UserId)));


    [Route("Remove/{id:int}")]
    [HttpDelete]
    public async Task<ActionResult> Edit(int id) =>
        Return(await Mediator.Send(new RemoveSurveyOptionCommand(id, UserId)));
}