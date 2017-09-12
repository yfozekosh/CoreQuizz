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
using CoreQuizz.WebService.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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
                return Ok(new ErrorServiceRespose("User id in token is invalid"));
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
                UserEmail = user
            };

            var result = await _commandDispatcher.ExecuteAsync(createSurveyCommand);

            if (result.IsSuccess)
            {
                return Ok(new OkServiceResponse<string>(""));
            }

            return Ok(new ErrorServiceRespose(result.Errors));
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

            SurveyListItem[] result = _queryDispatcher.Execute<SurveySearchPageQuery, SurveyListItem[]>(searchQuery);

            return Ok(new OkServiceResponse<SurveyListItem[]>(result));
        }

        [Route("{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<ServiceResponse> Survey([FromRoute] int? id)
        {
            if (!id.HasValue)
            {
                return new ErrorServiceRespose("id should be specified");
            }

            var query = new SurveyCreationPageQuery()
            {
                SurveyId = id.Value
            };

            SurveyPageResult result = await _queryDispatcher.ExecuteAsync<SurveyCreationPageQuery, SurveyPageResult>(query);
            
            return new OkServiceResponse<SurveyPageResult>(result);
        }
    }
}