using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CptDash.Models
{
    public class ReportSearchViewModel
    {
        public List<Period> Periods { get; set; }
        public List<Employee> Employees { get; set; }
    }
}