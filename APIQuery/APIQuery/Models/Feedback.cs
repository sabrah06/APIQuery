using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class Feedback
    {
        public int UserId { get; set; }
        public int QuestionId { get; set; }
        public bool YesNo { get; set; }
        public string Response { get; set; }
        public string FeedbackText { get; set; }
    }
}