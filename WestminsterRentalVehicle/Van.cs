using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem
{
    internal class Van : Vehicle
    {
        private string payload;
        public Van(string regnum, string vmake, string vmodel, double vdailyRentalPrice) : base(regnum, vmake, vmodel, vdailyRentalPrice)
        {
            Console.WriteLine("Enter Van Storage Capacity (small) (medium) or (large)");
            payload = Console.ReadLine();
        }
        public Van() : base()
        {
            Console.WriteLine("Enter Van Storage Capacity (small) (medium) or (large)");
            payload = Console.ReadLine();
        }

        public override string GetVehicleInfo()
        {
            string result = base.GetVehicleInfo();
            result += $"\nStorage Capacity: {payload}\n";
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
