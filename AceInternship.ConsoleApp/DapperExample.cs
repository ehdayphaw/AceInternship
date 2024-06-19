using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AceInternship.ConsoleApp
{
    internal class DapperExample
    {
        public void Run()
        {
            //Read();
            //Edit(38);
            //Edit(39);
            //Create("poe", "tha", "dar");
            // Delete(39);
            Update(40,"pan","ka","lay");
        }
        private void Read()
        {
            using IDbConnection db=new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst=db.Query<BlogDto>("select * from Tbl_Blog").ToList();
            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------");
            }
        }
        private void Edit(int id)
        {
            using IDbConnection db=new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item= db.Query<BlogDto>("Select * From Tbl_Blog where BlogId = @BlogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------");

        }
        private void Create(string title,string author,string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"INSERT INTO [dbo].[Tbl_Blog]([BlogTitle],[BlogAuthor],[BlogContent]) VALUES(@BlogTitle,@BlogAuthor,@BlogContent)";
            int result =db.Execute(query,item);
            string msg = result > 0 ? "Create Success" : "Create Fail";

            Console.WriteLine(msg);
        }
        private void Delete(int id)
        {
            
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
      WHERE BlogId =@BlogId";
           int result= db.Execute(query,new BlogDto { BlogId = id});
            string msg = result > 0 ? "Delete Success" : "Delete Fail";

            Console.WriteLine(msg);
        }
        private void Update(int id,string title,string author,string content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"UPDATE [dbo].[Tbl_Blog]SET BlogTitle=@BlogTitle,BlogAuthor=@BlogAuthor,BlogContent=@BlogContent WHERE BlogId =@BlogId";
            int result =db.Execute(query,item);

            string msg = result > 0 ? "Update Success" : "Update Fail";

            Console.WriteLine(msg);
        }
    }
}
