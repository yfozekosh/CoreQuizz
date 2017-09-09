using CoreQuizz.Commands.Contract;

namespace CoreQuizz.Commands.Commands
{
    public class CreateSurveyCommand : ICommand
    {
        public string Title { get; set; }

        public string UserEmail { get; set; }
    }
}