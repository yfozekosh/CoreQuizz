using System;
using System.Collections.Generic;
using System.Text;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.Contracts
{
    public interface ISurveyManager
    {
        void CreateSurvey(Survey survey, string userLogin);

        void DeleteSurvey(Survey survey);

        IList<Survey> GetSurveys(string userLogin);

        IList<Survey> GetSurveys(Func<Survey, bool> predicate = null);

        Survey GetSurvey(int id);

        Survey LoadFullSurvey(Survey survey);

        IList<Survey> LoadFullSurveys(IList<Survey> surveys);

        void UpdateSurvey(Survey survey);
    }
}
