using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem
{
    internal class Schedule : IOverlappable
    {
        private DateOnly PickUpDate;
        private DateOnly DropOffDate;
        Driver Driver;
        private double TotalPrice;

        public Schedule()
        {
            DateOnly todayDateOnly = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            bool correctvalue = false;
            do
            {
                Console.WriteLine("Enter Pickup Date: (YYYY-MM-DD)");
                string pickUpDate = Console.ReadLine();

                Console.WriteLine("Enter Dropoff Date: (YYYY-MM-DD)");
                string dropOffDate = Console.ReadLine();

                if (DateTime.TryParse(pickUpDate, out DateTime pickup) && DateTime.TryParse(dropOffDate, out DateTime dropoff))
                {
                    DateOnly startDate = new DateOnly(pickup.Year, pickup.Month, pickup.Day);
                    DateOnly endDate = new DateOnly(dropoff.Year, dropoff.Month, dropoff.Day);
                    if (startDate.CompareTo(endDate) < 0)
                    {
                        PickUpDate = startDate;
                        DropOffDate = endDate;
                        correctvalue = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Pick up date cant be after drop off date");
                    }

                    if (startDate.CompareTo(todayDateOnly) < 0)
                    {
                        Console.Clear();
                        correctvalue = false;
                        Console.WriteLine("Pickup date cant be in the past.");
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect date format!");
                }
            }
            while (!correctvalue);
        }

        public void Reserve(double dailyRentalPrice)
        {
            Driver = new Driver();
            TotalPrice = dailyRentalPrice * (DropOffDate.DayNumber - PickUpDate.DayNumber);
        }

        public bool Overlaps(Schedule other)
        {
            if (this.PickUpDate < other.DropOffDate && this.DropOffDate > other.PickUpDate || other.PickUpDate < this.DropOffDate && other.DropOffDate > this.PickUpDate)
            {
                return true;
            }
            return false;
        }

        public string GetScheduleInfo()
        {
            string scheduleInfo = $"From: {PickUpDate} To: {DropOffDate}";
             
            if (Driver != null) 
            {
                string DriverInfo = Driver.GetDriverInfo();
                scheduleInfo += $" {DriverInfo} | Total Price: ${TotalPrice}";
            }
            return scheduleInfo;
        }

        public bool Equals(Schedule other)
        {
            if (this.PickUpDate == other.PickUpDate && this.DropOffDate == other.DropOffDate)
            {
                return true;
            }
            return false;
        }
    }
}
