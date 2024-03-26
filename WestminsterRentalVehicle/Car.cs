using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem
{
    internal class Car : Vehicle
    {
        private string transmission;
        public Car(string regnum, string vmake, string vmodel, double vdailyRentalPrice) : base(regnum, vmake, vmodel, vdailyRentalPrice)
        {
            Console.WriteLine("Enter Car Transmission type (Manual) or (Automatic)");
            transmission = Console.ReadLine();
        }
        public Car() : base()
        {
            Console.WriteLine("Enter Car Transmission type (Manual) or (Automatic)");
            transmission = Console.ReadLine();
        }
        public override string GetVehicleInfo()
        {
            string result = base.GetVehicleInfo();
            result += $"\nTransmission: {transmission}\n";
            if (WestminsterRentalVehicle.GetUser() == "Admin")
            {
                result += $"Reservations: ";

                if (Bookings.Count != 0)
                {
                    foreach (Schedule schedule in Bookings)
                    {
                        result += schedule.GetScheduleInfo() + "\n";
                    }
                }
                else
                {
                    result += "No Bookings made for this vehicle\n";
                }
            }
            result += "------------------------------------------------";
            return result;
        }
    }
}
