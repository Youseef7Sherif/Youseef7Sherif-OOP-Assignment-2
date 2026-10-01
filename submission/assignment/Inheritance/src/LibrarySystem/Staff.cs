using System;

namespace LibrarySystem
{
    public class Staff : Person
    {
        protected DateTime HireDate { get; }
        public decimal MonthlySalary { get; private set; }
        private readonly decimal _allowance;

        protected Staff(int personId, string fullName, string phoneNumber,
                        DateTime hireDate, decimal monthlySalary, decimal allowance)
            : base(personId, fullName, phoneNumber)
        {
            if (monthlySalary <= 0)
                throw new ArgumentException("Monthly salary must be positive.");

            HireDate = hireDate;
            MonthlySalary = monthlySalary;
            _allowance = allowance;
        }

        public decimal GetMonthlyPay()
        {
            return MonthlySalary + _allowance;
        }

        public void GiveRaise(decimal percentage)
        {
            if (percentage <= 0)
                throw new ArgumentException("Raise percentage must be positive.");

            MonthlySalary += MonthlySalary * (percentage / 100m);
        }
    }
}