using System;
using System.Collections.Generic;
using System.Text;
using CoreQuizz.Queries.Contract;
using CoreQuizz.Queries.PageQueries.Responces;

namespace CoreQuizz.Queries.PageQueries.Queries
{
    public class SurveySearchPageQuery: IQuery<SurveyListItem[]>
    {
        public string SearchText { get; set; }
        public int PageNumber { get; set; }
        public int PageCount { get; set; }
    }
}
