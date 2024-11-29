namespace Project
{
    internal class Program
    {
        //.NET Delegates
        private static Predicate<string>? delStringIsNull;
        private static Action<string>? delStringIsOK;

        //Events
        private delegate string InputString();
        private static event InputString? EventInputString;

        private delegate int InputInt();
        private static event InputInt? EventInputInt;

        //Custom Delegate
        private delegate void DisplayError(string message);
        private static DisplayError? delStringError;

        static void Main()
        {
            int m;
            List<Customer> customers = [];
            delStringIsNull = IsNull;
            delStringIsOK = OutputOK;
            delStringError = OutputError;

            EventInputString += TextInputString;
            EventInputInt += TextInputInt;

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("At any point in the program, type \"back\" (for text) or \"0\" (for numbers) to exit the menu.\n");
            Console.ResetColor();

            do
            {
                Console.Write(InputMainMenu());
                m = CheckMainMenu();
                Console.WriteLine("\n");

                switch (m)
                {
                    case 0:
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("Exiting...");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;

                    case 1:
                        Customer customer = SignIn(customers);
                        if (customer != null)
                            CusOperation(ref customer);
                        break;

                    case 2:
                        SignUp(ref customers);
                        break;
                }
            } while (true);

        }

        private static string TextInputString()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string s = Console.ReadLine()!;
            Console.ResetColor();
            return s;
        }

        private static double TextInputDouble()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            double d = double.Parse(Console.ReadLine()!);
            Console.ResetColor();
            return d;
        }

        private static int TextInputInt()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            int i = int.Parse(Console.ReadLine()!);
            Console.ResetColor();
            return i;
        }

        private static void OutputError(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message + "\n");
            Console.ResetColor();
        }

        private static void OutputOK(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(message + "\n");
            Console.ResetColor();
        }

        private static string InputMainMenu()
        {
            string menu = "╔════ MENU ════╗\n" +
                          "║ 1 - Sign in  ║\n" +
                          "║ 2 - Sign up  ║\n" +
                          "║ 0 - Exit     ║\n" +
                          "╚══════════════╝\n" +
                          "--> ";
            return menu;
        }

        private static string InputCusMenu()
        {
            string menu = "╔════ CUS MENU ════╗\n" +
                          "║ 1 - Create order ║\n" +
                          "║ 2 - Find order   ║\n" +
                          "║ 3 - Put money    ║\n" +
                          "║ 4 - Get info     ║\n" +
                          "║ 5 - Cards info   ║\n" +
                          "║ 0 - Back         ║\n" +
                          "╚══════════════════╝\n" +
                          "--> ";
            return menu;
        }

        private static string InputOrderMenu()
        {
            string menu = "╔══════ ORDER MENU ══════╗\n" +
                          "║ 1 - Add products       ║\n" +
                          "║ 2 - Remove product     ║\n" +
                          "║ 3 - Buy order          ║\n" +
                          "║ 4 - Delete order       ║\n" +
                          "║ 0 - Back               ║\n" +
                          "╚════════════════════════╝\n" +
                          "--> ";
            return menu;
        }

        private static int CheckMainMenu()
        {
            ConsoleKeyInfo x = Console.ReadKey();
            while ((!char.IsDigit(x.KeyChar)) || (Convert.ToInt32(x.KeyChar.ToString()) < 0) || (Convert.ToInt32(x.KeyChar.ToString()) > 2))
            {
                if (x.Key == ConsoleKey.Enter)
                    Console.Write("--> ");
                else
                    Console.Write("\b \b");
                x = Console.ReadKey();
            }
            return Convert.ToInt32(x.KeyChar.ToString());
        }

        private static int CheckCusMenu()
        {
            ConsoleKeyInfo x = Console.ReadKey();
            while ((!char.IsDigit(x.KeyChar)) || (Convert.ToInt32(x.KeyChar.ToString()) < 0) || (Convert.ToInt32(x.KeyChar.ToString()) > 5))
            {
                if (x.Key == ConsoleKey.Enter)
                    Console.Write("--> ");
                else
                    Console.Write("\b \b");
                x = Console.ReadKey();
            }
            return Convert.ToInt32(x.KeyChar.ToString());
        }

        private static int CheckOrderMenu()
        {
            ConsoleKeyInfo x = Console.ReadKey();
            while ((!char.IsDigit(x.KeyChar)) || (Convert.ToInt32(x.KeyChar.ToString()) < 0) || (Convert.ToInt32(x.KeyChar.ToString()) > 4))
            {
                if (x.Key == ConsoleKey.Enter)
                    Console.Write("--> ");
                else
                    Console.Write("\b \b");
                x = Console.ReadKey();
            }
            return Convert.ToInt32(x.KeyChar.ToString());
        }

        private static Customer SignIn(List<Customer> customers) //PREDICATE!!!
        {
            bool repeat;
            Console.WriteLine("Sign in account\n────────────────");
            string name;
            do
            {
                Console.Write("Name: ");
                name = EventInputString!.Invoke();
                repeat = false;
                if (delStringIsNull!.Invoke(name) || !Customer.regex.IsMatch(name)) //!!!
                {
                    delStringError!.Invoke("Inncorrect NAME format!");
                    repeat = true;
                }
                else if (name.Equals("back", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine();
                    return null!;
                }
            } while (repeat);
            var customer = customers.Find(c => c != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (customer == null)
                delStringError!.Invoke("Customer was not found!");
            else
                delStringIsOK!.Invoke($"Welcome back, {customer.Name}!");

            return customer!;
        }

        private static void SignUp(ref List<Customer> customers)
        {
            bool repeat;
            Console.WriteLine("Create account\n───────────────");
            do
            {
                try
                {
                    Console.Write("Name: ");
                    string name = EventInputString!.Invoke();
                    if (name.Equals("back", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine();
                        return;
                    }

                    Console.Write("Money balance: ");
                    double money = TextInputDouble();
                    if (money == 0)
                    {
                        Console.WriteLine();
                        return;
                    }

                    if (customers.Any(c => c.Name == name))
                        throw new FormatException("This name has already existed!");

                    customers.Add(new Customer(name!, money));

                    delStringIsOK?.Invoke("Account was created!");
                    repeat = false;
                }
                catch (FormatException ex)
                {
                    delStringError!.Invoke(ex.Message);
                    repeat = true;
                }
            } while (repeat);
        }

        private static void CusOperation(ref Customer customer)
        {
            bool repeat;
            do
            {
                Console.Write(InputCusMenu());
                int m = CheckCusMenu();
                Console.WriteLine("\n");

                switch (m)
                {
                    case 0:
                        return;

                    case 1:
                        Order order = new(customer);
                        int c = 0, count = 0;
                        while (!BackOrNo())
                        {
                            Product product = CreateProduct(ref count);
                            order.AddProduct(product, count);
                            c++;
                        }
                        if (c != 0)
                        {
                            customer.CreateOrder(order);
                            delStringIsOK?.Invoke("\nOrder was created!");
                        }
                        else
                        {
                            order = null!;
                            Order.ChangeNumer();
                            delStringError!.Invoke("\nOrder was NOT created!");
                        }
                        break;


                    case 2:
                        if (customer.Orders.Count == 0)
                        {
                            delStringError!.Invoke("You do NOT have orders!");
                            break;
                        }
                        int n = 0;
                        do
                        {
                            repeat = false;
                            Console.Write("Order number to find: ");
                            try
                            {
                                n = EventInputInt!.Invoke();
                            }
                            catch (Exception ex) { delStringError!.Invoke(ex.Message); repeat = true; }
                        } while (repeat);
                        Order temp = customer.FindOrder(n);
                        if (temp == null)
                            delStringError!.Invoke("Order was NOT found!");
                        else
                            OrderOperation(temp, customer);
                        break;

                    case 3:
                        do
                        {
                            try
                            {
                                Console.Write("Amount to put on balance: ");
                                int money = EventInputInt!.Invoke();
                                repeat = false;

                                if (!customer.PutMoney(money))
                                    throw new Exception("Amount cannot be negative");
                                else
                                    delStringIsOK?.Invoke("Balance replenished!");
                            }
                            catch (Exception ex)
                            {
                                delStringError!.Invoke(ex.Message);
                                repeat = true;
                            }
                        } while (repeat);
                        break;

                    case 4:
                        Console.WriteLine(customer.GetInfo());
                        Console.WriteLine();
                        break;

                    case 5:
                        string str = "┌─────────────────────────────┐\n" +
                                     "│ Discount Cards Info         │\n" +
                                     "├─────────────────────────────┤\n" +
                                     "│ White Card  -  9% (Default) │\n" +
                                     "│ Black Card  - 13% (2 Orders)│\n" +
                                     "│ Gold Card   - 22% (5 Orders)│\n" +
                                     "└─────────────────────────────┘";

                        Console.WriteLine(str);
                        Console.WriteLine();
                        break;
                }
            } while (true);
        }

        private static Product CreateProduct(ref int count)
        {
            bool repeat;
            Product product = null!;
            Console.WriteLine("\nTypes of products:");
            foreach (var type in Enum.GetValues(typeof(ProductType)))
                Console.WriteLine($" {(int)type}) " + type);
            ProductType t = 0;
            do
            {
                try
                {
                    repeat = false;
                    Console.Write("Type: ");
                    string s = EventInputString!.Invoke();
                    s = s!.ToUpper();
                    t = (ProductType)Enum.Parse(typeof(ProductType), s);

                    if (!Enum.IsDefined(t)) delStringError!.Invoke("Incorrect type!");
                }
                catch (Exception ex) { delStringError!.Invoke(ex.Message); repeat = true; }
            }
            while (!Enum.IsDefined(t) || repeat);

            Console.WriteLine();

            do
            {
                try
                {
                    repeat = false;

                    Console.Write("Product name: ");
                    string name = EventInputString!.Invoke();

                    Console.Write("\nPrice: ");
                    double price = TextInputDouble();

                    product = new Product(name, price, t);
                }
                catch (Exception ex) { delStringError!.Invoke(ex.Message); repeat = true; }
            } while (repeat);

            do
            {
                repeat = false;
                Console.Write("\nCount: ");
                try
                {
                    count = EventInputInt!.Invoke();
                }
                catch (Exception ex) { delStringError!.Invoke(ex.Message); repeat = true; }
            } while (repeat);

            delStringIsOK?.Invoke("\nProduct was added to order!");

            return product;
        }

        private static bool BackOrNo()
        {
            bool repeat;
            do
            {
                try
                {
                    repeat = false;
                    Console.Write("Back? (Yes/No): ");
                    string back = EventInputString!.Invoke();
                    if (back == null)
                        throw new Exception("Input yes or no.");
                    else if (back.Equals("yes", StringComparison.OrdinalIgnoreCase))
                        return true;
                    else if (back.Equals("no", StringComparison.OrdinalIgnoreCase))
                        return false;
                    else
                        throw new Exception("Input yes or no.");
                }
                catch (Exception ex) { delStringError!.Invoke(ex.Message); repeat = true; }
            } while (repeat);
            return false;
        }

        private static void OrderOperation(Order order, Customer customer)
        {
            delStringIsOK?.Invoke($"Order #{order.Number:D3} was found!");
            int count = 0;
            bool repeat;
            string name = null!;
            do
            {
                Console.Write(InputOrderMenu());
                int m = CheckOrderMenu();
                Console.WriteLine("\n");

                switch (m)
                {
                    case 0:
                        return;

                    case 1:
                        if (order.Status == Status.BOUGHT)
                        {
                            delStringError!.Invoke("Order was bought. Impossible to add a product!");
                            break;
                        }
                        while (!BackOrNo())
                        {
                            Product product = CreateProduct(ref count);
                            order.AddProduct(product, count);
                        }
                        Console.WriteLine();
                        break;

                    case 2:
                        if (order.Status == Status.BOUGHT)
                        {
                            delStringError!.Invoke("Order was bought. Impossible to remove a product!");
                            break;
                        }
                        do
                        {
                            try
                            {
                                repeat = false;

                                Console.Write("Product name: ");
                                name = EventInputString!.Invoke();
                            }
                            catch (Exception ex) { delStringError!.Invoke(ex.Message); repeat = true; }
                        } while (repeat);
                        Product temp = order.Products.Find(p => p.Name == name)!;
                        if (temp != null)
                        {
                            order.RemoveProduct(temp);
                            delStringIsOK?.Invoke("Product was deleted from order.");
                        }
                        else
                            delStringError!.Invoke("Product was NOT found!");
                        if (order.Products.Count == 0)
                        {
                            customer.DeleteOrder(order);
                            delStringError!.Invoke($"Your order #{order.Number:D3} was deleted (does not contain any products)!");
                            return;
                        }
                        break;

                    case 3:
                        string str = "white";
                        if (customer.BuyOrder(order, ref str))
                        {
                            delStringIsOK?.Invoke("Order was bought. Thank you!");
                            if (str != "white")
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine($"You unblock {str.ToUpper()} CARD with discount {customer.Card.Percent}%!!!\n");
                                Console.ResetColor();
                            }
                        }
                        else
                            delStringError!.Invoke("You can not buy this order!");
                        break;

                    case 4:
                        if (customer.DeleteOrder(order))
                        {
                            delStringIsOK?.Invoke("Order was deleted.");
                            return;
                        }
                        else
                            delStringError!.Invoke("You can not delete this order!");
                        break;
                }
            } while (true);
        }

        private static bool IsNull(string str) => str == null;
    }
}
