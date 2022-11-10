using System;
using demoProject;
using LoginOperations;
using AdminOperations;
using System.Data.SqlClient;

namespace UserOperations
{
    class BookLend
    {
        SqlConnection  con=new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=library;Trusted_Connection=True;");
        Library o=new Library();
        public void Search()
        {
            Console.WriteLine("Enter book name to be searched:");
            string? name=Console.ReadLine();
            con.Open();
            SqlCommand cmd=new SqlCommand("SELECT id FROM Book where bookname='"+name+"'",con);
            bool nid=(bool)(cmd.ExecuteScalar()==null);
            //Console.WriteLine(nid+"  "+(null==cmd.ExecuteScalar()));
            if(!nid)
            {
                int id=(int)cmd.ExecuteScalar();
                Console.WriteLine(name+" exits with ID "+id);
            }
            else
            {
                Console.WriteLine("Invalid Book Name....");
            }
            con.Close();
        }
        User user=new User();
        public void Borrow(string? uname)
        {
            Console.WriteLine("Enter the book id to be borrowed:");
            int id=Convert.ToInt32(Console.ReadLine());
            con.Open();
            SqlCommand cmd=new SqlCommand("SELECT id FROM Book where id='"+id+"'",con);
            bool nid=(bool)(cmd.ExecuteScalar()==null);
            //Console.WriteLine(nid+"  "+(null==cmd.ExecuteScalar()));
            if(!nid)
            {
                if(o.UpdateCopies(id,"borrow"))
                {
                    var d=DateTime.Now.AddDays(10);
                    SqlCommand cmd1=new SqlCommand("INSERT INTO BorrowBookList (username,id,issuedate,duedate) VALUES ('"+uname+"','"+id+"',GETDATE(),DATEADD(day,10,GETDATE()))",con);
                    cmd1.ExecuteNonQuery();
                    Console.WriteLine("Book issued on "+DateTime.Now.ToShortDateString());
                    Console.WriteLine("Book due date is: "+d.ToShortDateString());
                }
                else
                {
                    Console.WriteLine("Insufficient copies....");
                }
            }
            else
            {
                Console.WriteLine("Invalid Book ID....");
            }
            con.Close();
        }
        public void Return(string? uname)
        {
            Console.WriteLine("Enter the book id to be returned:");
            int id=Convert.ToInt32(Console.ReadLine());
            con.Open();
            SqlCommand cmd=new SqlCommand("SELECT id FROM Book where id='"+id+"'",con);
            bool nid=(bool)(cmd.ExecuteScalar()==null);
            //Console.WriteLine(nid+"  "+(null==cmd.ExecuteScalar()));
            if(!nid)
            {
                SqlCommand cmd1=new SqlCommand("SELECT id FROM BorrowBookList where id='"+id+"' AND username='"+uname+"'",con);
                bool nid1=(bool)(cmd1.ExecuteScalar()==null);
                if(!nid1)
                {
                    SqlCommand cmd2=new SqlCommand("DELETE FROM BorrowBookList WHERE id='"+id+"' AND username='"+uname+"' ",con);
                    cmd2.ExecuteNonQuery();
                    bool stat=o.UpdateCopies(id,"return");
                    Console.WriteLine("Book returned...Thank You");
                }
                else
                {
                    Console.WriteLine("You didn't borrow any book with ID "+id);    
                }
            }
            else
            {
                Console.WriteLine("Invalid book ID");
            }
            con.Close();
        }
        public void Status(string? uname)
        {
            bool flag=true;
            SqlCommand cmd=new SqlCommand("select * from BorrowBookList WHERE username='"+uname+"'",con);
            con.Open();
            Console.WriteLine("************ Status : ************ ");
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if(i>1)
                        {
                            string str=(reader.GetValue(i)+" ").Substring(0,10);
                            Console.Write(str+"    ");
                        }
                        else
                        {
                            Console.Write(reader.GetValue(i)+"    ");
                        }
                    }
                    Console.WriteLine();
                }
            }
            con.Close();
        }
    }
}