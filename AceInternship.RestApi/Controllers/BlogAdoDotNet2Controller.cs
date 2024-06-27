using AceInternship.RestApi.Models;
using AceInternship.share;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using static AceInternship.share.AdoDotNetService;

namespace AceInternship.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _doDotNetService = new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            var lst = _doDotNetService.Query<BlogModel>(query);

            return Ok(lst);

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";

            /*AdoDotNetParameters[] parameters=new AdoDotNetParameters[1];
            parameters[0] = new AdoDotNetParameters("@BlogId", id);
            var lst = _doDotNetService.Query<BlogModel>(query, parameters);*/


            var item = _doDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));
            if(item is null)
            {
                return NotFound("Data not found.");
            }
            return Ok(item);
        }


        [HttpPost]
        public IActionResult CreateBlog(BlogModel model)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]([BlogTitle],[BlogAuthor],[BlogContent]) VALUES(@BlogTitle,@BlogAuthor,@BlogContent)";
            int result = _doDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle",model.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", model.BlogContent)
                );
            string msg = result > 0 ? "Create Success" : "Create Fail";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel model)
        {
            string query1 = "select * from Tbl_Blog where BlogId = @BlogId";
            var item = _doDotNetService.QueryFirstOrDefault<BlogModel>(query1, new AdoDotNetParameter("@BlogId", id));
            if (item is null)
            {
                return NotFound("Data not found.");
            }

            string query = @"UPDATE [dbo].[Tbl_Blog] SET BlogTitle=@BlogTitle,BlogAuthor=@BlogAuthor,BlogContent=@BlogContent WHERE BlogId =@BlogId";

            int result = _doDotNetService.Execute(query,
                 new AdoDotNetParameter("@BlogId", model.BlogId),
                new AdoDotNetParameter("@BlogTitle", model.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", model.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", model.BlogContent)
                );
            string msg = result > 0 ? "Update Success" : "Update Fail";
            return Ok(msg) ;
        }
        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id,BlogModel blog)
        {
            string query1 = "select * from Tbl_Blog where BlogId = @BlogId";
            var item = _doDotNetService.QueryFirstOrDefault<BlogModel>(query1, new AdoDotNetParameter("@BlogId", id));
            if (item is null)
            {
                return NotFound("Data not found.");
            }
            List<AdoDotNetParameter> lst = new List<AdoDotNetParameter>();
            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += " [BlogTitle] = @BlogTitle, ";
                lst.Add("@BlogTitle", blog.BlogTitle);
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += " [BlogAuthor] = @BlogAuthor, ";
                lst.Add("@BlogAuthor", blog.BlogAuthor);
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += " [BlogContent] = @BlogContent, ";
                lst.Add("@BlogContent", blog.BlogContent);
            }

            if (conditions.Length == 0)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            lst.Add(new AdoDotNetParameter("@BlogId", id));

            conditions = conditions.Substring(0, conditions.Length - 2);
            string query = $@"UPDATE [dbo].[Tbl_Blog] SET {conditions} WHERE BlogId = @BlogId";

            int result = _doDotNetService.Execute(query,
                lst.ToArray()
            );

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Ok(message);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)

        {
            string query1 = "select * from Tbl_Blog where BlogId = @BlogId";
            var item = _doDotNetService.QueryFirstOrDefault<BlogModel>(query1, new AdoDotNetParameter("@BlogId", id));
            if (item is null)
            {
                return NotFound("Data not found.");
            }
            string query = @"DELETE FROM [dbo].[Tbl_Blog]
                           WHERE BlogId = @BlogId";

            int result = _doDotNetService.Execute(query, new AdoDotNetParameter("@BlogId", id));
            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            return Ok(message);
        }

    }
}
