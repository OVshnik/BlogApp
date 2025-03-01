using BlogApp.API.Services;
using BlogApp.API.ViewModels.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
	private readonly ITagService _tagService;
	public TagController(ITagService tagService)
	{
		_tagService = tagService;
	}
	[Authorize("OnlyAdmin")]
	[HttpPost]
	[Route("CreateTag")]
	public async Task<IActionResult> AddTag(CreateTagViewModel model)
	{
		if (ModelState.IsValid)
		{
			await _tagService.CreateNewTagAsync(model);
			return StatusCode(201);
		}
		return BadRequest("Указанные данные не прошли валидацию");
	}
	[Authorize("OnlyAdmin")]
	[HttpGet]
	[Route("GetTag")]
	public async Task<IActionResult> GetTag(Guid id)
	{
		var tag = await _tagService.GetTagAsync(id);
		if (tag != null)
			return StatusCode(200, tag);
		return NotFound();
	}
	[Authorize("OnlyAdmin")]
	[HttpGet]
	[Route("GetAllTags")]
	public async Task<IActionResult> GetAllTag()
	{
		var tags = await _tagService.GetAllTagsAsync();
		if (tags != null)
			return StatusCode(200, tags);
		return NotFound();
	}
	[Authorize("OnlyAdmin")]
	[HttpPatch]
	[Route("EditTag")]
	public async Task<IActionResult> EditTag(EditTagViewModel model)
	{
		if (ModelState.IsValid)
		{
			await _tagService.UpdateTagAsync(model);
			return StatusCode(200);
		}
		return BadRequest("Указанные данные не прошли валидацию");
	}
	[Authorize("OnlyAdmin")]
	[HttpDelete]
	[Route("DeleteTag")]
	public async Task<IActionResult> DeleteTag(Guid id)
	{
		await _tagService.DeleteTagAsync(id);
		return StatusCode(200);
	}
}
