using FundTracker.Model;

namespace FundTracker.Components.Pages
{
    public partial class AddingTransactions
    {
        private List<CustomTags> Tags { get; set; } = new();
        private string ErrorMessage { get; set; } = string.Empty;

        private string _search = string.Empty;

        private List<Transaction> Filtered = new();
        private Transaction transaction { get; set; } = new();
        private CustomTags tag { get; set; } = new();

        protected override void OnInitialized()
        {
            Filtered = TransactionServices.GetAllTransactions();
            Tags = TransactionServices.GetTags();
        }
        private void AddIncome()
        {
            if (TransactionServices.AddInflow(transaction))
            {
                Nav.NavigateTo("/home");
            }
            else
            {
                ErrorMessage = "Data not inserted";
            }
        }
        private void AddTag()
        {

            if (TransactionServices.AddTags(tag))
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