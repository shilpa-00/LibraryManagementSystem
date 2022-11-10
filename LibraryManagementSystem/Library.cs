using System;
using  demoProject;
using LoginOperations;
using System.Data.SqlClient;
using System.Data;
// Server=localhost\SQLEXPRESS;Database=library;Trusted_Connection=True;
namespace AdminOperations
{
    class Library
    {
        SqlConnection  con=new SqlConnection(@"Server=localhost\SQLEXPRESS;Database=library;Trusted_Connection=True;");

        
        public void AddBook()
        {
            Console.WriteLine("Enter book id:");
            int id=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter book name:");
            string? name=Console.ReadLine();
            Console.WriteLine("Enter author name:");
            string? authorname=Console.ReadLine();
            Console.WriteLine("Enter number of copies:");
            int copies=Convert.ToInt32(Console.ReadLine());
            //obj.Add(new Book(id,name,authorname,copies));
            try
            {
                con.Open();
                SqlCommand cmd=new SqlCommand("INSERT INTO Book (id,bookname,authorname,copies) VALUES ('"+id+"','"+name+"','"+authorname+"','"+copies+"')",con);
                cmd.ExecuteNonQuery();
                Console.WriteLine("Book inserted successfully.....");
            }
            catch(Exception e)
            {
                Console.WriteLine("Book ID already exists....");
            }
            con.Close();
        }
        public void DeleteBook()
        {
            Console.WriteLine("Enter the book id to be deleted");
            int id=Convert.ToInt32(Console.ReadLine());
            con.Open();
            SqlCommand cmd=new SqlCommand("SELECT id FROM Book where id='"+id+"'",con);
            bool nid=(bool)(cmd.ExecuteScalar()==null);
            //Console.WriteLine(nid+"  "+(null==cmd.ExecuteScalar()));
            if(!nid)
            {
                SqlCommand cmd1=new SqlCommand("DELETE FROM Book WHERE id="+id+"",con);
                cmd1.ExecuteNonQuery();
                Console.WriteLine("Book deleted successfully.....");
            }
            else
            {
                Console.WriteLine("Book doesn't exist");
            }
            con.Close();
        }

        public void ViewBook()
        {
            SqlCommand cmd=new SqlCommand("select * from Book",con);
            con.Open();
            Console.WriteLine("****** Available books are : ****** ");
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if(reader.HasRows!=true)
                {
                    Console.WriteLine("No books are added....");
                }
                else
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetValue(i)+"     ||  ");
                        }
                        Console.WriteLine();
                    }
                }
            }

            con.Close();
        }
        public void UpdateBook()
        {
            Console.WriteLine("Enter the Book ID:");
            int id=Convert.ToInt32(Console.ReadLine());
            con.Open();
            SqlCommand cmd=new SqlCommand("SELECT id FROM Book where id='"+id+"'",con);
            bool nid=(bool)(cmd.ExecuteScalar()==null);
            //Console.WriteLine(nid+"  "+(null==cmd.ExecuteScalar()));
            if(!nid)
            {
                Console.WriteLine("Enter the Book Name:");
                string? name=Console.ReadLine();
                Console.WriteLine("Enter the Author name:");
                string? authorname=Console.ReadLine();
                Console.WriteLine("Enter the number of copies:");
                int copies=Convert.ToInt32(Console.ReadLine());
                SqlCommand cmd1=new SqlCommand("UPDATE Book SET bookname='"+name+"' , authorname='"+authorname+"' , copies='"+copies+"' where id='"+id+"' ",con);
                Console.WriteLine("Book Updated successfully......");
                cmd1.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("Invalid Book ID....");
            }
            con.Close();

        }
        public bool UpdateCopies(int id,string str)
        {
            con.Open();
            SqlCommand cmd=new SqlCommand("SELECT copies FROM Book WHERE id='"+id+"'",con);
            int copies=(int)cmd.ExecuteScalar();
            if(str.Equals("borrow"))
            {
                if(copies>0)
                {
                    SqlCommand cmd1=new SqlCommand("UPDATE Book SET copies=copies-1 WHERE id='"+id+"'",con);
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            else
            {
                SqlCommand cmd2=new SqlCommand("UPDATE Book SET copies=copies+1 WHERE id='"+id+"'",con);
                cmd2.ExecuteNonQuery();
            }
            con.Close();
            return false;
        }
    }   
}