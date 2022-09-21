using Application.Features.GitHubProfiles.Commands.AddGitHubProfile;
using Application.Features.GitHubProfiles.Commands.DeleteGitHubProfile;
using Application.Features.GitHubProfiles.Commands.UpdateGitHubProfile;
using Application.Features.GitHubProfiles.Dtos;
using Application.Features.GitHubProfiles.Models;
using Application.Features.GitHubProfiles.Queries.GetListGitHubProfiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GitHubProfileController : BaseController
    {
        [HttpPost("Add")]
        public async Task<IActionResult> AddProfileAsync ([FromBody] CreateGitHubProfileCommand createGitHubProfileCommand)
        {
            CreatedGitHubProfileDto result = await Mediator.Send(createGitHubProfileCommand);
            return Ok(result);
        }
        [HttpGet("GetProfiles")]
        public async Task<IActionResult> GetListUserProfile([FromQuery] GetListGitHubProfileQuery getListGitHubProfileQuery)
        {
            GetListGitHubProfileQuery getListGitHubProfileQuery1 = new GetListGitHubProfileQuery { PageRequest = getListGitHubProfileQuery.PageRequest, UserEmail = getListGitHubProfileQuery.UserEmail };
            GitHubProfileListModel gitHubProfileListModel = await Mediator.Send(getListGitHubProfileQuery1);
            return Ok(gitHubProfileListModel);
        }
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateProfilAsync([FromBody] UpdateGitHubProfileCommand updateGitHubProfileCommand)
        {
            UpdatedGitHubProfileDto result = await Mediator.Send(updateGitHubProfileCommand);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteProfilAsync([FromBody] DeleteGitHubProfileCommand deleteGitHubProfileCommand)
        {
            DeletedGitHubProfileDto result = await Mediator.Send(deleteGitHubProfileCommand);
            return Ok(result);
        }
    }
}
