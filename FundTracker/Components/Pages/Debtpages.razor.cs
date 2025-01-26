using FundTracker.Model;

namespace FundTracker.Components.Pages
{
    public partial class Debtpages
    {
        private Debt debt { get; set; } = new();
        private string ErrorMessage { get; set; } = string.Empty;

        private List<Debt> Debts { get; set; } = new();

        protected override void OnInitialized()
        {
            Debts= DebtServices.Getdebts();
        }

        private void AddDebt()
        {
            if (debt.Amount < 0)
            {
                ErrorMessage = "Debt amount cannot be negative.";
                return; // Stop execution if the debt amount is invalid
            }

            if (DebtServices.AddDebt(debt))
            {
                Nav.NavigateTo("/home");
            }
            else
            {
                ErrorMessage = "Data not inserted";
            }
        }
    }
}