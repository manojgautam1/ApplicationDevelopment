using FundTracker.Model;

namespace FundTracker.Services.Interface
{
    public interface IDebtServices
    {
        Debt AddDebt(Debt debt);

        List<Debt> Getdebts();
    }
}