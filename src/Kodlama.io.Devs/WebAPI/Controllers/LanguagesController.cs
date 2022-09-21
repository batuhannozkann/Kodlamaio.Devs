using Application.Features.Languages.Commands.CreateLanguage;
using Application.Features.Languages.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Core.Application.Requests;
using Application.Features.Languages.Queries.GetListLanguage;
using Application.Features.Languages.Models;
using Application.Features.Languages.Queries.GetByIdLanguage;
using Application.Features.Languages.Commands.DeleteLanguage;
using Application.Features.Languages.Commands.UpdateLanguage;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddAsync([FromBody] CreateLanguageCommand createLanguageCommand)
        {

            CreatedLanguageDto result = await Mediator.Send(createLanguageCommand);
            return Created("", result);
        }
        [HttpGet("GetAllLanguage")]
        [Authorize]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListLanguageQuery getListLanguageQuery = new() { PageRequest = pageRequest };
            LanguageListModel result = await Mediator.Send(getListLanguageQuery);
            return Ok(result);
        }
        [HttpGet("GetLanguage/{Id}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdLanguageQuery getByIdLanguageQuery)
        {
            LanguageGetByIdDto result = await Mediator.Send(getByIdLanguageQuery);
            return Ok(result);
        }
        [HttpPost("DeleteById/{Id}")]
        public async Task<IActionResult> DeleteByIdAsync([FromRoute] DeleteByIdLanguageCommand deleteByIdLanguageCommand)
        {

            DeletedLanguageDto result = await Mediator.Send(deleteByIdLanguageCommand);
            return Ok(result);
        }
        [HttpPost("DeleteByName/{Name}")]
        public async Task<IActionResult> DeleteByNameAsync([FromRoute] DeleteByNameLanguageCommand deleteByNameLanguageCommand)
        {

            DeletedLanguageDto result = await Mediator.Send(deleteByNameLanguageCommand);
            return Ok(result);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateLanguageCommand updateLanguageCommand)
        {
            UpdatedLanguageDto result = await Mediator.Send(updateLanguageCommand);
            return Ok(result);
        }
        

    }
}
