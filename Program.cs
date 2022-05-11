using System;
using System.Text;
using System.Threading;

namespace ATM_Machine
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sets title of the console.
            Console.Title = "$$$ ATM Machine $$$";

            // Welcome screen of ATM, followed by asking user to insert their card simulated by pressing ENTER.
            // User has 3 times to press ENTER before program exits.
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n\t\t $ $ $ Welcome to a random ATM! $ $ $");
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(3000);

            string access = EnterCard();
            int attemptEnter = 2;
            while (access != String.Empty)
            {
                if (attemptEnter <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\n\t\tATM crashed... GOOD BYE.");
                    Environment.Exit(0);
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\t\t  Just press ENTER and nothing else");
                Thread.Sleep(3000);
                attemptEnter--;
                Console.ForegroundColor = ConsoleColor.White;
                access = EnterCard();
            }

            // Enter predetermined pin to gain access to account.
            // User has 3 times to enter correct pin before program exits.
            string pin = RequestPIN();
            int attemptPin = 2;
            while (pin != "1234")
            {
                if (attemptPin <= 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\n\n\t\tATM crashed... GOOD BYE.");
                    Environment.Exit(0);
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n\n\n\t\t  Incorrect pin.");
                Thread.Sleep(3000);
                Console.ForegroundColor = ConsoleColor.White;
                attemptPin--;
                pin = RequestPIN();
            }

            // Customer is instantiated to act as a random person with a random account
            Random r = new Random();
            int accountBalance = r.Next(0, 5000);
            Customer c = new Customer(accountBalance);

            // MenuOption is called followed by the results menu
            int value = MenuOptions(c);
            MenuOptionsResults(value, c);

            Console.ReadKey();
        }

        // EnterCard Method, simulates inserting card by asking user to press enter.
        private static string EnterCard()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\t\tPlease press ENTER to insert your card:");
            string access = Console.ReadLine();
            return access;
        }

        // RequestPin Method, as user is typing pin it is converted to * char.
        private static string RequestPIN()
        {
            StringBuilder sb = new StringBuilder();
            ConsoleKeyInfo keyInfo;

            Console.Clear();
            Console.Write("\n\n\n\t\tEnter PIN: ");

            do
            {
                keyInfo = Console.ReadKey(true);

                if (!char.IsControl(keyInfo.KeyChar))
                {
                    sb.Append(keyInfo.KeyChar);
                    Console.Write("*");
                    Console.Beep();
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            {
                return sb.ToString();
            }
        }

        // MenuOptions Method, displays menu options and will only take a response that is 1-4.
        private static int MenuOptions(Customer c)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\t\tWhat would you like to do?\n\n\t\t\t1. Deposit\n\n\t\t\t2. Withdraw\n\n\t\t\t3. Check Balance\n\n\t\t\t4. Exit");

            bool valid = false;
            while (!valid)
            {
                int value = ConvertToInt();
                if (value == 1 || value == 2 || value == 3 || value == 4)
                {
                    valid = true;
                    Console.ForegroundColor = ConsoleColor.White;
                    return value;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Keypad error. Please try again.");
                }
            }
            return 4;
        }

        // MenuOptionsResults Method, directs user to method that matches response from MenuOptions.
        private static void MenuOptionsResults(int result, Customer c)
        {
            switch (result)
            {
                case 1:
                    Deposit(c);
                    break;
                case 2:
                    Withdraw(c);
                    break;
                case 3:
                    CheckBalance(c);
                    break;
                case 4:
                    Exit();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A random ATM crashed... GOOD BYE.");
                    Environment.Exit(0);
                    break;
            }
        }

        // Deposit Method, user types amount desired to deposit. Minimum of 1 and maximum of 2000. Receipt method.
        private static void Deposit(Customer c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("\n\n\n       **minimum deposit of $ 1, maximum deposit of $ 2000**");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\n\t\tEnter amount you wish to deposit: \n\n\t\t\t  $  ");

            bool valid = false;
            while (!valid)
            {
                Console.ForegroundColor = ConsoleColor.White;
                int amountDeposited = ConvertToInt();
                if (amountDeposited >= 1 && amountDeposited <= 2000)
                {
                    c.accountBalance += amountDeposited;

                    Processing();


                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\n\n\t\tDeposit was accepted!");
                    Console.ForegroundColor = ConsoleColor.White;

                    Thread.Sleep(5000);

                    // Receipt Method, asks user if they would like a receipt for transaction.
                    Console.Clear();
                    Console.WriteLine("\n\n\n\t\tWould you like a receipt?\n\n\t\t    1.  Yes\n\n\t\t    2.  No");

                    bool again = false;
                    while (!again)
                    {
                        // deposit, withdraw, 
                        int receipt = ConvertToInt();
                        switch (receipt)
                        {
                            case 1:
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("\n\n\n\t    $$$ A T M $$$");
                                Console.ForegroundColor = ConsoleColor.White;
                                DateTime now = DateTime.Now;
                                Console.WriteLine("\n\n\n\t{0}\n\n\tTransaction = Deposit\n\n\tDeposit Amount = $ {1}\n\n\tAccount Balance = $ {2}", now, amountDeposited, c.accountBalance);

                                Thread.Sleep(7000);

                                Processing();

                                Console.WriteLine("\n\n\n\t\tPlease collect receipt.");
                                again = true;
                                break;
                            case 2:
                                again = true;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Keypad error. Please try again");
                                break;
                        }
                    }
                    valid = true;
                }
                else if (amountDeposited > 2000)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ATM doesn't support transactions greater than $ 2000. Please try again");
                }
                else if (amountDeposited < 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("ATM requires minimum of $ 1 to process transaction. Please try again");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Keypad error. Please try again.");
                }
            }


            AnotherSelection(c);
        }

        // Withdraw Method, gives user menu of predetermined amounts to withdraw and option to enter their own amount.
        // Minimum of 1, maximum of 2000, divisable by 20, and greater than accountBalance. Receipt method.
        private static void Withdraw(Customer c)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\t\tSelect from the following options:\n\n\t\t  1. $ 20\t\t4. $ 80\n\n\t\t  2. $ 40\t\t5. $ 100\n\n\t\t  3. $ 60\t        6. Enter amount");

            int l = c.accountBalance; // For receipt purposes only.

            bool valid = false;
            while (!valid)
            {
                int option = ConvertToInt();
                switch (option)
                {
                    case 1:
                        c.accountBalance -= 20;
                        valid = true;
                        break;
                    case 2:
                        c.accountBalance -= 40;
                        valid = true;
                        break;
                    case 3:
                        c.accountBalance -= 60;
                        valid = true;
                        break;
                    case 4:
                        c.accountBalance -= 80;
                        valid = true;
                        break;
                    case 5:
                        c.accountBalance -= 100;
                        valid = true;
                        break;
                    case 6:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\n\n\n\t**maximum withdraw of $ 2000 in increments of $ 20**");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("\n\t\tEnter amount you wish to withdraw\n\n\t\t\t   $  ");

                        // Tests if user input is divisable by 20 AND is greater than zero AND is less than 2000.
                        bool word = false;
                        while (!word)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            int value = ConvertToInt();

                            int isModulus = value % 20;

                            int n = c.accountBalance;
                            n -= value;

                            if (isModulus == 0 && n >= 0 && value <= 2000 && value >= 1)
                            {
                                c.accountBalance = n;
                                word = true;
                            }
                            else if (value > 2000)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("ATM doesn't support transactions greater than $ 2000. Please try again");
                            }
                            else if (isModulus > 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Enter amount in increments of $ 20. Please try again");
                            }
                            else if (n <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Insufficient funds. Please try again");
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Keypad error. Please try again");
                            }
                        }
                        valid = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("A random ATM crashed... GOOD BYE.");
                        Environment.Exit(0);
                        break;
                }
            }

            Processing();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n\t\t   Withdraw was successful.\n\n\t\tPlease collect cash from tray!");
            Console.ForegroundColor = ConsoleColor.White;

            Thread.Sleep(7000);

            int t = c.accountBalance; // For receipt purposes only.

            // Receipt Method, asks user if they would like a receipt for transaction.
            Console.Clear();
            Console.WriteLine("\n\n\n\t\tWould you like a receipt?\n\n\t\t    1.  Yes\n\n\t\t    2.  No");

            int amountWithdrew = l - t;

            bool again = false;
            while (!again)
            {
                int receipt = ConvertToInt();
                switch (receipt)
                {
                    case 1:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\n\n\n\t    $$$ A T M $$$");
                        Console.ForegroundColor = ConsoleColor.White;
                        DateTime now = DateTime.Now;
                        Console.WriteLine("\n\n\n\t{0}\n\n\tTransaction = Withdraw\n\n\tWithdraw Amount = $ {1}\n\n\tAccount Balance = $ {2}", now, amountWithdrew, c.accountBalance);

                        Thread.Sleep(7000);

                        Processing();

                        Console.WriteLine("\n\n\n\t\tPlease collect receipt.");
                        again = true;
                        break;
                    case 2:
                        again = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Keypad error. Please try again");
                        break;
                }
            }

            AnotherSelection(c);

        }

        // Check Balance Method, user is given current accountBalance. Receipt method.
        private static void CheckBalance(Customer c)
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\t\tAccount Balance is: $" + c.accountBalance);

            Thread.Sleep(7000);

            // Receipt Method, asks user if they would like a receipt for transaction.
            Console.Clear();
            Console.WriteLine("\n\n\n\t\tWould you like a receipt?\n\n\t\t    1.  Yes\n\n\t\t    2.  No");

            bool valid = false;
            while (!valid)
            {
                int receipt = ConvertToInt();
                switch (receipt)
                {
                    case 1:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("\n\n\n\t    $$$ A T M $$$");
                        Console.ForegroundColor = ConsoleColor.White;
                        DateTime now = DateTime.Now;
                        Console.WriteLine("\n\n\n\t{0}\n\n\tTransaction = Balance Inquiry\n\n\tAccount Balance = $ {1}", now, c.accountBalance);

                        Thread.Sleep(7000);

                        Processing();

                        Console.WriteLine("\n\n\n\t\tPlease collect receipt.");
                        valid = true;
                        break;
                    case 2:
                        valid = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Keypad error. Please try again");
                        break;
                }
            }

            AnotherSelection(c);
        }

        // Exit Method, when user intentionally ends application.
        private static void Exit()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\n\t\t    Please collect your card.\n\n\t\tThank you for using a random ATM:)\n\n\n\n\n\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(0);
        }

        // AnotherSelection Method, asks user if they would like to do anything else and initiates request.
        public static void AnotherSelection(Customer c)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n\n\t\tWould you like to make another selection?\n\n\t\t\t1. Yes\n\n\t\t\t2. No");
            bool valid = false;
            while (!valid)
            {
                int result = ConvertToInt();

                switch (result)
                {
                    case 1:
                        valid = true;
                        break;
                    case 2:
                        Exit();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("Keypad error. Please try again.");
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                }
            }
            int value = MenuOptions(c);
            MenuOptionsResults(value, c);
        }

        // Processing Method, displays processing screen using a for loop and sleep method.
        private static void Processing()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n\n\n\t\t\tProcessing");
            Console.Write("\n\n\t\t");
            for (int i = 0; i < 13; i++)
            {
                Console.Write(". ");
                Thread.Sleep(400);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
        }

        // TryParse Method, converts readline string to an integer. And beeps.
        public static int ConvertToInt()
        {
            int.TryParse(Console.ReadLine(), out int result);
            Console.Beep();
            return result;
        }
    }
}