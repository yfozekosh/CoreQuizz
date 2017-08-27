using System.Collections.Generic;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL
{
    public static class AppSeed
    {
        public static void SeedDatabaseIfDevelop(IAccountManager accountManager, ISurveyManager surveyManager, string email)
        {
            if (!accountManager.IsUserExists(email))
            {
                accountManager.RegisterUser("yfozekosh@gmail.com", "1234");
                surveyManager.CreateSurvey(new Survey()
                {
                    Title = "Title",
                    Questions = new List<Question>()
                        {
                            new CheckboxQuestion()
                            {
                                QuestionLabel = "What about checkboxes?",
                                Options = new List<QuestionOption>()
                                {
                                    new QuestionOption()
                                    {
                                        IsSelected = true,
                                        Value = "select1"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select2"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select3"
                                    }
                                }
                            },
                            new RadioQuestion()
                            {
                                QuestionLabel = "What about radio?",
                                Options = new List<QuestionOption>()
                                {
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select1"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select2"
                                    },
                                    new QuestionOption()
                                    {
                                        IsSelected = false,
                                        Value = "select3"
                                    }
                                }
                            },
                            new InputQuestion()
                            {
                                QuestionLabel = "What about input?"
                            }
                        }
                }, email);
            }
        }
    }
}
