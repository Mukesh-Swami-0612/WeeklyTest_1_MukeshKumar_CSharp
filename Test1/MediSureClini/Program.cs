using System;

namespace MediSureClinicBilling
{
    // =======================
    // Model Class
    // =======================
    // This class stores all details related to a single patient bill
    public class BillRecord
    {
        // Unique bill number
        public string BillNumber { get; set; }

        // Patient name
        public string Patient { get; set; }

        // Indicates whether insurance is available or not
        public bool InsuranceAvailable { get; set; }

        // Charges for doctor consultation
        public decimal DoctorFee { get; set; }

        // Charges for lab tests
        public decimal TestFee { get; set; }

        // Charges for medicines
        public decimal MedicineFee { get; set; }

        // Total bill amount before discount
        public decimal TotalAmount { get; set; }

        // Discount applied due to insurance
        public decimal InsuranceDiscount { get; set; }

        // Final amount to be paid by the patient
        public decimal NetPayable { get; set; }
    }

    // =======================
    // Service Class
    // =======================
    // This class handles all billing operations
    public static class BillingService
    {
        // Stores the most recent bill created
        private static BillRecord _latestBill;

        // Method to generate a new bill
        public static void GenerateBill()
        {
            // Create a new bill object
            BillRecord bill = new BillRecord();

            // Read bill number
            Console.Write("Bill Number: ");
            bill.BillNumber = Console.ReadLine();

            // Read patient name
            Console.Write("Patient Name: ");
            bill.Patient = Console.ReadLine();

            // Validation: Bill number and patient name cannot be empty
            if (string.IsNullOrWhiteSpace(bill.BillNumber) || string.IsNullOrWhiteSpace(bill.Patient))
            {
                Console.WriteLine("Bill number and patient name are mandatory.");
                return;
            }

            // Read insurance availability (Y/N)
            Console.Write("Insurance (Y/N): ");
            bill.InsuranceAvailable = Console.ReadLine().Trim().ToUpper() == "Y";

            // Read all charge amounts
            bill.DoctorFee = ReadAmount("Doctor Consultation Fee");
            bill.TestFee = ReadAmount("Lab Test Charges");
            bill.MedicineFee = ReadAmount("Medicine Charges");

            // Calculate total, discount and payable amount
            CalculateAmounts(bill);

            // Store bill as latest bill
            _latestBill = bill;

            // Display summary
            Console.WriteLine("\n✔ Bill Generated Successfully");
            Console.WriteLine($"Total Amount : {bill.TotalAmount:F2}");
            Console.WriteLine($"Discount     : {bill.InsuranceDiscount:F2}");
            Console.WriteLine($"Payable      : {bill.NetPayable:F2}");
        }

        // Method to read and validate monetary values
        private static decimal ReadAmount(string label)
        {
            Console.Write($"Enter {label}: ");

            // Validate input: must be numeric and non-negative
            if (!decimal.TryParse(Console.ReadLine(), out decimal value) || value < 0)
            {
                Console.WriteLine("Invalid amount entered.");
                Environment.Exit(0); // Exit program on invalid input
            }

            return value;
        }

        // Method to calculate total amount, discount, and final payable amount
        private static void CalculateAmounts(BillRecord bill)
        {
            // Calculate total amount
            bill.TotalAmount = bill.DoctorFee + bill.TestFee + bill.MedicineFee;

            // Apply 10% discount if insurance is available
            bill.InsuranceDiscount = bill.InsuranceAvailable
                                     ? bill.TotalAmount * 0.10m
                                     : 0;

            // Calculate net payable amount
            bill.NetPayable = bill.TotalAmount - bill.InsuranceDiscount;
        }

        // Method to display the last generated bill
        public static void ShowLastBill()
        {
            // Check if any bill exists
            if (_latestBill == null)
            {
                Console.WriteLine("No billing record found.");
                return;
            }

            // Display bill details
            Console.WriteLine("\n------ Last Bill Details ------");
            Console.WriteLine($"Bill No     : {_latestBill.BillNumber}");
            Console.WriteLine($"Patient     : {_latestBill.Patient}");
            Console.WriteLine($"Insurance   : {(_latestBill.InsuranceAvailable ? "Yes" : "No")}");
            Console.WriteLine($"Doctor Fee  : {_latestBill.DoctorFee:F2}");
            Console.WriteLine($"Lab Fee     : {_latestBill.TestFee:F2}");
            Console.WriteLine($"Medicine Fee: {_latestBill.MedicineFee:F2}");
            Console.WriteLine($"Total       : {_latestBill.TotalAmount:F2}");
            Console.WriteLine($"Discount    : {_latestBill.InsuranceDiscount:F2}");
            Console.WriteLine($"Payable     : {_latestBill.NetPayable:F2}");
            Console.WriteLine("--------------------------------");
        }

        // Method to delete the last bill
        public static void RemoveLastBill()
        {
            _latestBill = null; // Clear stored bill
            Console.WriteLine("Billing data cleared successfully.");
        }
    }

    // =======================
    // Program Class
    // =======================
    // Entry point of the application
    class Program
    {
        static void Main()
        {
            int choice;

            // Menu-driven loop
            do
            {
                Console.WriteLine("\n=== MediSure Billing System ===");
                Console.WriteLine("1. Generate New Bill");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Delete Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Choose option: ");

                // Validate menu input
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid numeric option.");
                    continue;
                }

                // Perform action based on user choice
                switch (choice)
                {
                    case 1:
                        BillingService.GenerateBill();
                        break;

                    case 2:
                        BillingService.ShowLastBill();
                        break;

                    case 3:
                        BillingService.RemoveLastBill();
                        break;

                    case 4:
                        Console.WriteLine("Application terminated.");
                        break;

                    default:
                        Console.WriteLine("Invalid menu selection.");
                        break;
                }

            } while (choice != 4); // Loop until Exit option is chosen
        }
    }
}
