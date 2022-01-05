using System;
using System.Collections.Generic;

#nullable disable

namespace BudgetApi.Models
{
    public partial class ManualMonthlyExpense
    {
        public int ManualMonthlyExpensesId { get; set; }
        public string Description { get; set; }
        public int MasMonthlyExpensesId { get; set; }
        public decimal Budget { get; set; }
        public decimal Payment { get; set; }

        //public virtual MasMonthlyExpense MasMonthlyExpenses { get; set; }
    }
}
