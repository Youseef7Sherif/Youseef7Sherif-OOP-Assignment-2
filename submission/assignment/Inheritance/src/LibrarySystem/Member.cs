using System;
using System.Collections.Generic;

namespace LibrarySystem
{
    public class Member : Person
    {
        private readonly List<Loan> _loans;
        private readonly int _maxLoans;
        private readonly decimal _discountPercentage;

        public IReadOnlyList<Loan> Loans => _loans;

        protected Member(int personId, string fullName, string phoneNumber,
                         int maxLoans, decimal discountPercentage)
            : base(personId, fullName, phoneNumber)
        {
            _loans = new List<Loan>();
            _maxLoans = maxLoans;
            _discountPercentage = discountPercentage;
        }

        public decimal GetDiscountPercentage()
        {
            return _discountPercentage;
        }

        public Loan Borrow(LibraryItem item)
        {
            if (item.IsWithdrawn)
                throw new InvalidOperationException("This item has been withdrawn from circulation.");
            if (item.IsOnLoan)
                throw new InvalidOperationException("This item is already on loan.");

            int activeLoans = 0;
            foreach (Loan existingLoan in _loans)
            {
                if (existingLoan.Status == LoanStatus.Borrowed)
                    activeLoans++;
            }

            if (activeLoans >= _maxLoans)
                throw new InvalidOperationException("You have reached your maximum number of active loans.");

            Loan loan = new Loan(this, item, DateTime.Now);
            _loans.Add(loan);
            item.MarkOnLoan();

            return loan;
        }
    }
}