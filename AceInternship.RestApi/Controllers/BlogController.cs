using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AceInternship.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;
        public BlogController()
        {
            /* _connectionStringBuilder = new SqlConnectionStringBuilder();
             _connectionStringBuilder.DataSource = "."; //Server Name
             _connectionStringBuilder.InitialCatalog = "AceInternship"; //Database Name
             _connectionStringBuilder.UserID = "sa";
             _connectionStringBuilder.Password = "edp@123";*/

            _connectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".",
                InitialCatalog = "AceInternship",
                UserID = "sa",
                Password="edp@123"
            };
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
           
            using IDbConnection db=new SqlConnection(_connectionStringBuilder.ConnectionString);
            var lst=db.Query<TblBlog>(Queries.BlogList).ToList();
            return Ok(lst);
        }
        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<TblBlog>(Queries.BlogById, new { BlogId = id}).FirstOrDefault();
            if(item is null)
                return NotFound("No data Found.");

            return Ok(item);
        }

        [HttpPost]
       
        public IActionResult   CreateBlog(TblBlog blog)
        {
            using IDbConnection db=new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result =db.Execute(Queries.BlogCreate, blog);
            string message = result > 0 ? "Saving Success" : "Saving Fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id,TblBlog  blog)
        {
            blog.BlogId = id;
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result =db.Execute(Queries.BlogUpdate, blog);
            string msg = result > 0 ? "Update Success" : "Update Fail";
            return Ok(msg);
        }

        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok("PatchBlog");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            int result=db.Execute(Queries.BlogDelete, new { BlogId = id });
            string msg = result > 0 ? "Delete Success" : "Delete Fail";
            return Ok(msg);

        }
    }

    public class TblBlog
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogContent { get; set; }
    }
    public static class Queries
    {
        public static string BlogList { get; } = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent] FROM [dbo].[Tbl_Blog]";
        public static string BlogById { get; } = @"SELECT [BlogId], [BlogTitle], [BlogAuthor], [BlogContent] FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Tbl_Blog] ([BlogTitle], [BlogAuthor], [BlogContent]) VALUES (@BlogTitle, @BlogAuthor, @BlogContent)";
        public static string BlogUpdate { get; } = @"UPDATE [dbo].[Tbl_Blog] SET [BlogTitle] = @BlogTitle, [BlogAuthor] = @BlogAuthor, [BlogContent] = @BlogContent WHERE [BlogId] = @BlogId";
        public static string BlogDelete { get; } = @"DELETE FROM [dbo].[Tbl_Blog] WHERE BlogId = @BlogId";
    }
}
