namespace LibrarySystem
{
    public class Student : Member
    {
        public Student(int personId, string fullName, string phoneNumber)
            : base(personId, fullName, phoneNumber, 3, 0m)
        {
        }
    }
}