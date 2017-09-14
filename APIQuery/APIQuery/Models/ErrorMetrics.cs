using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIQuery.Models
{
    public class ErrorMetrics
    {
        public decimal MeanAbsoluteError { get; set; }
        public decimal MeanSquareError { get; set; }
        public decimal RootMeanSquareError { get; set; }
    }
}