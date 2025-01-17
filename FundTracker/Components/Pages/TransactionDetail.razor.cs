

using FundTracker.Model;
using FundTracker.Services;

namespace FundTracker.Components.Pages
{
    public partial class TransactionDetail
    {
        private List<CustomTags> Tags { get; set; } = new();
        private string ErrorMessage { get; set; } = string.Empty;

        private string _search = string.Empty;
        
        private List<Transaction> Filtered = new();
        private Transaction transaction { get; set; } = new();
        private CustomTags tag{ get; set; } = new();

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
    
        private async Task FilteredTransaction()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(Search))
                {
                    Filtered = TransactionServices.GetAllTransactions();
                    ErrorMessage = string.Empty;
                    return;
                }
                Filtered = await TransactionServices.SearchTransaction(Search);

                if (!Filtered.Any())
                {
                    ErrorMessage = "No Match Users Found";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
        private string Search
        {
            get => _search;

            set
            {
                if (_search == value) return;
                _search = value;
                _ = OnSearchInput(_search);
            }
        }
        private async Task OnSearchInput(string search)
        {
            Search = search;
            FilteredTransaction();
            StateHasChanged();
        }
    }
}

