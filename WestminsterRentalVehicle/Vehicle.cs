using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem
{
    internal abstract class Vehicle : IComparable<Vehicle>
    {
        private string registrationNumber;
        private string make;
        private string model;
        private double dailyRentalPrice;
        protected List<Schedule> Bookings = new List<Schedule>();
        private static List<string> NumOfParkingSpaces = new List<string>();


        public Vehicle(string registrationNumber, string make, string model, double dailyRentalPrice)
        {
            this.registrationNumber = registrationNumber;
            this.make = make;
            this.model = model;
            this.dailyRentalPrice = dailyRentalPrice;
        }

        public Vehicle() 
        {
            bool vehicleExists = true;
            do
            { 
                Console.Write("Registration Number: ");
                registrationNumber = Console.ReadLine();
                if (NumOfParkingSpaces.Count() > 0)
                {
                    if (!NumOfParkingSpaces.Contains(registrationNumber))
                    {
                        vehicleExists = false;
                    }
                    else if (NumOfParkingSpaces.Contains(registrationNumber))
                    {
                        Console.WriteLine("A vehicle already exists with this registration number, please try again");
                        vehicleExists = true;
                    }
                }
                else
                {
                    vehicleExists = false;
                }

            }
            while (vehicleExists);

            Console.Write("Make: ");
            make = Console.ReadLine();
            Console.Write("Model: ");
            model = Console.ReadLine();
            Console.Write("Daily Rental Price: ");
            dailyRentalPrice = Double.Parse(Console.ReadLine());
        }

        public void Reserve(Schedule schedule) 
        {
            schedule.Reserve(dailyRentalPrice);
            Bookings.Add(schedule);
        }

        public bool AlterBooking(Schedule oldbooking, Schedule newbooking)
        {
            foreach (Schedule schedule in Bookings)
            {
                if (schedule.Equals(oldbooking) && this.Available(newbooking))
                {
                    Bookings.Remove(schedule);
                    this.Reserve(newbooking);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteBooking(Schedule booking)
        {
            foreach (Schedule schedule in Bookings)
            {
                if (schedule.Equals(booking))
                {
                    Bookings.Remove(schedule);
                    return true;
                }
            }
            return false;
        }

        public string GetRegNum() 
        { 
            return registrationNumber; 
        }

        public bool Available(Schedule schedule)
        {
            foreach (Schedule booking in Bookings) 
            {
                if (booking.Overlaps(schedule))
                {
                    return false;
                }
            }
            return true;
        }

        public static List<Vehicle> GetAvailableVehicles(List<Vehicle> parking, Schedule wantedSchedule, Type type)
        {
            List<Vehicle> availableVehicles = new List<Vehicle>();
            foreach (Vehicle vehicle in parking)
            {
                if (vehicle.GetType() == type && vehicle.Available(wantedSchedule))
                {
                    availableVehicles.Add(vehicle);
                }
            }
            return availableVehicles;
        }

        public virtual string GetVehicleInfo()
        {
            string result = $"------------------------------------------------\n" +
                $"Registration Number {this.GetRegNum()}\n" +
                $"Type: {this.GetType().Name}\n" +
                $"Make: {this.make}\n" +
                $"Model: {this.model}\n" +
                $"Daily Rental Price: ${this.dailyRentalPrice}";
            return result;
        }

        public int CompareTo(Vehicle other)
        {
            if (string.Compare(this.make, other.make, true) > 0)
            {
                return 1;
            }
            else if (string.Compare(this.make,other.make, true) < 0)
            {
                return -1;
            }
            else
            { 
                return 0; 
            }   
        }
        public static void UpdateSpaces(string regnum)
        {
            if (NumOfParkingSpaces.Contains(regnum))
            {
                NumOfParkingSpaces.Remove(regnum);
            }
            else
            {
                NumOfParkingSpaces.Add(regnum);
            }
        }
    }
}
