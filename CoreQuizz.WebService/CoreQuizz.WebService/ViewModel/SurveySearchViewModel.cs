using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreQuizz.WebService.ViewModel
{
    public class SurveySearchViewModel
    {
        public string SearchText { get; set; }

        public int ItemsOnPage { get; set; }

        public int PageNumber { get; set; }

        public bool IsIncludeCount { get; set; } = false;
    }
}
