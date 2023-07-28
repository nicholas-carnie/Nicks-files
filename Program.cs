using System;
using System.Net.Mail;
using System.Collections.Generic;

namespace Assignment_3_Nicholas_Carnie                          // Assign the namespace
{
    public class Bank                                           // Assign the class
    {
        private object[] user_info = new object[7];             // Creates private array with 7 elements. Object array can store any type of variable.
        private Dictionary<string, string> userDetails;         // Creates private list that stores users username/password
        private Dictionary<string, double> userMoney;           // Store user accounts: username -> balance

        static int deposit_limit = 0;
        static int withdraw_limit = 0;

        public Bank()                                           // Creates a constructor for the Bank class
        {

            userDetails = new Dictionary<string, string>();     // Sets initial values for dictionary userDetails
            userMoney = new Dictionary<string, double>();
        }

        public void Login()                                     // Public method - Login
        {
            withdraw_limit = 0;                                 // Resets withdraw limit
            deposit_limit = 0;                                  // Resets deposit limit

            Console.WriteLine("Please enter the username: ");   // Call WriteLine method for console object. Prompts user input
            var username = Console.ReadLine();                  // Assign string variable from user input.
            Console.WriteLine("Please enter the password: ");   // Call WriteLine method for console object. Prompts user input
            var password = Console.ReadLine();                  // Assign string variable from user input.
            bool login_match = false;                           // Assign Boolean variable
            int i = 0;                                          // Assign interger variable. Set initial value to 0. Used for max login attempts.

            while (login_match == false)                        // while loop. Continues while boolean variable equals false
            {
                if (userDetails.TryGetValue(username, out string dictionaryPassword) && dictionaryPassword == password)   // if else statement. Checks if username/password combination is correct.
                {
                    login_match = true;                         // while loop exit. Returns true when username/password combination is valid.
                    Welcome(username);                          // Returns Welcome method with username as a parameter
                }
                else                                            // Else condition if no other conditions true.
                {
                    login_match = false;                        // If false, starts while loop again
                    Console.WriteLine("Username and/or password doesn't match or invalid"); // Call WriteLine method for console object. Informs user incorrect entry
                    Console.WriteLine("Please enter the username: ");   // Call WriteLine method for console object. Prompts user input
                    username = Console.ReadLine();              // Asks user input again
                    Console.WriteLine("Please enter the password: ");   // Call WriteLine method for console object. Prompts user input
                    password = Console.ReadLine();              // Asks user input again
                    i++;                                        // Increases login attempt number
                    if (i == 2)                                 // Nested if statement. Entry when max 3 attempts reached.
                    {
                        Console.WriteLine("Sorry, only a maximum of 3 entries allowed!");   // Call WriteLine method for console object. Inform user max attempts reached. Gives input options which are evaluated from Invalid_Login method.
                        login_match = true;                     // while loop exit. Returns true when login fails 3 times
                        Invalid_Login();                        // Returns Invalid Login method
                    }
                }
            }
        }

        private void Welcome(string username)                   // Private method - Welcome with username as parameter
        {
            Console.WriteLine("\nWelcome {0}\n" +               // Call WriteLine method for console object. Seperates onto different lines.
                            "1: View Balance\n" +
                            "2: Deposit\n" +
                            "3: Withdraw\n" +
                            "4: Transfer\n" +
                            "5: Main Menu\n" +
                            "6: Quit\n", username);             // Takes the username from userDetails list & inputs it in the Welcome message.
            Console.Write("Select Option: ");                   // Call Write method for console object. Asks user input
            var W_choice = (Console.ReadLine());                // Assign Var variable from user input.
            int W_choice_int;                                   // Assign Int variable

            if (int.TryParse(W_choice, out W_choice_int))       // if / else statement with Try Parse condition. Enters if successful conversion to integer
            {
                // Nested if / else if / else statement
                if (W_choice_int == 1)                          // If condition if user enters 1
                {
                    View_Balance(username);                     // Returns View_Balance method with username as a parameter
                }
                else if (W_choice_int == 2)                     // else if condition if user enters 2
                {
                    Deposit(username);                          // Returns Deposit method with username as a parameter
                }
                else if (W_choice_int == 3)                     // else if condition if user enters 3
                {
                    Withdraw(username);                         // Returns Withdraw method with username as a parameter
                }
                else if (W_choice_int == 4)                     // else if condition if user enters 4
                {
                    Transfer(username);                         // Returns Transfer method with username as a parameter
                }
                else if (W_choice_int == 5)                     // else if condition if user enters 5
                {
                    // Exits method & returns to Main method while loop
                }
                else if (W_choice_int == 6)                     // else if condition if user enters 6
                {
                    Quit();                                     // Returns Quit method
                }
                else                                            // Else condition if user doesn't enter any number 1-6
                {
                    Console.WriteLine("Not an option! Please enter either 1, 2, 3, 4, 5 or 6.\n");  // Call WriteLine method for console object. Informs user incorrect input
                    Welcome(username);                          // Returns Welcome method with username as a parameter               
                }
            }
            else                                                // Else condition if TryParse not successful.
            {
                Console.WriteLine("Not an option! Please enter either 1, 2, 3, 4, 5 or 6.\n"); // Call WriteLine method for console object. Informs user incorrect input
                Welcome(username);                              // Returns Welcome method with username as a parameter                     
            }
        }

        public void View_Balance(string username)               // Public method - View_Balance with username as future parameter
        {
            Console.WriteLine($"Your balance is: {userMoney[username]:C}"); // Call WriteLine method for console object. Informs user option not ready & to return to Home screen.
            Console.WriteLine("Press any key to return to Home");
            Console.ReadKey();                                  // Call Readkey method from Console object. Requires keyboard input to progress.
            Console.WriteLine();                                // Adds line between Readkey user input & Welcome method
            Welcome(username);                                  // Returns Welcome method with username as a parameter
        }

        public void Deposit(string username)                    // Public method - Deposit with username as a future parameter
        {
            if (deposit_limit == 3)
            {
                Console.WriteLine("Sorry, only maximum 3 deposits per login\n");
                return;                                         // Returns Welcome method with username as a parameter
            }

            Console.WriteLine("Enter amount to deposit:");      // Call WriteLine method for console object. Informs user option not ready & to return to Home screen.
            var deposit = Console.ReadLine();
            bool deposit_check = false;
            float deposit_float;

            while (deposit_check == false)
            {
                if (float.TryParse(deposit, out deposit_float) && deposit_float >= 0)
                {
                    deposit_limit++;
                    userMoney[username] += deposit_float;
                    deposit_check = true;
                    Console.WriteLine($"Thank you for your deposit of {deposit_float:C}. Your updated balance is {userMoney[username]:C} Please choose an option below: ");
                    Console.WriteLine();                        // Adds line between Readkey user input & Deposit_Choice method
                    Deposit_Choice(username);                   // Returns Deposit_Choice method
                }
                else
                {
                    Console.WriteLine("Error! Only non-negative numerical data to be entered for deposit");
                    deposit_check = false;
                    deposit = Console.ReadLine();
                }
            }
        }

        public void Deposit_Choice(string username)
        {

            Console.WriteLine("1: Deposit\n" +
                                "2: Return to Home\n" +
                                "3: Exit\n");
            Console.Write("Select Option: ");

            var Dep_choice = Console.ReadLine();                // Assign Var variable from user input.
            int Dep_choice_int;                                 // Assign Int variable

            if (int.TryParse(Dep_choice, out Dep_choice_int))   // if / else statement with Try Parse condition. Enters if successful conversion to integer
            {
                // Nested if / else if / else statement
                if (Dep_choice_int == 1)                        // If condition if user enters 1
                {
                    Deposit(username);                          // Returns Deposit method
                }
                else if (Dep_choice_int == 2)                   // else if condition if user enters 2
                {
                    Welcome(username);                          // Returns Welcome method
                }
                else if (Dep_choice_int == 3)                   // else if condition if user enters 3
                {
                    Quit();                                     // Returns Quit method
                }
                else                                            // Else condition if no other conditions true.
                {
                    Console.WriteLine("Not an option! Please enter either 1, 2 or 3\n"); // Call WriteLine method for console object. Informs user incorrect input
                    Deposit_Choice(username);                   // Returns Deposit_Choice method
                }
            }
            else                                                // Else condition if no other conditions true.
            {
                Console.WriteLine("Not an option! Please enter either 1, 2 or 3\n"); // Call WriteLine method for console object. Informs user incorrect input
                Deposit_Choice(username);                       // Returns Deposit_Choice method
            }
        }

        public void Withdraw(string username)                   // Public method - Withdraw with username as future parameter
        {
            if (withdraw_limit == 3)
            {
                Console.WriteLine("Sorry, only maximum 3 withdrawals per login\n");
                return;
            }

            Console.WriteLine("Enter amount to Withdraw:");     // Call WriteLine method for console object. Informs user option not ready & to return to Home screen.
            var withdraw = Console.ReadLine();
            bool withdraw_check = false;
            float withdraw_float;

            while (withdraw_check == false)
            {
                if (float.TryParse(withdraw, out withdraw_float) && withdraw_float >= 0)
                {
                    if (withdraw_float <= userMoney[username])
                    {
                        withdraw_limit++;
                        userMoney[username] -= withdraw_float;
                        withdraw_check = true;
                        Console.WriteLine($"Thank you for your withdrawal of {withdraw_float:C}. Your updated balance is {userMoney[username]:C}. Please choose an option below:");
                        Console.WriteLine();                    // Adds line between Readkey user input & Withdraw_Choice method
                        Withdraw_Choice(username);              // Returns Withdraw_Choice method
                    }
                    else
                    {
                        Console.WriteLine("Error! Not sufficient fund available");
                        withdraw_check = false;
                        withdraw = Console.ReadLine();
                    }
                }
                else
                {
                    Console.WriteLine("Error! Only non-negative numerical data to be entered for withdrawal");
                    withdraw_check = false;
                    withdraw = Console.ReadLine();
                }
            }
        }

        public void Withdraw_Choice(string username)
        {
            Console.WriteLine("1: Withdraw\n" +
                                "2: Return to Home\n" +
                                "3: Exit\n");
            Console.Write("Select Option: ");



            var with_choice = (Console.ReadLine());             // Assign Var variable from user input.
            float with_choice_int;                              // Assign Int variable

            if (float.TryParse(with_choice, out with_choice_int))   // if / else if / else statement with Try Parse condition. Enters if successful conversion to integer
            {
                // Nested if / else if / else statement
                if (with_choice_int == 1)                       // If condition if user enters 1
                {
                    Withdraw(username);                         // Returns Withdraw method
                }
                else if (with_choice_int == 2)                  // else if condition if user enters 2
                {
                    Welcome(username);                          // Returns Welcome method
                }
                else if (with_choice_int == 3)                  // else if condition if user enters 3
                {
                    Quit();                                     // Returns Quit method
                }
                else                                            // Else condition if no other conditions true.
                {
                    Console.WriteLine("Not an option! Please enter either 1, 2 or 3\n"); // Call WriteLine method for console object. Informs user incorrect input
                    Withdraw_Choice(username);                  // Returns Withdraw_Choice method
                }
            }
            else                                                // Else condition if no other conditions true.
            {
                Console.WriteLine("Not an option! Please enter either 1, 2 or 3\n"); // Call WriteLine method for console object. Informs user incorrect input
                Withdraw_Choice(username);                      // Returns Withdraw_Choice method
            }
        }

        public void Transfer(string username)                   // Public method - Transfer with username as future parameter
        {
            Console.WriteLine("Still under construction! Press any key to return to Home."); // Call WriteLine method for console object. Informs user option not ready & to return to Home screen.
            Console.ReadKey();                                  // Call Readkey method from Console object. Requires keyboard input to progress.
            Console.WriteLine();                                // Adds line between Readkey user input & Welcome method
            Welcome(username);                                  // Returns Welcome method
        }

        public void Invalid_Login()                             // Public method - Invalid_Login
        {
            Console.WriteLine("Invalid email and/or password\n" +   // Call WriteLine method for console object. Seperates onto different lines. Gives user options.
                            "1: Try again\n" +
                            "2: Main Menu\n" +
                            "3: Quit\n");
            Console.Write("Select Option: ");                   // Call Write method for console object. Asks user input

            var Inv_choice = (Console.ReadLine());              // Assign Var variable from user input.
            int Inv_choice_int;                                 // Assign Int variable

            if (int.TryParse(Inv_choice, out Inv_choice_int))   // if / else statement with Try Parse condition. Enters if successful conversion to integer
            {
                // Nested if / else if / else statement
                if (Inv_choice_int == 1)                        // If condition if user enters 1
                {
                    Login();                                    // Returns Login method
                }
                else if (Inv_choice_int == 2)                   // else if condition if user enters 2
                {
                    // Exits method & returns to Main method while loop
                }
                else if (Inv_choice_int == 3)                   // else if condition if user enters 3
                {
                    Quit();                                     // Returns Quit method
                }
                else                                            // Else condition if user doesn't enter any interger between 1 & 3.
                {
                    Console.WriteLine("Not an option! Please enter either 1, 2 or 3\n"); // Call WriteLine method for console object. Informs user incorrect input
                    Invalid_Login();                            // Returns Invalid Login method
                }
            }
            else                                                // Else condition if TryParse not successful.
            {
                Console.WriteLine("Not an option! Please enter either 1, 2 or 3\n"); // Call WriteLine method for console object. Informs user incorrect input
                Invalid_Login();                                // Returns Invalid Login method
            }
        }

        public void Signup()                                    // Public method - Signup
        {
            Console.WriteLine("What is your first name?");      // Call WriteLine method for console object. Asks user First name
            var name_First = Console.ReadLine();                // Assign Var variable from user input.
            bool name_First_check = false;                      // Assign Boolean variable

            while (name_First_check == false)                   // while loop. Continues while boolean variable equals false
            {
                if (string.IsNullOrEmpty(name_First))           // if / else statement. Checks if user enters nothing
                {
                    Console.WriteLine("Name is empty!");        // Call WriteLine method for console object. Informs user no input
                    name_First_check = false;                   // If false, starts while loop again
                    name_First = Console.ReadLine();            // Asks user input again
                }
                else                                            // Else condition if name_First isn't Null
                {
                    name_First_check = true;                    // while loop exit. Returns true when input is not empty
                    user_info[0] = name_First;                  // Assigns variable to 1st position of array user_info
                }
            }

            Console.WriteLine("What is your last name?");       // Call WriteLine method for console object. Asks user Last name
            var name_Last = Console.ReadLine();                 // Assign Var variable from user input.
            bool name_Last_check = false;                       // Assign Boolean variable

            while (name_Last_check == false)                    // while loop. Continues while boolean variable equals false
            {

                if (string.IsNullOrEmpty(name_Last))            // if / else statement. Checks if user enters nothing
                {
                    Console.WriteLine("Name is empty!");        // Call WriteLine method for console object. Informs user no input
                    name_Last_check = false;                    // If false, starts while loop again
                    name_Last = Console.ReadLine();             // Asks user input again
                }
                else                                            // Else condition if name_Last isn't Null
                {
                    name_Last_check = true;                     // while loop exit. Returns true when input is not empty
                    user_info[1] = name_Last;                   // Assigns variable to 2nd position of array user_info
                }
            }

            Console.WriteLine("Enter a username. Usernames must be a minimum of 8 characters & cannot be used already: ");   // Call WriteLine method for console object. Asks user input. Informs user input condition.
            var username = Console.ReadLine();                  // Assign Var variable from user input.
            bool username_check = false;                        // Assign Boolean variable

            while (username_check == false)                     // while loop. Continues when boolean variable equals false
            {
                if (userDetails.ContainsKey(username))          // Uses ContainKey method to check if username already exists in userDetails list
                {
                    Console.WriteLine("Error, username already exists"); // Call WriteLine method for console object. Informs user username already exists
                    username_check = false;                     // If false, starts while loop again
                    username = Console.ReadLine();              // Asks user input again
                }
                else if (username?.Length < 8)                  // else if statement. Entry when legth of username is less than 8.
                {
                    username_check = false;                     // If false, starts while loop again
                    Console.WriteLine("Error, username must be a minimum of 8 characters"); // Call WriteLine method for console object. Informs user input error
                    username = Console.ReadLine();              // Asks user input again
                }
                else                                            // else condition. Entry when above conditions are false. 
                {
                    username_check = true;                      // while loop exit. Returns true when conditions satisfied
                    user_info[2] = username;                    // Assigns variable to 3rd position of array user_info
                    userMoney.Add(username, 0);                 // Initialize the user's account with a balance of 0
                }
            }

            Console.WriteLine("Enter email: ");                 // Call WriteLine method for console object. Asks user input
            var email = Console.ReadLine();                     // Assign Var variable from user input.
            bool email_check = false;                           // Assign Boolean variable

            while (email_check == false)                        // while loop. Continues while boolean variable equals false
            {
                if (MailAddress.TryCreate(email, out var email_valid))  //TryCreate method with MailAddress object. Checks if entered input is valid email address
                {
                    email_check = true;                         // while loop exit. Returns true when user enters valid email
                    user_info[3] = email_valid;                 // Assigns variable to 4th position of array user_info  
                }
                else                                            // Else condition if no other conditions true.
                {
                    Console.WriteLine("Error! Enter a valid email address");    // Call WriteLine method for console object. Informs user input error
                    email_check = false;                        // If false, starts while loop again
                    email = Console.ReadLine();                 // Asks user input again
                }
            }

            Console.WriteLine("Enter age: ");                   // Call WriteLine method for console object. Asks user input
            var age = Console.ReadLine();                       // Assign Var variable from user input.  
            int age_int;                                        // Assign Int variable
            bool age_check = false;                             // Assign Boolean variable

            while (age_check == false)                          // while loop. Continues while boolean variable equals false
            {
                if (int.TryParse(age, out age_int) && age_int >= 18 && age_int <= 70)    // if / else statement with Try Parse condition. Also age range condition (Must be between 18 & 70).
                {
                    age_check = true;                           // while loop exit. Returns true when above conditions are met
                    user_info[4] = age_int;                     // Assigns variable to 5th position of array user_info   
                }
                else                                            // Else condition if above conditions false.
                {
                    Console.WriteLine("Error! Age must be of an interger type value & be between 18 & 70.");     // Call WriteLine method for console object. Informs user input error
                    age_check = false;                          // If false, starts while loop again
                    age = Console.ReadLine();                   // Asks user input again
                }
            }

            Console.WriteLine("Enter phone number. Do not insert any spaces between numbers: ");          // Call WriteLine method for console object. Asks user input
            var phone = Console.ReadLine();                     // Assign Var variable from user input.
            double phone_double;                                // Assigns double variable
            bool phone_check = false;                           // Assign Boolean variable

            while (phone_check == false)                        // while loop. Continues while boolean variable equals false
            {
                if (double.TryParse(phone, out phone_double) && phone_double > 9999999 && phone_double < 999999999999999)     // if / else statement with Try Parse condition if converted to integer. Also phone number digit range between 8 & 15 numbers
                {
                    phone_check = true;                         // while loop exit. Returns true when above conditions are met
                    user_info[5] = phone_double;                // Assigns variable to 6th position of array user_info   
                }
                else                                            // Else condition if above conditions false.
                {
                    Console.WriteLine("Error! Phone number must be of an interger type, be between 8 & 15 numbers & have no spaces.");    // Call WriteLine method for console object. Informs user input error
                    phone_check = false;                        // If false, starts while loop again
                    phone = Console.ReadLine();                 // Asks user input again
                }
            }

            Console.WriteLine(
                "Please insert your password.Passwords must contain:\n" +   // Call WriteLine method for console object. Seperates onto different lines. User prompt for password. Details password criteria.
                "Minimum 8 Letters\n" +
                "Minimum 1 Capital letter\n" +
                "Minimum 1 Number");

            var password = Console.ReadLine();                  // Assign Var variable from user input.
            bool password_check = false;                        // Assign Boolean variable

            while (password_check == false)                     // while loop. Continues while boolean variable equals false
            {
                if (string.IsNullOrEmpty(password))             // if statement. Checks if user enters nothing
                {
                    Console.WriteLine("Password is empty!");    // Call WriteLine method for console object. Informs user input error
                    password_check = false;                     // If false, starts while loop again
                    password = Console.ReadLine();              // Asks user input again
                }
                else if (password.Length < 8)                   // else if condition. Checks minimum length
                {
                    Console.WriteLine($"Your password is too short, it contains {password.Length} characters!");    // Call WriteLine method for console object. Informs user input error
                    password_check = false;                     // If false, starts while loop again
                    password = Console.ReadLine();              // Asks user input again
                }
                else if (!password.Any(char.IsNumber))          // else if condition. Checks if a number is present
                {
                    Console.WriteLine("Your password doesn't contain a number!");   // Call WriteLine method for console object. Informs user input error
                    password_check = false;                     // If false, starts while loop again
                    password = Console.ReadLine();              // Asks user input again
                }
                else if (!password.Any(char.IsUpper))           // else if condition. Checks for an uppercase letter
                {
                    Console.WriteLine("Your password doesn't contain an uppercase letter!");    // Call WriteLine method for console object. Informs user input error
                    password_check = false;                     // If false, starts while loop again
                    password = Console.ReadLine();              // Asks user input again
                }
                else                                            // Else condition if password criteria met
                {
                    Console.WriteLine("Your password is valid!");   // Call WriteLine method for console object. Confirms password validity
                    password_check = true;                      // while loop exit. Returns true when above conditions are met
                }
            }

            // Check password 2 more times. Ask user to enter password 2nd time.

            Console.WriteLine("Enter password 2nd time:");         // Call WriteLine method for console object. Asks user input password 2nd time
            var password2 = Console.ReadLine();                 // Assign Var variable from user input.
            bool password_check2 = false;                       // Assign Boolean variable
            bool password_check3 = false;                       // Assign Boolean variable

            while (password_check2 == false)                    // while loop. Continues while boolean variable equals false
            {
                if (password != password2)                      // if statement with not equal condition. Enters if 2nd password entry != 1st password entry
                {
                    Console.WriteLine("Your passwords do not match(2nd). Please enter password again:"); // Call WriteLine method for console object. Informs user input error
                    password_check2 = false;                    // If false, starts while loop again
                    password2 = Console.ReadLine();             // Assign Var variable from user input.
                }
                else                                            // Else condition. Enters if 2nd password entry = 1st password entry
                {
                    Console.WriteLine("Enter password 3rd time:");    // Call WriteLine method for console object. Asks user input password 3rd time
                    var password3 = Console.ReadLine();         // Assign Var variable from user input.

                    while (password_check3 == false)            // nested while loop. Continues while boolean variable equals false
                    {
                        if (password != password3)              // if statement with not equal condition. Enters if 3rd password entry != 1st password entry
                        {
                            Console.WriteLine("Your passwords do not match(3rd). Please enter password again");  // Call WriteLine method for console object. Informs user input error
                            password_check3 = false;            // If false, starts while loop again
                            password3 = Console.ReadLine();     // Assign Var variable from user input.
                        }
                        else                                    // Else condition. Enters if 3rd password entry = 1st password entry
                        {
                            Console.WriteLine("Congratulations, you have set a strong pasword!"); // Call WriteLine method for console object. Confirms password has been set.
                            user_info[6] = password;            // Assigns variable to 7th position of array user_info
                            userDetails.Add(username, password);  // Adds username/password combination to userDetails list
                            password_check2 = true;             // while loop exit. Returns true when password set
                            password_check3 = true;             // while loop exit. Returns true when password set
                            Console.WriteLine("Press any key to return to Main Menu"); // Call WriteLine method for console object. Asks user to enter 'Any Key' to return to Main Menu
                            Console.ReadKey();                  // Call Readkey method from Console object. Requires keyboard input to progress.
                            Console.WriteLine();                // Adds line between Readkey user input & Main method
                        }
                    }
                }
            }
        }

        public void Quit()                                      // Public method - Quit
        {
            Console.WriteLine("Goodbye!");                      // Call WriteLine method for console object. Goodbye statement
            Console.WriteLine("Press any key to close program!");   // Call WriteLine method for console object. Asks user to enter 'Any Key' to exit
            Console.ReadKey();                                  // Call Readkey method from Console object. Requires keyboard input to progress.
            Environment.Exit(0);                                // Exit method from Environment Console object.
        }
    }

    public class Program                                        // Program class to keep Main method in it's own 'folder'
    {
        public static void Main(string[] args)                  // Entry point of the program with method 'Main'. It is 'void' so has no return value.
        {
            Bank bank = new Bank();                             // Calls Bank constructor . Creates bank object.
            bool quit = false;                                  // Assign Boolean variable

            while (quit == false)                               // while loop. Continues while boolean variable equals false
            {
                Console.WriteLine("Welcome to Nick's Bank\n" +     // Call WriteLine method for console object. Seperates onto different lines. Welcome statement & options. 
                "1: Login\n" +
                "2: Signup\n" +
                "3: Quit\n");
                Console.Write("Select Option: ");               // Call Write method for console object. Asks user input
                string option = Console.ReadLine();             // Assign string variable from user input.

                switch (option)                                 // Switch/case/default statement with option expression. 
                {
                    case "1":                                   // Entry when option = 1
                        bank.Login();                           // Returns Login method of bank object
                        break;                                  // Stops code execution when case condition is met

                    case "2":                                   // Entry when option = 1
                        bank.Signup();                          // Returns Signup method of bank object
                        break;                                  // Stops code execution when case condition is met

                    case "3":                                   // Entry when option = 1
                        quit = true;                            // While loop exit. Returns true when user input option = 3
                        bank.Quit();                            // Returns Quit method of bank object
                        break;                                  // Stops code execution when case condition is met

                    default:                                    // Default if no case conditions are met
                        Console.WriteLine("Incorrect choice! Please enter either 1, 2 or 3."); // Call WriteLine method for console object. Error message.
                        break;                                  // Stops code execution when no conditions are met
                }
            }
        }
    }
}
