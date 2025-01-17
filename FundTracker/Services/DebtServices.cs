using FundTracker.Model;
using FundTracker.Model.Abstraction;
using FundTracker.Services.Interface;

namespace FundTracker.Services
{
    public class DebtServices : DebtBase,IDebtServices
    {
        private readonly List<Debt> _debts = new();
        private readonly ITransaction _transactionService;

        public DebtServices(ITransaction transactionService)
        {
            _transactionService = transactionService;
            _debts = GetAllDebts();
        }

        public Debt AddDebt(Debt debt)
        {
            var borrow = new Transaction
            {
                Id = Guid.NewGuid().ToString(),
                Title = $"Lender is{debt.Creditor}",
                Amount = debt.Amount,
                Date = DateTime.Now,
                Type = "Debt",
                tagId = Guid.NewGuid()
            };
            _transactionService.AddInflow(borrow);

            // Creating
            _debts.Add( new Debt
            {
                Id = Guid.NewGuid(),
                Title = debt.Title,
                Creditor = debt.Creditor,
                Amount = debt.Amount,
                takenDate = DateTime.Now,
                DueDate = debt.DueDate,
                TransactionId = borrow.Id
            });
            _debts.Add(debt);
            SaveDebt(_debts);
            return debt;
        }
        public List<Debt> Getdebts()
        {
            var tranDetails = GetAllDebts();
            if (tranDetails != null)
            {
                return tranDetails;
            }
            else
            {
                return new List<Debt>();
            }
        }
    }
}
