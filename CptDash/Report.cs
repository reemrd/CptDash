//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CptDash
{
    using System;
    using System.Collections.Generic;
    
    public partial class Report
    {
        public Report()
        {
            this.DailyPayments = new HashSet<DailyPayment>();
        }
    
        public int Id { get; set; }
        public int PeriodId { get; set; }
        public int EmployeeId { get; set; }
    
        public virtual ICollection<DailyPayment> DailyPayments { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Period Period { get; set; }
    }
}
