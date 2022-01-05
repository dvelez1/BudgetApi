using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetApi.Models
{
    public partial class MasExpense
    {
        //public MasExpense()
        //{
        //    MonthlyExpenses = new HashSet<MonthlyExpense>();
        //}

        public int MasExpensesId { get; set; }
        public string Description { get; set; }
        public int BiweeklyNumber { get; set; }
        public decimal Budget { get; set; }

        //public virtual ICollection<MonthlyExpense> MonthlyExpenses { get; set; }
    }
}
