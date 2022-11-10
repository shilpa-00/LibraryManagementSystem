using System;
using demoProject;
using System.Data.SqlClient;
using System.Data;
using AdminOperations;
using UserOperations;
namespace LoginOperations
{
    class Login
    {
        public void AdminLogin()
        {
            Console.WriteLine("\nAdmin Login");
            Console.WriteLine("-----------");
            Console.WriteLine("Enter username:");
            string? uname=Console.ReadLine();
            Console.WriteLine("Enter password:");
            string? pass=Console.ReadLine();
            if(uname.Equals("admin") && pass.Equals("123"))
            {
                Console.WriteLine("\n\nWelcome Admin");
                Console.WriteLine("-------------");
                Library o=new Library();
                bool flag=true;
                while(flag)
                {
                    Console.WriteLine("\n1.Add\t\t2.View\t\t3.Delete\t\t4.Update\t\t5.Logout");
                    Console.WriteLine("Enter your choice:");
                    int inp1=Convert.ToInt32(Console.ReadLine());
                    switch(inp1)
                    {
                        case 1:o.AddBook();
                            break;
                        case 2:o.ViewBook();
                            break;
                        case 3:o.DeleteBook();
                            break;
                        case 4:o.UpdateBook();
                            break;
                        case 5:Console.WriteLine("You have logged out successfully......");
                               flag=false;
                               break;
                        default: Console.WriteLine("Invalid input");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid credentials");
            }
        }
        public void UserLoginOrRegister()
        {
            Console.WriteLine("\n\nWelcome User");
            Console.WriteLine("-----------");
            User user=new User();
            bool flag=true;
            while(flag)
            {
                Console.WriteLine("\n1.Login\t\t2.Register\t\t3.Exit");
                Console.WriteLine("Enter your choice:");
                int inp=Convert.ToInt32(Console.ReadLine());
                switch(inp)
                {
                    case 1:user.UserLogin();
                            break;
                    case 2:user.UserRegister();
                            break;
                    case 3: flag=false;
                            break;
                    default:Console.WriteLine("Invalid Input");
                            break;
                }
            }
        }
    }

    class User
    {
        SqlConnection  con=new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=library;Trusted_Connection=True;");
        public void UserRegister()
        {
            string? name="",password="";
            Console.WriteLine("Username:");
            name=Console.ReadLine();
            Console.WriteLine("Password:");
            password=Console.ReadLine();
            con.Open();
            try
            {
                SqlCommand cmd=new SqlCommand("INSERT INTO UserTable (username,userpassword) VALUES ('"+name+"','"+password+"')",con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("\nRegistration successful......");
                
            }
            catch(Exception e)
            {
                Console.WriteLine("Username already exists...");
            }
            con.Close();
        }
        public void UserLogin()
        {
            Console.WriteLine("Enter your username:");
            string? uname=Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string? pass=Console.ReadLine();
            con.Open();
            BookLend obj=new BookLend();
            SqlCommand cmd=new SqlCommand("SELECT userpassword from UserTable where username='"+uname+"'",con);
            cmd.ExecuteNonQuery();  
            string userpassword=""+cmd.ExecuteScalar();
            if(userpassword.Equals(pass))
            {
                Console.WriteLine("\nWelcome "+uname);
                Console.WriteLine("----------------");
                bool flag=true;
                while(flag)
                {
                    Console.WriteLine("\n1.Search\t\t2.Borrow\t\t3.Return\t\t4.Status\t\t5.Logout");
                    Console.WriteLine("Enter your choice:");
                    int inp=Convert.ToInt32(Console.ReadLine());
                    switch(inp)
                    {
                        case 1:obj.Search();
                               break;
                        case 2:obj.Borrow(uname);
                               break;
                        case 3:obj.Return(uname);
                               break;
                        case 4:obj.Status(uname);
                               break;
                        case 5:Console.WriteLine("You have logged out successfully......");
                               flag=false;
                               break;
                        default:Console.WriteLine("Invalid input");
                               break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid Credentials.....");
            }
            con.Close();

        }
    }

}