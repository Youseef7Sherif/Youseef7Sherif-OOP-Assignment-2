using System;

namespace LibrarySystem
{
    public class Librarian : Staff
    {
        public Librarian(int personId, string fullName, string phoneNumber,
                         DateTime hireDate, decimal monthlySalary)
            : base(personId, fullName, phoneNumber, hireDate, monthlySalary, 0m)
        {
        }

        public void ReturnLoan(Loan loan, DateTime? returnDate = null)
        {
            loan.Return(returnDate);
        }

        public void MarkAsLost(Loan loan)
        {
            loan.MarkAsLost();
        }
    }
}