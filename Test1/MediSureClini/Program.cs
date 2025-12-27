
using System;

namespace MediSureClinicBilling
{
    //represents  patient's bill details
    public class PatientBill
    {
        // Bill unique ID
        public string BillId { get; set; }

        // Patient name
        public string PatientName { get; set; }

        // True if patient has insurance, otherwise false
        public bool HasInsurance { get; set; }

        // Charges for doctor consultation
        public decimal ConsultationFee { get; set; }

        // Charges for lab tests
        public decimal LabCharges { get; set; }

        // Charges for medicines
        public decimal MedicineCharges { get; set; }

        // Total amount before discount
        public decimal GrossAmount { get; set; }

        // Discount amount (insurance based)
        public decimal DiscountAmount { get; set; }

        // Final amount to be paid
        public decimal FinalPayable { get; set; }
    }

    // Main program class
    class Program
    {
        // Stores the last created bill
        static PatientBill LastBill = null;

        // Flag to check whether last bill exists or not
        static bool HasLastBill = false;

        // Main method → program execution starts from here
        static void Main(string[] args)
        {
            int option = 0;

            // Do-while loop keeps running until user chooses Exit
            do
            {
                // Display menu options
                Console.WriteLine("\nMediSure Clinic Billing");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                // Read user input and check if it is a valid number
                bool isValid = int.TryParse(Console.ReadLine(), out option);

                // If input is not a number
                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    continue;
                }

                // Switch case to perform action based on option
                switch (option)
                {
                    case 1:
                        // Create new bill
                        CreateNewBill();
                        break;

                    case 2:
                        // View last bill
                        ViewLastBill();
                        break;

                    case 3:
                        // Clear last bill
                        ClearLastBill();
                        break;

                    case 4:
                        // Exit the application
                        Console.WriteLine("\nThank you. Application closed normally.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select from 1 to 4.");
                        break;
                }

            } while (option != 4); // Loop stops when user chooses Exit
        }

        // Method to create a new patient bill
        public static void CreateNewBill()
        {
            // Create object of PatientBill class
            PatientBill bill = new PatientBill();

            // Read Bill ID
            Console.Write("Enter Bill Id: ");
            bill.BillId = Console.ReadLine().Trim();

            // Validation: Bill ID should not be empty
            if (string.IsNullOrEmpty(bill.BillId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            // Read Patient Name
            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine().Trim();

            // Validation: Patient name should not be empty
            if (string.IsNullOrEmpty(bill.PatientName))
            {
                Console.WriteLine("Patient name cannot be empty.");
                return;
            }

            // Ask if patient has insurance
            Console.Write("Is the patient insured? (Y/N): ");
            string insuranceInput = Console.ReadLine().Trim().ToUpper();

            // Convert Y/N input into boolean
            bill.HasInsurance = insuranceInput == "Y";

            // Read Consultation Fee
            Console.Write("Enter Consultation Fee: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal consult) || consult <= 0)
            {
                Console.WriteLine("Consultation Fee must be greater than 0.");
                return;
            }
            bill.ConsultationFee = consult;

            // Read Lab Charges
            Console.Write("Enter Lab Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal lab) || lab < 0)
            {
                Console.WriteLine("Lab Charges cannot be negative.");
                return;
            }
            bill.LabCharges = lab;

            // Read Medicine Charges
            Console.Write("Enter Medicine Charges: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal med) || med < 0)
            {
                Console.WriteLine("Medicine Charges cannot be negative.");
                return;
            }
            bill.MedicineCharges = med;

            // Calculate Gross Amount
            bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges;

            // Apply 10% discount if insured
            bill.DiscountAmount = bill.HasInsurance ? bill.GrossAmount * 0.10m : 0;

            // Calculate final payable amount
            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;

            // Save bill as last bill
            LastBill = bill;
            HasLastBill = true;

            // Display bill summary
            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
        }

        // Method to display last bill
        public static void ViewLastBill()
        {
            // If no bill exists
            if (!HasLastBill || LastBill == null)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            // Display last bill details
            Console.WriteLine("\nLast Bill");
            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Insured: {(LastBill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {LastBill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }

        // Method to clear last bill
        public static void ClearLastBill()
        {
            LastBill = null;       // Remove bill object
            HasLastBill = false;   // Reset flag
            Console.WriteLine("Last bill cleared.");
        }
    }
}
