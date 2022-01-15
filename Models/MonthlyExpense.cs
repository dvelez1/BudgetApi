using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetApi.Models
{
    public partial class MonthlyExpense
    {
        public int MontlyExpensesId { get; set; }
        public int MasExpensesId { get; set; }
        public int MasMonthlyExpensesId { get; set; }
        public decimal Payment { get; set; }
        public decimal Budget { get; set; }

        //public virtual MasExpense MasExpenses { get; set; }
        //public virtual MasMonthlyExpense MasMonthlyExpenses { get; set; }
    }
}
