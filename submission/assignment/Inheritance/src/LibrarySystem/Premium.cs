namespace LibrarySystem
{
    public class Premium : Member
    {
        public Premium(int personId, string fullName, string phoneNumber, decimal lateFeeDiscount)
            : base(personId, fullName, phoneNumber, 10, lateFeeDiscount)
        {
        }

        public int ReadingPoints
        {
            get
            {
                int returnedCount = 0;
                foreach (Loan loan in Loans)
                {
                    if (loan.Status == LoanStatus.Returned)
                        returnedCount++;
                }

                return returnedCount * 5;
            }
        }
    }
}