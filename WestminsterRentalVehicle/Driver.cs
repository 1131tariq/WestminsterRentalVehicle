using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem
{
    internal class Driver
    {
        private string DriverName;
        private string DriverSurname;
        private DateOnly DateOfBirth;
        private int LicenseNumber;

        public Driver()
        {
            Console.Write("Please Enter Driver First Name: ");
            DriverName = Console.ReadLine();
            Console.Write("Please Enter Driver Surname: ");
            DriverSurname = Console.ReadLine();

            bool correctvalue = false;
            do
            {
                Console.WriteLine("Enter Date of Birth: (YYYY-MM-DD)");
                string dob = Console.ReadLine();

                if (DateTime.TryParse(dob, out DateTime dateofbirth))
                {
                    DateOfBirth = new DateOnly(dateofbirth.Year, dateofbirth.Month, dateofbirth.Day);
                    correctvalue = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect date format!");
                }
            }
            while (!correctvalue);
            correctvalue = false;
            do
            {
                Console.Write("Please Enter Driver License number: ");
                try
                {
                    LicenseNumber = Int32.Parse(Console.ReadLine());
                    correctvalue = true;
                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Please input a valid license number, letters and symbols are not allowed");
                }
            }
            while (!correctvalue);
        }

        public string GetDriverInfo() 
        {
            string DriverInfo = $"| Driver Name: {DriverName} {DriverSurname} " +
               $"| License Number {LicenseNumber} | Date of Birth {DateOfBirth.ToString()}";
            return DriverInfo;
        }
    }
}
