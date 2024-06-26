using AceInternship.RestApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Security.AccessControl;

namespace AceInternship.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            using IDbConnection db=new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            foreach (BlogModel item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("--------------");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item=FindById(id);
            if (item is null)
            {
                return NotFound("Data not found.");
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------");
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateBlogs(BlogModel model)
        {
            string query= @"INSERT INTO [dbo].[Tbl_Blog]([BlogTitle],[BlogAuthor],[BlogContent])
                            VALUES(@BlogTitle,@BlogAuthor,@BlogContent)";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, model);
            string msg = result > 0 ? "Create Success" : "Create Fail";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id,BlogModel model)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("Data not found.");
            }
            model.BlogId = id;
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"UPDATE [dbo].[Tbl_Blog]SET BlogTitle=@BlogTitle,BlogAuthor=@BlogAuthor,BlogContent=@BlogContent WHERE BlogId =@BlogId";
            int result = db.Execute(query, model);

            string msg = result > 0 ? "Update Success" : "Update Fail";
            return Ok(msg);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,BlogModel model)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("Data not found.");
            }
            string conditions=string.Empty;
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
                conditions += "BlogTitle=@BlogTitle";
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                conditions += "BlogAuthor=@BlogAuthor";
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                conditions += "BlogContent=@BlogContent";
            }
            if(conditions.Length == 0)
            {
                return NotFound("No Data to Update.");
            }
            //conditions=conditions.Substring(0,conditions.Length-2); if put comma and space after  conditions += "BlogTitle=@BlogTitle, ";
            string condition =string.Join(",", conditions);
            model.BlogId = id;
            string query= $@"UPDATE [dbo].[Tbl_Blog]SET {condition} WHERE BlogId =@BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, model);
            string msg = result > 0 ? "Patch Success" : "Patch Fail";
            return Ok(msg);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("Data not found.");
            }
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
            WHERE BlogId =@BlogId";
            int result = db.Execute(query, new BlogModel { BlogId = id });
            string msg = result > 0 ? "Delete Success" : "Delete Fail";
            return Ok(msg);
        }
        private BlogModel? FindById(int id)
        {
            string query = "Select * From Tbl_Blog where BlogId = @BlogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;
        }
    }
}
