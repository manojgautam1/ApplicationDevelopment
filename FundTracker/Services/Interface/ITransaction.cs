﻿using FundTracker.Model;

namespace FundTracker.Services.Interface
{
    public interface ITransaction
    {
        List<Transaction> GetAllTransactions();
        CustomTags GettagsById(Guid id);
        List<CustomTags> GetTags();
        bool AddTags(CustomTags ctags);
        bool AddInflow(Transaction transaction);

        Task<List<Transaction>> SearchTransaction(string searchName);

        int TotalTransactionValue();
        int GetTotalTransaction();
        int GetLowestInflow();
        int GetHighestInflow();

        int Highestoutflow();
        int LowestOutflow();
    }
}