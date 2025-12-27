
using System;

namespace QuickMartProfitCalculator
{
    public class SaleTransaction
    {
        // Invoice number
        public string InvoiceNo { get; set; }

        // Name of customer
        public string CustomerName { get; set; }

        // Name of item sold
        public string ItemName { get; set; }

        // Quantity of items sold
        public int Quantity { get; set; }

        // Total purchase amount (cost price)
        public decimal PurchaseAmount { get; set; }

        // Total selling amount (selling price)
        public decimal SellingAmount { get; set; }

        // Stores PROFIT / LOSS / BREAK-EVEN
        public string ProfitOrLossStatus { get; set; }

        // Amount of profit or loss
        public decimal ProfitOrLossAmount { get; set; }

        // Profit or loss percentage
        public decimal ProfitMarginPercent { get; set; }
    }

    // Main program class
    class Program
    {
        // Stores last transaction object
        static SaleTransaction LastTransaction = null;

        // Flag to check whether transaction exists
        static bool HasLastTransaction = false;

        // Main method → execution starts from here
        static void Main(string[] args)
        {
            int option = 0;

            // Loop runs until user selects Exit
            do
            {
                // Display menu
                Console.WriteLine("\nQuickMart Traders");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                // Validate numeric input
                bool isValid = int.TryParse(Console.ReadLine(), out option);

                // If input is not a number
                if (!isValid)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 4.");
                    continue;
                }

                // Perform action based on option
                switch (option)
                {
                    case 1:
                        // Create new transaction
                        CreateTransaction();
                        break;

                    case 2:
                        // View last transaction
                        ViewTransaction();
                        break;

                    case 3:
                        // Recalculate profit/loss
                        CalculateProfitLoss();
                        break;

                    case 4:
                        // Exit program
                        Console.WriteLine("\nThank you. Application closed normally.");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please select 1 to 4.");
                        break;
                }

            } while (option != 4); // Loop stops when option is 4
        }

        // Method to create a new sale transaction
        public static void CreateTransaction()
        {
            // Create object of SaleTransaction class
            SaleTransaction t = new SaleTransaction();

            // Read invoice number
            Console.Write("Enter Invoice No: ");
            t.InvoiceNo = Console.ReadLine().Trim();

            // Validate invoice number
            if (string.IsNullOrEmpty(t.InvoiceNo))
            {
                Console.WriteLine("Invoice number cannot be empty.");
                return;
            }

            // Read customer name
            Console.Write("Enter Customer Name: ");
            t.CustomerName = Console.ReadLine().Trim();

            // Validate customer name
            if (string.IsNullOrEmpty(t.CustomerName))
            {
                Console.WriteLine("Customer name cannot be empty.");
                return;
            }

            // Read item name
            Console.Write("Enter Item Name: ");
            t.ItemName = Console.ReadLine().Trim();

            // Validate item name
            if (string.IsNullOrEmpty(t.ItemName))
            {
                Console.WriteLine("Item name cannot be empty.");
                return;
            }

            // Read quantity
            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }
            t.Quantity = qty;

            // Read purchase amount
            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchase) || purchase <= 0)
            {
                Console.WriteLine("Purchase amount must be greater than 0.");
                return;
            }
            t.PurchaseAmount = purchase;

            // Read selling amount
            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal selling) || selling < 0)
            {
                Console.WriteLine("Selling amount cannot be negative.");
                return;
            }
            t.SellingAmount = selling;

            // Check profit or loss condition
            if (t.SellingAmount > t.PurchaseAmount)
            {
                // Profit case
                t.ProfitOrLossStatus = "PROFIT";
                t.ProfitOrLossAmount = t.SellingAmount - t.PurchaseAmount;
            }
            else if (t.SellingAmount < t.PurchaseAmount)
            {
                // Loss case
                t.ProfitOrLossStatus = "LOSS";
                t.ProfitOrLossAmount = t.PurchaseAmount - t.SellingAmount;
            }
            else
            {
                // Break-even case
                t.ProfitOrLossStatus = "BREAK-EVEN";
                t.ProfitOrLossAmount = 0;
            }

            // Calculate profit or loss percentage
            t.ProfitMarginPercent = (t.ProfitOrLossAmount / t.PurchaseAmount) * 100;

            // Save transaction
            LastTransaction = t;
            HasLastTransaction = true;

            // Display result
            Console.WriteLine("\nTransaction saved successfully.");
            Console.WriteLine($"Status: {t.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {t.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {t.ProfitMarginPercent:F2}");
        }

        // Method to display last transaction
        public static void ViewTransaction()
        {
            // Check if transaction exists
            if (!HasLastTransaction || LastTransaction == null)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            // Print transaction details
            Console.WriteLine("\n Last Transaction ");
            Console.WriteLine($"Invoice No: {LastTransaction.InvoiceNo}");
            Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
            Console.WriteLine($"Item: {LastTransaction.ItemName}");
            Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
            Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}");
            Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}");
            Console.WriteLine("--------------------------------------------");
        }

        // Method to recalculate profit or loss
        public static void CalculateProfitLoss()
        {
            // Check if transaction exists
            if (!HasLastTransaction || LastTransaction == null)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            // Use last transaction
            SaleTransaction t = LastTransaction;

            // Recalculate profit/loss
            if (t.SellingAmount > t.PurchaseAmount)
            {
                t.ProfitOrLossStatus = "PROFIT";
                t.ProfitOrLossAmount = t.SellingAmount - t.PurchaseAmount;
            }
            else if (t.SellingAmount < t.PurchaseAmount)
            {
                t.ProfitOrLossStatus = "LOSS";
                t.ProfitOrLossAmount = t.PurchaseAmount - t.SellingAmount;
            }
            else
            {
                t.ProfitOrLossStatus = "BREAK-EVEN";
                t.ProfitOrLossAmount = 0;
            }

            // Calculate percentage again
            t.ProfitMarginPercent = (t.ProfitOrLossAmount / t.PurchaseAmount) * 100;

            // Display recalculated result
            Console.WriteLine("\nRecalculated Successfully!");
            Console.WriteLine($"Status: {t.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {t.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {t.ProfitMarginPercent:F2}");
        }
    }
}
