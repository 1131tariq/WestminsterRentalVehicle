using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace VehicleRentalSoftwareSystem
{
    internal class WestminsterRentalVehicle : IRentalCustomer, IRentalManager
    {
        private List<Vehicle> parking = new List<Vehicle>();
        private static string currentUser;

        public static string GetUser()
        {
            return currentUser;
        }

        public WestminsterRentalVehicle()
        {
            
            AddVehicle(new Car("1234", "Ford", "Escort", 14.99));
            AddVehicle(new Car("12345", "Saab", "Nine-Five", 19.99));
            AddVehicle(new Car("123456", "BMW", "5 Series", 29.99));
            AddVehicle(new ElectricCar("5636", "Tesla", "Model X", 59.99));
            AddVehicle(new Motorbike("2992", "Yamaha", "R1", 149.99));
            AddVehicle(new Van("9881", "Citroen", "Berlingo", 34.99));
            
            List<string> customerOptions = new List<string> {
                "1: List Available Vehicles",
                "2: Make Reservation",
                "3: Change Reservation",
                "4: Delete Reservation",
                "5: Switch User",
                "6: Exit"
            };

            List<string> adminOptions = new List<string> {
                "1: Add Vehicle to Parking",
                "2: Delete Vehicle from Parking",
                "3: List All Parked Vehicles",
                "4: List All Parked Vehicles (ordered)",
                "5: Generate Report",
                "6: Switch User",
                "7: Exit"
            };

            List<string> islogged = customerOptions;
            currentUser = "Customer";
            bool sysOn = true;

            while (sysOn)
            {
                Console.Clear();

                Console.WriteLine("\r\n __          __       _             _           _               \r\n \\ \\        / /      | |           (_)         | |              \r\n  \\ \\  /\\  / /__  ___| |_ _ __ ___  _ _ __  ___| |_ ___ _ __    \r\n   \\ \\/  \\/ / _ \\/ __| __| '_ ` _ \\| | '_ \\/ __| __/ _ \\ '__|   \r\n    \\  /\\  /  __/\\__ \\ |_| | | | | | | | | \\__ \\ ||  __/ |      \r\n   __\\/_ \\/ \\___||___/\\__|_| |_| |_|_|_| |_|___/\\__\\___|_|      \r\n  / ____|           |  __ \\          | |      | |  / ____|      \r\n | |     __ _ _ __  | |__) |___ _ __ | |_ __ _| | | |     ___   \r\n | |    / _` | '__| |  _  // _ \\ '_ \\| __/ _` | | | |    / _ \\  \r\n | |___| (_| | |    | | \\ \\  __/ | | | || (_| | | | |___| (_) | \r\n  \\_____\\__,_|_|    |_|  \\_\\___|_| |_|\\__\\__,_|_|  \\_____\\___(_)\r\n                                                                \r\n                                                                \r\n - Verson 1.0");

                Console.WriteLine(
                    $"------------------------------------------------\n" +
                    $"Logged in as {currentUser}\n" +
                    $"------------------------------------------------");

                Console.WriteLine("Please select one of the following options\n" +
                    "------------------------------------------------");

                foreach (string option in islogged)
                {
                    Console.WriteLine(option);
                }

                Console.WriteLine("------------------------------------------------");
                Console.Write("Input: ");
                string input = Console.ReadLine();

                Console.Clear();

                if (currentUser == "Customer")
                {
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine($"Availabe Vehicles to Book");
                           
                            if (parking.Count() > 0)
                            {
                                Console.WriteLine($"------------------------------------------------\nPlease Choose the vehicle type from below list:");

                                List<Type> types = new List<Type> { System.Type.GetType("VehicleRentalSoftwareSystem.Car"),
                                    System.Type.GetType("VehicleRentalSoftwareSystem.ElectricCar"),
                                    System.Type.GetType("VehicleRentalSoftwareSystem.Motorbike"),
                                    System.Type.GetType("VehicleRentalSoftwareSystem.Van")
                                };
                                
                                for (int i = 0; i < types.Count(); i++)
                                {
                                    Console.WriteLine($"{i + 1} {types[i].Name}");
                                }
                                int typeChosen = 0;

                                bool correctInput = false;
                                do
                                {
                                    try
                                    {
                                        typeChosen = Int32.Parse(Console.ReadLine());
                                        if (typeChosen >= 1 && typeChosen <= types.Count())
                                        {
                                            correctInput = true;
                                        }
                                        else
                                        {
                                            Console.WriteLine($"No Type corresponds to the selection provided, please input a valid number between 1 & {types.Count()}");
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        Console.WriteLine($"Please input a valid number between 1 & {types.Count()}");
                                    }
                                }
                                while (!correctInput);
                                

                                Console.Clear();

                                ListAvailableVehicles(new Schedule(), types[typeChosen - 1]);
                            }
                            else
                            {
                                Console.WriteLine("There are no vehicles available at the moment, please check with you sales representative for availability");
                            }

                            BackToMainMenu();

                            break;

                        case "2":
                            Console.WriteLine($"Make a Reservation\n------------------------------------------------");

                            if (parking.Count() > 0)
                            {
                                Console.WriteLine("Enter the registration number for the vehicle you wish to book");

                                string RegNum = Console.ReadLine();

                                bool carinParking = false;
                                foreach (Vehicle v in parking)
                                {
                                    if (v.GetRegNum() == RegNum)
                                    {
                                        carinParking |= true;
                                    }
                                }

                                if (carinParking)
                                {
                                    AddReservation(RegNum, new Schedule());
                                }
                                else
                                {
                                    Console.WriteLine("The Registration Number entered does not exist");
                                }
                                
                            }
                            else
                            {
                                Console.WriteLine("There are no vehicles available at the moment, please check with you sales representative for availability");
                            }

                            BackToMainMenu();

                            break;

                        case "3":
                            Console.WriteLine($"Change a Reservation\n------------------------------------------------");
                            
                            bool VehicleNotFound = true;
                            
                            if (parking.Count() > 0)
                            {
                                Console.WriteLine("Enter the registration number for the vehicle you wish to amend the booking for");
                                string vehicleRegistrationNumber = Console.ReadLine();
                                foreach (Vehicle v in parking)
                                {
                                    if (v.GetRegNum().Equals(vehicleRegistrationNumber))
                                    {
                                        VehicleNotFound = false;
                                        Console.WriteLine("Please enter the details of the schedule you want to amend");
                                        Schedule oldSchedule = new Schedule();

                                        if (!v.Available(oldSchedule))
                                        {
                                            Console.WriteLine("Please enter the details of the update reservation");
                                            Schedule newSchedule = new Schedule();

                                            ChangeReservation(vehicleRegistrationNumber, oldSchedule, newSchedule);
                                        }
                                        else
                                        {
                                            Console.WriteLine("The dates entered do not seem to be reserved in order for it to be ameneded. Consider making a reservation instead.");
                                        }
                                        break;
                                    }
                                    else
                                    {
                                        VehicleNotFound = true;
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("There are no available bookings to be amended at the moment. Please make a reservation");
                            }

                            if (VehicleNotFound && parking.Count() > 0)
                            {
                                Console.WriteLine("There are no vehicles available holding the registration number entered");
                            }

                            BackToMainMenu();

                            break;

                        case "4":
                            Console.WriteLine($"Cancel a Reservation\n------------------------------------------------");

                            if (parking.Count() > 0)
                            {
                                Console.WriteLine("Enter the registration number for the vehicle you wish to cancel the booking for");

                                DeleteReservation(Console.ReadLine(), new Schedule());
                            }
                            else
                            {
                                Console.WriteLine("There are no available bookings to be cancelled at the moment, Please check with your sales representative for available vehicles");
                            }

                            BackToMainMenu();

                            break;

                        case "5":
                            currentUser = "Admin";
                            islogged = adminOptions;
                            break;

                        case "6":
                            sysOn = false;
                            break;
                    }
                }
                else
                {
                    switch (input)
                    {
                        case "1":
                            Console.WriteLine($"Add Vehicle to Parking\n------------------------------------------------");

                            List<Type> types = Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(Vehicle))).ToList();

                            for (int i = 0; i < types.Count(); i++)
                            {
                                Console.WriteLine($"{i + 1} {types[i].Name}");
                            }
                            int typeChosen = 0;
                            bool correctInput = false;
                            do
                            {
                                try
                                {
                                    typeChosen = Int32.Parse(Console.ReadLine());
                                    if (typeChosen >= 1 && typeChosen <= types.Count())
                                    {
                                        correctInput = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"No Type corresponds to the selection provided, please input a valid number between 1 & {types.Count()}");
                                    }
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine($"Please input a valid number between 1 & {types.Count() + 1}");
                                }
                            }
                            while (!correctInput);

                            Console.WriteLine($"Enter {types[typeChosen - 1].Name} Details:");

                            switch (typeChosen)
                            {
                                case 1:
                                    AddVehicle(new Car());
                                    break;
                                case 2:
                                    AddVehicle(new ElectricCar());
                                    break;
                                case 3:
                                    AddVehicle(new Motorbike());
                                    break;
                                case 4:
                                    AddVehicle(new Van());
                                    break;
                            }

                            BackToMainMenu();

                            break;

                        case "2":
                            Console.WriteLine($"Remove Vehicle from Parking\n------------------------------------------------");

                            if (parking.Count() > 0)
                            {
                                Console.Write("Please enter the vehicle registration Number: ");

                                DeleteVehicle(Console.ReadLine());
                            }
                            else
                            {
                                Console.WriteLine("There are no vehicles in the parking lot to be delete, ");
                            }

                            BackToMainMenu();

                            break;

                        case "3":
                            Console.WriteLine($"List Parked Vehicles");

                            ListVehicles();

                            BackToMainMenu();

                            break;

                        case "4":
                            Console.WriteLine($"List Parked Vehicles (Ordered)");

                            ListOrderedVehicles();

                            BackToMainMenu();

                            break;

                        case "5":
                            Console.WriteLine($"Generate Report\n------------------------------------------------");

                            if (parking.Count() > 0)
                            {
                                Console.WriteLine("Please enter a file name to generate the report to");
                                string outputFilename = Console.ReadLine();
                                string outputFilePath = $"C://Users//USER//Desktop/{outputFilename}.txt";
                                GenerateReport(outputFilePath);

                                Console.WriteLine($"File saved in {outputFilePath}");
                            }
                            else
                            {
                                Console.WriteLine("To generate report, please add vehicles to the parking lot.");
                            }

                            BackToMainMenu();

                            break;

                        case "6":
                            currentUser = "Customer";
                            islogged = customerOptions;
                            break;

                        case "7":
                            sysOn = false;
                            break;
                    }
                }
            }
        }

        public bool AddReservation(string number, Schedule wantedSchedule)
        {
            foreach (Vehicle v in parking) 
            {
                if (v.GetRegNum() == number)
                {
                    if (v.Available(wantedSchedule))
                    {
                        v.Reserve(wantedSchedule);
                        Console.WriteLine("Reservation Complete");
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine("Reservation Unsuccessful");
            return false;
        }

        public bool AddVehicle(Vehicle v)
        {
            if (parking.Count < 50)
            {
                bool carParked = false;
                foreach (Vehicle vehicle in parking) 
                {
                    if (vehicle.GetRegNum() == v.GetRegNum())
                    {
                        carParked = true;
                        break;
                    }
                }

                if (!carParked)
                {
                    parking.Add(v);
                    Vehicle.UpdateSpaces(v.GetRegNum());
                    Console.WriteLine($"Vehicle added successfuly! - Available Parking Spaces Available: {50 - parking.Count}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Vehicle Already in parking lot");
                    return false;
                }
            }
            Console.WriteLine("No Space left in parking lot");
            return false;
        }

        public bool ChangeReservation(string number, Schedule oldSchedule, Schedule newSchedule)
        {
            foreach (Vehicle v in parking) 
            {
                if (v.GetRegNum() == number)
                {
                    if (v.AlterBooking(oldSchedule, newSchedule))
                    {
                        Console.WriteLine("Booking Revised Successfuly");
                        return true;
                    }
                }
            }
            Console.WriteLine("Booking was not revised successfuly");
            return false;
        }

        public bool DeleteReservation(string number, Schedule schedule)
        {
            foreach (Vehicle v in parking)
            {
                if (v.GetRegNum() == number)
                {
                    if (v.DeleteBooking(schedule))
                    {
                        Console.WriteLine("Booking Cancelled Successfuly");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("No schedule with the given details exists for the given vehicle");
                        return false;
                    }
                }
            }
            Console.WriteLine("The Vehicle specified does not exist on the system");
            return false;
        }

        public bool DeleteVehicle(string number)
        {
            foreach (Vehicle vehicle in parking) 
            {
                if (vehicle.GetRegNum() == number)
                {
                    Console.WriteLine(vehicle.GetVehicleInfo());
                    Vehicle.UpdateSpaces(number);
                    parking.Remove(vehicle);
                    Console.WriteLine($"Vehicle removed successfuly! - Available Parking Spaces Available: {50 - parking.Count}");
                    return true;
                }
            }
            Console.WriteLine("Vehicle not in parking");
            return false;
        }

        public void GenerateReport(string fileName)
        {
            StreamWriter sw = new StreamWriter(fileName, false);
            foreach (Vehicle vehicle1 in parking)
            {
                sw.WriteLine(vehicle1.GetVehicleInfo());
            }
            sw.Dispose();
        }

        public void ListAvailableVehicles(Schedule wantedSchedule, Type type)
        {
            List<Vehicle> availableVehicles = Vehicle.GetAvailableVehicles(parking, wantedSchedule, type);
            string information;
            if (availableVehicles.Count() != 0)
            {
                information = $"Available {type.Name}s: {wantedSchedule.GetScheduleInfo()}\n";
                foreach (Vehicle vehicle in availableVehicles)
                {
                    information += vehicle.GetVehicleInfo() + "\n";
                }
            }
            else
            {
                information = "No Vehicles available on the requested dates";
            }

            Console.Write(information);
        }

        public void ListOrderedVehicles()
        {
            parking.Sort();
            ListVehicles();
        }

        public void ListVehicles()
        {
            if (parking.Count() != 0)
            {
                foreach (Vehicle vehicle in parking)
                {
                    Console.WriteLine(vehicle.GetVehicleInfo());
                }
            }
            else
            {
                Console.WriteLine("No Vehicles are Available at the moment");
            }
        }

        public void BackToMainMenu()
        {
            string backToMainMenu = "0";
            while (backToMainMenu != "1")
            {
                Console.WriteLine("1: Go Back");
                backToMainMenu = Console.ReadLine();
                if (backToMainMenu != "1")
                {
                    Console.WriteLine("Please enter '1' to go back to main menu");
                }
            }
        }

        static void Main(string[] args)
        {
            WestminsterRentalVehicle wrvs = new WestminsterRentalVehicle();
        }
    }
}  