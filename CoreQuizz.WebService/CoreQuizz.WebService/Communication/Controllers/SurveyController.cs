using System;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;
using CoreQuizz.Commands.Commands;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.Contract.Result;
using CoreQuizz.Queries.PageQueries.Queries;
using CoreQuizz.Queries.PageQueries.Responces;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.WebService.Communication.Responses;
using CoreQuizz.WebService.Communication.ViewModel;
using CoreQuizz.WebService.Identity.JWT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoreQuizz.WebService.Communication.Controllers
{
    [Authorize]
    [Route("/api/survey")]
    public class SurveyController : Controller
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogger<SurveyController> _logger;

        public SurveyController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher,
            ILogger<SurveyController> logger)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
            _logger = logger;
        }

        [Route("get-all")]
        public IActionResult GetSurveys()
        {
            string userIdStr = User.FindFirstValue(CustomClaimType.UserId);
            int userId;

            if (String.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out userId))
            {
                return Ok(new ErrorServiceRespose("User id in token is invalid"));
            }

            var query = new SurveyListPageQuery()
            {
                UserId = userId
            };

            QueryResult<SurveyListItem[]> result =
                _queryDispatcher.Execute<SurveyListPageQuery, SurveyListItem[]>(query);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<SurveyListItem[]>(result.Value));
            }
            return BadRequest(result.Error);
        }

        [Route("create")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SurveyCreateViewModel model)
        {
            var user = User.Identity.Name;
            string userIdStr = User.FindFirstValue(CustomClaimType.UserId);
            int userId;

            if (String.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out userId))
            {
                return Ok(new ErrorServiceRespose("User id in token is invalid"));
            }

            var createSurveyCommand = new CreateSurveyCommand
            {
                Title = model.Title,
                Description = model.Description,
                UserEmail = user
            };

            var result = await _commandDispatcher.ExecuteAsync(createSurveyCommand);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<string>(""));
            }

            return Ok(new ErrorServiceRespose(result.Error));
        }

        [Route("get-global")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search([FromQuery] SurveySearchViewModel model)
        {
            if (model == null)
            {
                return Ok(new ErrorServiceRespose("parameters was not specified"));
            }

            if (model.ItemsOnPage > 200)
            {
                return Ok(new ErrorServiceRespose("you cannot request more then 200 items on page"));
            }

            if (model.PageNumber < 0)
            {
                return Ok(new ErrorServiceRespose("page number is illegal"));
            }

            var searchQuery = new SurveySearchPageQuery()
            {
                SearchText = model.SearchText ?? "",
                PageCount = model.ItemsOnPage,
                PageNumber = model.PageNumber
            };

            QueryResult<SurveyListItem[]> result =
                _queryDispatcher.Execute<SurveySearchPageQuery, SurveyListItem[]>(searchQuery);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<SurveyListItem[]>(result.Value));
            }

            return BadRequest(result.Error);
        }

        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Survey([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("id should be specified");
            }

            var query = new SurveyCreationPageQuery()
            {
                SurveyId = id.Value
            };

            QueryResult<SurveyPageResult> result =
                await _queryDispatcher.ExecuteAsync<SurveyCreationPageQuery, SurveyPageResult>(query);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<SurveyPageResult>(result.Value));
            }
            return BadRequest(result.Error);
        }

        [Route("save")]
        [HttpPost]
        public async Task<ActionResult> SaveSurvey([FromBody] SaveSurveyViewModel survey)
        {
            if (survey == null) return BadRequest("survey not specified");

            var command = new UpdateSurveyCommand()
            {
                Survey = new Survey()
                {
                    Id = survey.SurveyId,
                    Title = survey.SurveyName,
                    Description = survey.Description
                }
            };

            CommandResult result = await _commandDispatcher.ExecuteAsync(command);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<string>("saved"));
            }

            if (result.Error != null)
            {
                return BadRequest(new ErrorServiceRespose(result.Error));
            }

            return BadRequest(new ErrorServiceRespose("Unexpected error"));
        }

        [Route("{id}/edit")]
        [HttpGet]
        public async Task<ActionResult> GetSurveyForEdit([FromRoute] int id)
        {
            Claim userClaim = User.FindFirst(CustomClaimType.UserId);
            if (String.IsNullOrWhiteSpace(userClaim?.Value))
            {
                _logger.LogError($"Request edit survey {id}.Api token missing userID");
                throw new AuthenticationException("Api token not contains userID");
            }

            int userId;
            if (!int.TryParse(userClaim.Value, out userId))
            {
                return NotFound($"No user with id {id}");
            }

            var query = new GetSurveyForEditQuery(id, userId);

            QueryResult<SurveyDefinitionViewModel> result =
                await _queryDispatcher.ExecuteAsync<GetSurveyForEditQuery, SurveyDefinitionViewModel>(query);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<SurveyDefinitionViewModel>(result.Value));
            }

            return BadRequest(result.Error);
        }
    }
}