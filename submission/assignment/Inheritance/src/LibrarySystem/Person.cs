using System;

namespace LibrarySystem
{
    public class Person
    {
        public int PersonId { get; }
        public string FullName { get; }
        public string PhoneNumber { get; }

        protected Person(int personId, string fullName, string phoneNumber)
        {
            if (personId <= 0)
                throw new ArgumentException("Person ID must be a positive number.");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Full name cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number cannot be null or empty.");

            PersonId = personId;
            FullName = fullName;
            PhoneNumber = phoneNumber;
        }
    }
}