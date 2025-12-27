using System;

namespace QuickMartProfitCalculator
{
    // ============================
    // Class to store sale details
    // ============================
    class SaleInfo
    {
        // Invoice number of the sale
        public string InvoiceNo;

        // Customer name
        public string CustomerName;

        // Item name
        public string ItemName;

        // Quantity sold
        public int Quantity;

        // Cost price (total)
        public decimal CostPrice;

        // Selling price (total)
        public decimal SellingPrice;

        // Profit / Loss / Break-Even
        public string Result;

        // Amount of profit or loss
        public decimal ResultAmount;

        // Percentage of profit or loss
        public decimal ResultPercent;
    }

    // ============================
    // Main Program Class
    // ============================
    class Program
    {
        // Variable to store last transaction
        static SaleInfo lastSale = null;

        static void Main(string[] args)
        {
            int choice;

            // Menu keeps running until user exits
            do
            {
                Console.WriteLine("\n====== QuickMart Profit Calculator ======");
                Console.WriteLine("1. Enter New Sale");
                Console.WriteLine("2. Show Last Sale");
                Console.WriteLine("3. Calculate Profit / Loss Again");
                Console.WriteLine("4. Exit");
                Console.Write("Enter choice: ");

                // Check if input is valid number
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Please enter a valid number.");
                    continue;
                }

                // Perform operation based on choice
                if (choice == 1)
                {
                    AddNewSale();
                }
                else if (choice == 2)
                {
                    ShowSale();
                }
                else if (choice == 3)
                {
                    ReCalculate();
                }
                else if (choice == 4)
                {
                    Console.WriteLine("Program Closed.");
                }
                else
                {
                    Console.WriteLine("Invalid option.");
                }

            } while (choice != 4);
        }

        // ============================
        // Method to enter sale details
        // ============================
        static void AddNewSale()
        {
            SaleInfo s = new SaleInfo();

            // Read invoice number
            Console.Write("Invoice No: ");
            s.InvoiceNo = Console.ReadLine();

            // Read customer name
            Console.Write("Customer Name: ");
            s.CustomerName = Console.ReadLine();

            // Read item name
            Console.Write("Item Name: ");
            s.ItemName = Console.ReadLine();

            // Read quantity
            Console.Write("Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out s.Quantity) || s.Quantity <= 0)
            {
                Console.WriteLine("Invalid quantity.");
                return;
            }

            // Read cost price
            Console.Write("Purchase Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out s.CostPrice) || s.CostPrice <= 0)
            {
                Console.WriteLine("Invalid purchase amount.");
                return;
            }

            // Read selling price
            Console.Write("Selling Amount: ");
            if (!decimal.TryParse(Console.ReadLine(), out s.SellingPrice) || s.SellingPrice < 0)
            {
                Console.WriteLine("Invalid selling amount.");
                return;
            }

            // Calculate profit or loss
            CalculateResult(s);

            // Store last sale
            lastSale = s;

            Console.WriteLine("\nSale saved successfully.");
            Console.WriteLine("Result: " + s.Result);
            Console.WriteLine("Amount: " + s.ResultAmount);
            Console.WriteLine("Percentage: " + s.ResultPercent);
        }

        // ============================
        // Method to calculate result
        // ============================
        static void CalculateResult(SaleInfo s)
        {
            if (s.SellingPrice > s.CostPrice)
            {
                // Profit case
                s.Result = "PROFIT";
                s.ResultAmount = s.SellingPrice - s.CostPrice;
            }
            else if (s.SellingPrice < s.CostPrice)
            {
                // Loss case
                s.Result = "LOSS";
                s.ResultAmount = s.CostPrice - s.SellingPrice;
            }
            else
            {
                // Break-even case
                s.Result = "BREAK-EVEN";
                s.ResultAmount = 0;
            }

            // Calculate percentage
            s.ResultPercent = (s.ResultAmount / s.CostPrice) * 100;
        }

        // ============================
        // Method to display last sale
        // ============================
        static void ShowSale()
        {
            if (lastSale == null)
            {
                Console.WriteLine("No sale found.");
                return;
            }

            Console.WriteLine("\n------ Last Sale Details ------");
            Console.WriteLine("Invoice No   : " + lastSale.InvoiceNo);
            Console.WriteLine("Customer     : " + lastSale.CustomerName);
            Console.WriteLine("Item         : " + lastSale.ItemName);
            Console.WriteLine("Quantity     : " + lastSale.Quantity);
            Console.WriteLine("Cost Price   : " + lastSale.CostPrice);
            Console.WriteLine("Selling Price: " + lastSale.SellingPrice);
            Console.WriteLine("Result       : " + lastSale.Result);
            Console.WriteLine("Amount       : " + lastSale.ResultAmount);
            Console.WriteLine("Percentage   : " + lastSale.ResultPercent);
            Console.WriteLine("--------------------------------");
        }

        // ============================
        // Method to recalculate result
        // ============================
        static void ReCalculate()
        {
            if (lastSale == null)
            {
                Console.WriteLine("No sale found to calculate.");
                return;
            }

            // Recalculate profit/loss
            CalculateResult(lastSale);

            Console.WriteLine("\nCalculation Updated!");
            Console.WriteLine("Result: " + lastSale.Result);
            Console.WriteLine("Amount: " + lastSale.ResultAmount);
            Console.WriteLine("Percentage: " + lastSale.ResultPercent);
        }
    }
}
