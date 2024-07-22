using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IT502_Assignment_2
{
    internal class LHMSProgram
    {
        
        static string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        
        static void Main(string[] args)
        {
            

                if (Directory.Exists(myDocuments + "\\LHMS.DATA"))
                {
                    Console.WriteLine("Directory has been found!");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Creating new directory 'LHMS.DATA' in myDocuments....");
                    Directory.CreateDirectory(myDocuments + "\\LHMS.DATA");
                    Console.ReadLine();
                }

                char ans;

                do
                {
                    Console.Clear();
                    Console.WriteLine("***********************************************************************************");
                    Console.WriteLine("                 LANGHAM HOTEL MANAGEMENT SYSTEM                  ");
                    Console.WriteLine("                            MENU                                 ");
                    Console.WriteLine("***********************************************************************************");
                    Console.WriteLine("1. Add Rooms");
                    Console.WriteLine("2. Display Rooms");
                    Console.WriteLine("3. Allocate Rooms");
                    Console.WriteLine("4. De-Allocate Rooms");
                    Console.WriteLine("5. Display Room Allocation Details");
                    Console.WriteLine("6. Billing");
                    Console.WriteLine("7. Save Room Allocations To Backup File");
                    Console.WriteLine("8. Show the Room Allocations From Backup File");
                    Console.WriteLine("9. Exit");
                    Console.WriteLine("***********************************************************************************");
                    Console.Write("Enter Your Choice Number Here:");
                    int choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            AddRoom();
                            break;
                        case 2:
                            DisplayRooms();
                            break;
                        case 3:
                            AllocateRooms();
                            break;
                        case 4:
                            DeAllocateRooms();
                            break;
                        case 5:
                            AllocationDetails();
                            break;
                        case 6:
                            Billing();
                            break;
                        case 7:
                            BackupFile();
                            break;
                        case 8:
                            ShowBackupFile();
                            break;
                        case 9:
                            Exit();
                            break;
                        default:
                            break;
                    }
                    Console.Write("\nWould You Like To Continue (Y/N):");
                    ans = Convert.ToChar(Console.ReadLine());
                }
                while (ans == 'y' || ans == 'Y');
                

        }

        static void CreateAddRoomFile()
        {
            string pathName1 = @"C:\Users\JD\Documents\LHMS.DATA\AddRoom.txt";
            FileStream fs1 = File.Create(pathName1);
        }

        static void CreateRoomAllocationFile()
        {
            string pathName2 = @"C:\Users\JD\Documents\LHMS.DATA\RoomAllocation.txt"; 
            FileStream fs2 = File.Create(pathName2);
        }

        static void AddRoom() 
        {
            try
            {
                Console.WriteLine("Please enter room number: ");
                int roomNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter the number of beds: ");
                int numberOfBeds = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter bed sizes (Single, Double, Queen, King): ");
                string bedSize = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Please specify Lakeview or Cityview: ");
                string roomView = Convert.ToString(Console.ReadLine());

                string pathName1 = @"C:\Users\JD\Documents\LHMS.DATA\AddRoom.txt";
                FileStream fs1 = new FileStream(pathName1, FileMode.Append, FileAccess.Write);
                using (StreamWriter LHMSAddRoom = new StreamWriter(fs1))
                {
                    LHMSAddRoom.WriteLine("\nInformation added to the system at: " + DateTime.Now);
                    LHMSAddRoom.WriteLine($"\nROOM AVAILABILITY \nRoom Number: {roomNumber} \nNumber of Beds: {numberOfBeds} \nBed Size: {bedSize} \nRoom View: {roomView}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Unhandled Exception: System.FormatException: Input string was not in a correct format");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unhandled Exception: System.ArgumentException: Stream was not writable");
            }

        }

        static void DisplayRooms()
        {
            try
            {
                string ReadRooms = File.ReadAllText(myDocuments + "\\LHMS.DATA\\AddRoom.txt");
                Console.WriteLine(ReadRooms);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unhandled Exception: System.FileNotFoundException: Could not locate file");
            }

        }

        static void AllocateRooms()
        {
            try
            {
                Console.Write("Please specify customer check-in date (dd/mm/yy): ");
                string checkInDate = Convert.ToString(Console.ReadLine());

                Console.Write("Please allocate a room number: ");
                int roomNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter customer ID");
                int customerID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please enter customer's Fullname");
                String customerName = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Please enter number of guest: ");
                int numberOfGuest = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please indicate late check-out (Y/N): ");
                Char lateCheckOut = Convert.ToChar(Console.ReadLine());

                string pathName2 = @"C:\Users\JD\Documents\LHMS.DATA\RoomAllocation.txt";
                FileStream fs2 = new FileStream(pathName2, FileMode.Append, FileAccess.Write);
                using (StreamWriter LHMSAllocate = new StreamWriter(fs2))
                {
                    LHMSAllocate.WriteLine("\nInformation added to the system at: " + DateTime.Now);
                    LHMSAllocate.WriteLine($"\nThe guest has checked into the room: {checkInDate} \nRoom Number: {roomNumber} \nCustomer ID: {customerID}");
                    LHMSAllocate.WriteLine($"\nCustomer Name: {customerName} \nNumber of Guest: {numberOfGuest} \nLate Check-out: {lateCheckOut}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Unhandled Exception: System.FormatException: Input string was not in a correct format");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Unhandled Exception: System.InvalidOperationException: Sequence contains no matching element");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unhandled Exception: System.ArgumentException: Stream was not writable");
            }

        }

        static void DeAllocateRooms()
        {
            try
            {
                Console.WriteLine("Please indicate check-out date (dd/mm/yy): ");
                string checkOutDate = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Please enter customer ID");
                int customerID = Convert.ToInt32(Console.ReadLine());

                Console.Write("Please allocate a room number: ");
                int roomNumber = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Please indicate if room has been cleaned (Y/N): ");
                Char cleaningStatus = Convert.ToChar(Console.ReadLine());

                string pathName2 = @"C:\Users\JD\Documents\LHMS.DATA\RoomAllocation.txt";
                FileStream fs2 = new FileStream(pathName2, FileMode.Append, FileAccess.Write);
                using (StreamWriter LHMSDeAllocate = new StreamWriter(fs2))
                {
                    LHMSDeAllocate.WriteLine("\n Information added to the system at: " + DateTime.Now);
                    LHMSDeAllocate.WriteLine($"\nThe room has been returned: {checkOutDate} \nCustomer ID: {customerID} \nRoom Number: {roomNumber} \nCleaning Status {cleaningStatus}");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Unhandled Exception: System.FormatException: Input string was not in a correct format");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Unhandled Exception: System.InvalidOperationException: Sequence contains no matching element");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unhandled Exception: System.ArgumentException: Stream was not writable");
            }
        }

        static void AllocationDetails()
        {
            try
            {
                string ReadRooms = File.ReadAllText(myDocuments + "\\LHMS.DATA\\RoomAllocation.txt");
                Console.WriteLine(ReadRooms);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unhandled Exception: System.FileNotFoundException: Could not locate file");
            }

        }

        static void Billing()
        {
            Console.WriteLine("Billing Feature is Under Construction and will be added soon…!!!");
        }

        static void BackupFile()
        {
            try
            {
                string source1 = File.ReadAllText(@"C:\Users\JD\Documents\LHMS.DATA\AddRoom.txt");
                string source2 = File.ReadAllText(@"C:\Users\JD\Documents\LHMS.DATA\RoomAllocation.txt");
                string TempBackup = myDocuments + "\\LHMS.DATA\\lhms_studentid.txt";

                using (StreamWriter backup = new StreamWriter(TempBackup))
                {
                    backup.WriteLine("Data from the first file");
                    backup.WriteLine(source1);
                    backup.WriteLine("Data from the second file");
                    backup.WriteLine(source2);
                }

                string RealBackup = myDocuments + "\\LHMS.DATA\\lhms_studentid_backup.txt";
                File.Copy(TempBackup, RealBackup);
                using (FileStream EraseFunction = new FileStream(TempBackup, FileMode.Open))
                {
                    EraseFunction.SetLength(0);
                }
                Console.WriteLine("Backup has been created and original file has been reset");
                Console.ReadLine();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Unhandled Exception: System.ArgumentException: Stream was not writable");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unhandled Exception: System.FileNotFoundException: Could not locate file");
            }
        }

        static void ShowBackupFile()
        {
            try
            {
                string ReadBackup = File.ReadAllText(myDocuments + "\\LHMS.DATA\\lhms_studentid_backup.txt");
                Console.WriteLine(ReadBackup);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Unhandled Exception: System.FileNotFoundException: Could not locate file");
            }
        }

        static void Exit()
        {
            Console.WriteLine("Would you like to exit the application? (Y/N)");
            string selection = Console.ReadLine();

            if (selection.Equals("Y") || selection.Equals("y"))
            {
                Environment.Exit(0);
            }

        }

    }

}
