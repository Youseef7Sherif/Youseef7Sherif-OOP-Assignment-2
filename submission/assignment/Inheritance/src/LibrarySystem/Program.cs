namespace LibrarySystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Library System Demo ===\n");

            // --- must NOT compile: cannot create a "plain" parent directly ---
            // Person p = new Person(1, "Test", "0100000000");
            // Member m = new Member(1, "Test", "0100000000", 5, 0);
            // Staff s = new Staff(1, "Test", "0100000000", DateTime.Now, 5000, 0);
            // LibraryItem li = new LibraryItem("C1", "Test", 5, 10, 1);

            Student student = new Student(1, "Ahmed Ali", "01000000001");
            Premium premium = new Premium(2, "Sara Hassan", "01000000002", 10m);

            // --- must NOT compile: identity fields are get-only ---
            // student.FullName = "New Name";

            // --- must NOT compile: Loans is exposed only as IReadOnlyList<Loan> ---
            // student.Loans.Add(someLoan);

            Book book = new Book("B-001", "Clean Code", 2m);
            DVD dvd = new DVD("D-001", "Inception", 3m);
            Magazine magazine = new Magazine("M-001", "National Geographic", 1m);

            // --- must NOT compile: IsOnLoan can only change through Borrow/Return ---
            // book.IsOnLoan = true;

            Console.WriteLine("--- Borrowing a withdrawn item ---");
            book.Withdraw();
            try
            {
                student.Borrow(book);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected as expected: {ex.Message}");
            }
            book.Restore();

            Console.WriteLine("\n--- Borrowing an item already on loan ---");
            student.Borrow(book);
            try
            {
                premium.Borrow(book);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected as expected: {ex.Message}");
            }

            Console.WriteLine("\n--- Student member borrowing a 4th item (max is 3) ---");
            Book book2 = new Book("B-002", "The Pragmatic Programmer", 2m);
            Book book3 = new Book("B-003", "Design Patterns", 2m);
            Book book4 = new Book("B-004", "Refactoring", 2m);
            student.Borrow(book2);
            student.Borrow(book3);
            try
            {
                student.Borrow(book4);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected as expected: {ex.Message}");
            }

            Console.WriteLine("\n--- Staff monthly pay, one List<Staff> ---");
            Librarian librarian = new Librarian(10, "Omar Said", "01111111111", new DateTime(2020, 1, 1), 6000m);
            Shelver shelver = new Shelver(11, "Nour Adel", "01222222222", new DateTime(2021, 5, 1), 4500m, "Fiction");
            HeadLibrarian headLibrarian = new HeadLibrarian(12, "Mona Khaled", "01333333333", new DateTime(2018, 3, 1), 9000m);

            List<Staff> staffList = new List<Staff> { librarian, shelver, headLibrarian };
            foreach (Staff staff in staffList)
            {
                Console.WriteLine($"{staff.FullName}: {staff.GetMonthlyPay():C}");
            }

            Console.WriteLine("\n--- Item loan period & daily late fee, one List<LibraryItem> ---");
            List<LibraryItem> items = new List<LibraryItem> { book, dvd, magazine };
            foreach (LibraryItem item in items)
            {
                Console.WriteLine($"{item.Title}: {item.GetLoanPeriodDays()} days, late fee/day = {item.GetDailyLateFee():C}");
            }

            Console.WriteLine("\n--- Premium member returns a DVD 5 days late ---");
            Loan dvdLoan = premium.Borrow(dvd);
            DateTime lateReturnDate = dvdLoan.DueDate.AddDays(5);
            dvdLoan.Return(lateReturnDate);
            Console.WriteLine($"Due date: {dvdLoan.DueDate:d}");
            Console.WriteLine($"Late fee: {dvdLoan.LateFee:C}");
            Console.WriteLine($"Premium reading points: {premium.ReadingPoints}");

            Console.WriteLine("\n--- Returning the same loan twice ---");
            try
            {
                dvdLoan.Return(lateReturnDate);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected as expected: {ex.Message}");
            }

            Console.WriteLine("\n--- Marking a returned loan as lost ---");
            try
            {
                dvdLoan.MarkAsLost();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Rejected as expected: {ex.Message}");
            }

            Console.WriteLine("\n=== Demo complete ===");
        }
    }
}
