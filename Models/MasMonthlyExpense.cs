using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetApi.Models
{
    public partial class MasMonthlyExpense
    {
        //public MasMonthlyExpense()
        //{
        //    ManualMonthlyExpenses = new HashSet<ManualMonthlyExpense>();
        //    MonthlyExpenses = new HashSet<MonthlyExpense>();
        //}

        public int MasMonthlyExpensesId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Income { get; set; }
        public int BiweeklyNumber { get; set; }

        //public virtual ICollection<ManualMonthlyExpense> ManualMonthlyExpenses { get; set; }
        //public virtual ICollection<MonthlyExpense> MonthlyExpenses { get; set; }
    }
}
