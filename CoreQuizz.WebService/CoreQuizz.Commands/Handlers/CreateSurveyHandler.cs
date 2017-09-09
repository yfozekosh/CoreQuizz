using System;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.Commands.Commands;
using CoreQuizz.Commands.Contract;
using CoreQuizz.Commands.Handlers.Abstract;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Commands.Handlers
{
    public class CreateSurveyHandler : EfCommandHandler<CreateSurveyCommand>
    {
        public CreateSurveyHandler(SurveyContext surveyContext) : base(surveyContext)
        {
        }

        public override async Task<CommandResult> ExecuteAsync(CreateSurveyCommand command)
        {
            if (command == null) throw new ArgumentNullException(nameof(command));

            bool isSurveyWithSameNameExists = await SurveyContext.Surveys.AnyAsync(x => x.Title == command.Title);
            if (isSurveyWithSameNameExists)
            {
                return new CommandResult(false)
                {
                    Errors = $"Survey with name {command.Title} allready Exists"
                };
            }

            User user = await SurveyContext.Users.FirstOrDefaultAsync(u => u.Email == command.UserEmail);
            SurveyContext.Surveys.Add(new Survey()
            {
                Title = command.Title,
                CreatedBy = user
            });

            await SurveyContext.SaveChangesAsync();

            return new CommandResult(true);
        }
    }
}