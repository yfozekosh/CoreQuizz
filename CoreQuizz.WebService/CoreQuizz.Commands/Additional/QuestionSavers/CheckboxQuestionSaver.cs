using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreQuizz.Commands.Additional.QuestionSavers.Abstract;
using CoreQuizz.Commands.Contract;
using CoreQuizz.DataAccess.DbContext;
using CoreQuizz.Shared.DomainModel.Survey;
using CoreQuizz.Shared.DomainModel.Survey.Question;
using CoreQuizz.Shared.DomainModel.Survey.Question.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CoreQuizz.Commands.Additional.QuestionSavers
{
    public class CheckboxQuestionSaver : EfQuestionSaver<CheckboxQuestion>
    {
        public CheckboxQuestionSaver(SurveyContext surveyContext) : base(surveyContext)
        {
        }

        public override async Task<CommandResult> SaveAsync(Survey survey, CheckboxQuestion question)
        {
            if (question == null) throw new ArgumentNullException(nameof(question));
            Question dbQuestion = SurveyContext.Find<Question>(question.Id);

            if (dbQuestion == null)
            {
                survey.Questions.Add(question);
                return new CommandResult(true);
            }
            
            var checkboxQuestion = dbQuestion as CheckboxQuestion;
            if (checkboxQuestion == null)
            {
                SurveyContext.Entry(dbQuestion).State = EntityState.Deleted;
            }
            else
            {
                await SurveyContext.Entry(checkboxQuestion).Collection(q => q.Options).LoadAsync();
                
                checkboxQuestion.ModifieDateTime = DateTime.Now;
                checkboxQuestion.QuestionLabel = question.QuestionLabel;

                if (checkboxQuestion.Options == null)
                {
                    checkboxQuestion.Options = new List<QuestionOption>();
                }
                
                await SurveyContext.Entry(checkboxQuestion).Collection(q => q.Options).LoadAsync();

                if (checkboxQuestion.Options == null)
                {
                    checkboxQuestion.Options = new List<QuestionOption>();
                }

                IList<QuestionOption> inputOptions = question.Options ?? new List<QuestionOption>();

                foreach (QuestionOption questionOption in checkboxQuestion.Options)
                {
                    QuestionOption newOption = inputOptions.FirstOrDefault(o => o.Id == questionOption.Id);


                    if (newOption != null)
                    {
                        questionOption.Value = newOption.Value;
                        questionOption.ModifieDateTime = DateTime.Now;
                        questionOption.IsSelected = newOption.IsSelected;
                    }
                    else
                    {
                        SurveyContext.Entry(questionOption).State = EntityState.Deleted;
                    }
                }

                IList<QuestionOption> optionsToAdd = inputOptions.Where(i => checkboxQuestion.Options.All(o => o.Id != i.Id)).ToList();
                
                foreach (var questionOption in optionsToAdd)
                {
                    checkboxQuestion.Options.Add(questionOption);                    
                }
            }

            return new CommandResult(true);
        }
    }
}