using CoreQuizz.Commands.Contract;
using CoreQuizz.Shared.DomainModel.Survey;

namespace CoreQuizz.Commands.Commands
{
    public class UpdateSurveyCommand : ICommand
    {
        public Survey Survey { get; set; }
    }
}