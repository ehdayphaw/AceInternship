using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AceInternship.ConsoleApp
{
    internal class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "AceInternship",
            UserID = "sa",
            Password = "edp@123"
        };
        public void Read()
        {
            
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = "select * from Tbl_Blog";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            connection.Close();

            foreach (DataRow dr in tbl.Rows)
            {
                Console.WriteLine("BlogId :" + dr["BlogId"]);
                Console.WriteLine("BlogTitle :" + dr["BlogTitle"]);
                Console.WriteLine("BlogAuthor :" + dr["BlogAuthor"]);
                Console.WriteLine("BlogContent :" + dr["BlogContent"]);
                Console.WriteLine("-------------------------------");
            }
        }
        
        public void Create(string title,string author,string content)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"INSERT INTO [dbo].[Tbl_Blog]([BlogTitle],[BlogAuthor],[BlogContent]) VALUES(@BlogTitle,@BlogAuthor,@BlogContent)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string msg=result >0 ? "Create Success":"Create Fail";
            
            Console.WriteLine(msg);
        }

        public void Update(int id,string title,string author,string content)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[Tbl_Blog]SET BlogTitle=@BlogTitle,BlogAuthor=@BlogAuthor,BlogContent=@BlogContent WHERE BlogId =@BlogId";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            int result=cmd.ExecuteNonQuery();
            connection.Close();
            string msg = result > 0 ? "Update Success" : "Update Fail";

            Console.WriteLine(msg);
        }

        public void Delete(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId =@BlogId";
            SqlCommand command = new SqlCommand(query, connection); 
            command.Parameters.AddWithValue("@BlogId",id);
            int result=command.ExecuteNonQuery();
            connection.Close();
            string msg = result > 0 ? "Delete Success" : " Fail";

            Console.WriteLine(msg);

        }
    }
}
