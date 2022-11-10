using System;
using System.Collections.Generic;
using LoginOperations;
namespace demoProject
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine ("Library Management System");
            Console.WriteLine("-------------------------\n");
            Login o=new Login();
            bool flag=true;
            while(flag)
            {
                Console.WriteLine("\n------HOME PAGE------");
                Console.WriteLine("\n1.Admin\t\t2.User\t\t3.Exit");
                Console.WriteLine("Enter your choice:");
                int inp=Convert.ToInt32(Console.ReadLine());
                switch(inp)
                {
                    case 1:o.AdminLogin();
                        break;
                    case 2:o.UserLoginOrRegister();
                        break;
                    case 3:flag=false;
                        break;
                    default:Console.WriteLine("Invalid input");
                            break;
                }
            }
        }
    }
}