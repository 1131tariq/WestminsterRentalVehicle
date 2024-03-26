using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleRentalSoftwareSystem
{
    internal class Motorbike : Vehicle
    {
        private int displacement;

        public Motorbike(string regnum, string vmake, string vmodel, double vdailyRentalPrice) : base(regnum, vmake, vmodel, vdailyRentalPrice)
        {
            Console.WriteLine("Enter engine displacement in cc(s):");
            displacement = Int32.Parse(Console.ReadLine());
        }
        public Motorbike() : base()
        {
            Console.WriteLine("Enter engine displacement in cc(s):");
            displacement = Int32.Parse(Console.ReadLine());
        }


        public override string GetVehicleInfo()
        {
            string result = base.GetVehicleInfo();
            result += $"\ndisplacement: {displacement}cc\n";
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
