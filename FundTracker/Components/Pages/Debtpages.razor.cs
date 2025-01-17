using FundTracker.Model;

namespace FundTracker.Components.Pages
{
    public partial class Debtpages
    {
        private Debt debt { get; set; } = new();

        private List<Debt> Debts { get; set; } = new();

        protected override void OnInitialized()
        {
            Debts= DebtServices.Getdebts();
        }

        private void AddDebt()
        {
            DebtServices.AddDebt(debt);
        }
    }
}