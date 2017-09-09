using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreQuizz.Commands.Commands;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CoreQuizz.WebService.Controllers
{
    [Authorize]
    [Route("/api/survey")]
    public class SurveyController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public SurveyController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [Route("get-all")]
        public IActionResult GetSurveys()
        {
            string userIdStr = User.FindFirstValue(CustomClaimType.UserId);
            int userId;

            if (String.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out userId))
            {
                return BadRequest(new ErrorServiceRespose("User id in token is invalid"));
            }

            var query = new SurveyListPageQuery()
            {
                UserId = userId
            };

            SurveyListItem[] result = _queryDispatcher.Execute<SurveyListPageQuery, SurveyListItem[]>(query);

            return Ok(new OkServiceResponse<SurveyListItem[]>(result));
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string title)
        {
            var user = User.Identity.Name;
            string userIdStr = User.FindFirstValue(CustomClaimType.UserId);
            int userId;

            if (String.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out userId))
            {
                return BadRequest(new ErrorServiceRespose("User id in token is invalid"));
            }

            var createSurveyCommand = new CreateSurveyCommand
            {
                Title = title,
                UserEmail = user
            };

            var result = await _commandDispatcher.ExecuteAsync(createSurveyCommand);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<string>(""));
            }

            return BadRequest(new ErrorServiceRespose(result.Errors));
        }
    }
}