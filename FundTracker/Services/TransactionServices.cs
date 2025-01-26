﻿
using FundTracker.Services;
using FundTracker.Model;
using FundTracker.Model.Abstraction;
using FundTracker.Services.Interface;


namespace FundTracker.Services
{
    public class TransactionServices : TransactionServicesBase, ITransaction
    {
        //declaration
        private List<Transaction> _Transaction;
        private List<CustomTags> Ctags;
        private readonly IUser _userService;
        private DateTime StartDate { get; set; } = DateTime.Now.AddDays(-30);
        private DateTime EndDate { get; set; } = DateTime.Now.AddDays(30);

        public int GetTotalTransaction()
        {
        // Fetch all transactions from the data source
        var transactions = GetAllDetails();

        // Sum the transaction amounts
        var TotalTransactions = transactions.Count;

        return TotalTransactions;
        }
        public int GetLowestInflow()
        {
            var transactions = GetAllDetails();
            // Get the inflow transactions
            var inflows = transactions.Where(t => t.Type == "Inflow").Select(t => t.Amount).ToList();

            // Get the lowest inflow amount or 0 if there are no inflows
            int lowestInflow = inflows.Any() ? inflows.Min() : 0;

            // Convert the lowest inflow to an int (assuming you want to round down)
            return lowestInflow;
        }

        public int GetHighestInflow()
        {
            var transactions = GetAllDetails();
            
            var inflows = transactions.Where(t => t.Type == "Inflow").Select(t => t.Amount).ToList();

            
            int highestInflow = inflows.Any() ? inflows.Max() : 0;

            
            return highestInflow;
        }
        public int TotalTransactionValue()
        {
            var transactions = GetAllDetails();
            
            var totalcount = transactions.Sum(t=> t.Amount);

            
            return totalcount;

        }
        public int Highestoutflow()
        {
            var transactions = GetAllDetails();
            
            var outflows = transactions.Where(t => t.Type == "Outflow").Select(t => t.Amount).ToList();

            return outflows.Max();
        }

        public int LowestOutflow()
        {
            var transactions = GetAllDetails();

            var outflows = transactions.Where(t => t.Type == "Outflow").Select(t => t.Amount).ToList();

            return outflows.Min();
        }

        //transaction services
        public TransactionServices(IUser userservice)
        {
            _Transaction = GetAllDetails();
            _userService = userservice;
        }

        

        public List<CustomTags> GetTags()
        {
            var tagDetails = Gettags();
            if (tagDetails != null)
            {
                return tagDetails;
            }
            else
            {
                return new List<CustomTags>();
            }
        }
        public bool AddTags(CustomTags ctags)
        {
            Ctags = Gettags();
            Ctags.Add(new CustomTags
            {
                Name = ctags.Name,
            });
            SaveTags(Ctags);
            return true;
        }
        public CustomTags GettagsById(Guid id)
        {
            Ctags = Gettags();
            return Ctags.FirstOrDefault(x => x.Id == id);
        }
        

        
        public bool AddInflow(Transaction transaction)
        {
            
            if (transaction.Type.ToLower() == "inflow" || transaction.Type.ToLower() == "debt")
            {
                int balance = _userService.getBalanceamt();
                int Currentbalance = balance + transaction.Amount;
                _userService.updateBalanceAmt(Currentbalance);
            }
            else if (transaction.Type.ToLower() == "outflow" || transaction.Type.ToLower() == "debt paid")
            {
                int balance = _userService.getBalanceamt();
                int Currentbalance = balance - transaction.Amount;
                _userService.updateBalanceAmt(Currentbalance);
            }
            _Transaction.Add(new Transaction
            {
                Title = transaction.Title,
                Amount = transaction.Amount,
                Type = transaction.Type,
                Date = transaction.Date,
                tagId = transaction.tagId,
            });
            SaveTransaction(_Transaction);
            return true;
        }
        public List<Transaction> GetAllTransactions()
        {
            var tranDetails = GetAllDetails();
            if (tranDetails != null)
            {
                return tranDetails;
            }
            else
            {
                return new List<Transaction>();
            }
        }

        public async Task <List<Transaction>> SearchTransaction(string searchName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchName))
                    throw new ArgumentNullException("Search Cannot be Null or Empty", nameof(searchName));

                return await Task.FromResult(
                    _Transaction
                    .Where(t => t.Date >= StartDate && t.Date <= EndDate)
                    .Where(t => t.Title.Contains(searchName, StringComparison.OrdinalIgnoreCase)).ToList());

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while searching for the Transaction.", ex);
            }
        }
        
    }
}
