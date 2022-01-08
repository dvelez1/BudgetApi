using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BudgetApi.ViewModels
{
    public class MonthlyExpenseViewModel
    {
        public int MontlyExpensesId { get; set; }
        public int MasMonthlyExpensesId { get; set; }
        public int MasExpensesId { get; set; }
        public string MasExpensesDescription { get; set; }
        public decimal Budget { get; set; }
        public decimal Payment { get; set; }
    }
}
