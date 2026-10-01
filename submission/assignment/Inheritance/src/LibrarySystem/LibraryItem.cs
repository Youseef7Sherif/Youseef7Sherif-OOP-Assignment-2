using System;

namespace LibrarySystem
{
    public class LibraryItem
    {
        public string CatalogNumber { get; }
        public string Title { get; }
        public decimal BaseLateFee { get; private set; }
        public bool IsWithdrawn { get; private set; }
        public bool IsOnLoan { get; private set; }
        private readonly int _loanPeriodDays;
        private readonly decimal _lateFeeMultiplier;

        protected LibraryItem(string catalogNumber, string title, decimal baseLateFee,
                              int loanPeriodDays, decimal lateFeeMultiplier)
        {
            if (string.IsNullOrWhiteSpace(catalogNumber))
                throw new ArgumentException("Catalog number cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be null or empty.");
            if (baseLateFee <= 0)
                throw new ArgumentException("Base late fee must be positive.");

            CatalogNumber = catalogNumber;
            Title = title;
            BaseLateFee = baseLateFee;
            _loanPeriodDays = loanPeriodDays;
            _lateFeeMultiplier = lateFeeMultiplier;
            IsWithdrawn = false;
            IsOnLoan = false;
        }

        public int GetLoanPeriodDays()
        {
            return _loanPeriodDays;
        }

        public decimal GetDailyLateFee()
        {
            return BaseLateFee * _lateFeeMultiplier;
        }

        public void Withdraw()
        {
            IsWithdrawn = true;
        }

        public void Restore()
        {
            IsWithdrawn = false;
        }

        public void ChangeBaseLateFee(decimal newFee)
        {
            if (newFee <= 0)
                throw new ArgumentException("Late fee must be positive.");

            BaseLateFee = newFee;
        }

        internal void MarkOnLoan()
        {
            IsOnLoan = true;
        }

        internal void MarkReturned()
        {
            IsOnLoan = false;
        }
    }
}