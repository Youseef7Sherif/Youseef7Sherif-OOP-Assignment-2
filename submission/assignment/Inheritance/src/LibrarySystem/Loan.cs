using System;

namespace LibrarySystem
{
    public class Loan
    {
        private static int _nextLoanId = 1;

        public int LoanId { get; }
        public DateTime BorrowDate { get; }
        public Member Member { get; }
        public LibraryItem LibraryItem { get; }
        public LoanStatus Status { get; private set; }
        public DateTime? ReturnDate { get; private set; }

        public DateTime DueDate => BorrowDate.AddDays(LibraryItem.GetLoanPeriodDays());

        public decimal LateFee
        {
            get
            {
                if (ReturnDate == null)
                    return 0m;

                int lateDays = (ReturnDate.Value.Date - DueDate.Date).Days;
                if (lateDays <= 0)
                    return 0m;

                decimal rawFee = lateDays * LibraryItem.GetDailyLateFee();
                decimal discount = Member.GetDiscountPercentage();
                decimal discountedFee = rawFee - (rawFee * (discount / 100m));

                return discountedFee < 0m ? 0m : discountedFee;
            }
        }

        internal Loan(Member member, LibraryItem item, DateTime borrowDate)
        {
            LoanId = _nextLoanId++;
            BorrowDate = borrowDate;
            Member = member;
            LibraryItem = item;
            Status = LoanStatus.Borrowed;
        }

        public void Return(DateTime? returnDate = null)
        {
            DateTime actualReturnDate = returnDate ?? DateTime.Now;

            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException("Only a borrowed loan can be returned.");
            if (actualReturnDate.Date < BorrowDate.Date)
                throw new ArgumentException("Return date cannot be earlier than the borrow date.");

            ReturnDate = actualReturnDate;
            Status = LoanStatus.Returned;
            LibraryItem.MarkReturned();
        }

        public void MarkAsLost()
        {
            if (Status != LoanStatus.Borrowed)
                throw new InvalidOperationException("Only a borrowed loan can be marked as lost.");

            Status = LoanStatus.Lost;
            LibraryItem.MarkReturned();
        }
    }
}