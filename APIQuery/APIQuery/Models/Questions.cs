using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class Questions
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public string QuestionType { get; set; }
        public short QuestionOrder { get; set; }
        public string OptionsValue1 { get; set; }
        public string OptionsValue2 { get; set; }
        public string OptionsValue3 { get; set; }
        public string OptionsValue4 { get; set; }
        public string OptionsValue5 { get; set; }
    }

    public class Survey
    {
        public IList<Questions> ListOfQuestions { get; set; }
        public int UserId { get; set; }
        public string Feedback1 { get; set; }
        public string Feedback2 { get; set; }
        public string Feedback3 { get; set; }
        public string Feedback4 { get; set; }
        public string Feedback5 { get; set; }
    }
}