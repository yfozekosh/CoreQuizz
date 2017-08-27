using System;
using System.Collections.Generic;
using System.Linq;
using CoreQuizz.BAL.Contracts;
using CoreQuizz.DataAccess.Contract.Contracts;
using CoreQuizz.Shared.DomainModel;

namespace CoreQuizz.BAL.Managers
{
    public class SurveyManager : ISurveyManager, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuestionManager _questionManager;

        public SurveyManager(IUnitOfWork unitOfWork, IQuestionManager questionManager)
        {
            _unitOfWork = unitOfWork;
            _questionManager = questionManager;
        }

        public void CreateSurvey(Survey survey, string userLogin)
        {
            User dbUser = _unitOfWork.GetRepository<User>().Get(user=>user.Email==userLogin).FirstOrDefault();
            if (dbUser == null) throw new ArgumentException($"User {userLogin} do not exists.");

            if (dbUser.Surveys == null)
            {
                dbUser.Surveys = new List<Survey>();
            }
            survey.CreatedBy = dbUser;
            //dbUser.Surveys.Add(survey);
            _unitOfWork.GetRepository<Survey>().Add(survey);
            
            _unitOfWork.Save();
        }

        public void DeleteSurvey(Survey survey)
        {
            _unitOfWork.GetRepository<Survey>().Delete(survey);
            _unitOfWork.Save();
        }

        public IList<Survey> GetSurveys(string userLogin)
        {
            return
                _unitOfWork.GetRepository<User>()
                    .Get(x => x.Email == userLogin, x => x.Surveys)
                    .FirstOrDefault()?.Surveys.ToList();
        }

        public IList<Survey> GetSurveys(Func<Survey, bool> predicate = null)
        {
            using (IRepository<Survey> repository = _unitOfWork.GetRepository<Survey>())
            {
                if (predicate == null)
                {
                    return repository.GetAll().ToList();
                }
                return repository.Get(predicate).ToList();
            }
        }

        public Survey LoadFullSurvey(Survey survey)
        {
            IList<Question> questions = _unitOfWork.GetRepository<Question>()
                .Get(question => question.Survey != null && question.Survey.Id == survey.Id, question => question.Survey).ToList();

            _questionManager.LoadFullQuestions(questions);
            survey.Questions = questions;
            return survey;
        }

        public IList<Survey> LoadFullSurveys(IList<Survey> surveys)
        {
            return surveys.Select(LoadFullSurvey).ToList();
        }

        public void UpdateSurvey(Survey survey)
        {
            _unitOfWork.GetRepository<Survey>().Update(survey);
        }

        public void Dispose()
        {
            _unitOfWork?.Dispose();
        }

        public Survey GetSurvey(int id)
        {
            using (IRepository<Survey> surveyRepository = _unitOfWork.GetRepository<Survey>())
            {
                return surveyRepository.Get(id);
            }
        }
    }
}