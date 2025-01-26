using FundTracker.Model;

namespace FundTracker.Services.Interface
{
    public interface IDebtServices
    {
        List<Debt> Getdebts();
        bool AddDebt(Debt debt);
    }
}