using System;

namespace AttendanceListGenerator.Core.Data
{
    public class Person : IPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Person(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
                throw new ArgumentNullException();

            FirstName = firstName;
            LastName = lastName;
        }

        public Person(string fullname)
        {
            if (fullname == null)
                throw new ArgumentNullException();

            string[] splittedFullname = fullname.Split(' ');

            string firstName = string.Empty;
            string lastName = string.Empty;

            firstName = splittedFullname[0];
            for (int i = 1; i < splittedFullname.Length; ++i)
            {
                // Add strings to fullname
                lastName += splittedFullname[i];

                // If it isn't last string then add space at the end of this string
                if (i < splittedFullname.Length - 1)
                    lastName += " ";
            }

            FirstName = firstName;
            LastName = lastName;
        }
    }
}