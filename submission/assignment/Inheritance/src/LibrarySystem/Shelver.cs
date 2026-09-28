using System;

namespace LibrarySystem
{
    public class Shelver : Staff
    {
        public string Section { get; private set; }

        public Shelver(int personId, string fullName, string phoneNumber,
                       DateTime hireDate, decimal monthlySalary, string section)
            : base(personId, fullName, phoneNumber, hireDate, monthlySalary, 0m)
        {
            if (string.IsNullOrWhiteSpace(section))
                throw new ArgumentException("Section cannot be null or empty.");

            Section = section;
        }

        public void Reassign(string newSection)
        {
            if (string.IsNullOrWhiteSpace(newSection))
                throw new ArgumentException("Section cannot be null or empty.");

            Section = newSection;
        }
    }
}