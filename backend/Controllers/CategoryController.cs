using backend.Dtos.CategoryDtos;
using backend.MediatRServices.Commands.CategoryCommands;
using backend.MediatRServices.Queries.CategoryQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Authorize]
public class CategoryController : BaseController
{
    [Route("Page/{pageIndex:int}/{pageSize:int}/{prefix?}")]
    [HttpGet]
    public async Task<ActionResult> Page(int pageIndex, int pageSize, string prefix = "") =>
        Return(await Mediator.Send(new GetCategoriesPyPageQuery(prefix, pageIndex, pageSize)));

    [Route("{id:int}")]
    [HttpGet]
    public async Task<ActionResult> Get(int id) =>
        Return(await Mediator.Send(new GetCategoryPyIdQuery(id)));

    [Route("Add")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Add([FromBody] AddCategoryDto addCategoryDto) =>
        Return(await Mediator.Send(new AddCategoryCommand(addCategoryDto)));

    [Route("Edit")]
    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Edit([FromBody] EditCategoryDto editCategoryDto) =>
        Return(await Mediator.Send(new EditCategoryCommand(editCategoryDto)));

    [Route("Remove/{id:int}")]
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Remove(int id) =>
        Return(await Mediator.Send(new DeleteCategoryCommand(id)));
}