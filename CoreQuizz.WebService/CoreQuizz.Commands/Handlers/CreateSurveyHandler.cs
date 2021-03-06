﻿using System;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.Commands.Commands;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Commands.Handlers.Abstract;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel;
using CoreQuizz.Shared.DomainModel.Survey;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreQuizz.Commands.Handlers
{
    public class CreateSurveyHandler : EfCommandHandler<CreateSurveyCommand>
    {
        protected override async Task<CommandResult> _ExecuteAsync(CreateSurveyCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            bool isSurveyWithSameNameExists = await SurveyContext.Surveys.AnyAsync(x => x.Title == command.Title);
            if (isSurveyWithSameNameExists)
            {
                return new CommandResult(false)
                {
                    Error = $"Survey with name {command.Title} allready Exists"
                };
            }

            User user = await SurveyContext.Users.FirstOrDefaultAsync(u => u.Email == command.UserEmail);
            SurveyContext.Surveys.Add(new Survey()
            {
                Title = command.Title,
                CreatedBy = user,
                Description = command.Description
            });

            await SurveyContext.SaveChangesAsync();

            return new CommandResult(true);
        }

        public CreateSurveyHandler(SurveyContext surveyContext, ILogger<EfCommandHandler<CreateSurveyCommand>> logger) : base(surveyContext, logger)
        {
        }
    }
}