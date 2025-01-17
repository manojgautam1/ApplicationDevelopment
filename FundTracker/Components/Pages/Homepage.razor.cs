


using FundTracker.Model;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace FundTracker.Components.Pages
{
    public partial class Homepage
    {
        private List<Transaction> DisplayT = new();

        private List<string> TransactionLabels = new();
        private List<double> TransactionValues = new();
        private List<ChartSeries> series = new();
        private int LowestInflow;
        private int HighestInflow;
        private int totaltransaction;
        private int LowOutflow;
        private int HighOutflow;
        private string ErrorMessage { get; set; } = string.Empty;
        protected override void OnInitialized()
        {
            DisplayT = TransactionServices.GetAllTransactions();
            LowestInflow = TransactionServices.GetLowestInflow();
            HighestInflow = TransactionServices.GetHighestInflow();
            LowOutflow = TransactionServices.LowestOutflow();
            HighOutflow = TransactionServices.Highestoutflow();
            totaltransaction = TransactionServices.GetTotalTransaction();
            // Ensuring transactions before sorting values
            if (DisplayT.Any())
            {
                var topTransactions = DisplayT.OrderByDescending(t => t.Amount).Take(5).ToList();

                TransactionLabels = topTransactions.Select(t => t.Title).ToList();
                TransactionValues = topTransactions.Select(t => (double)t.Amount).ToList();

                series = new List<ChartSeries>
                {
                    new ChartSeries
                    {
                        Name = "Transaction Amount",
                        Data = TransactionValues.ToArray(),
                    }
                };
            }
        }
        [CascadingParameter]
        private UserState _LiveState { get; set; }

        private int Balanceamt() 
        {
            int Balance = UserService.getBalanceamt();
            return Balance;
        }
    }
}
