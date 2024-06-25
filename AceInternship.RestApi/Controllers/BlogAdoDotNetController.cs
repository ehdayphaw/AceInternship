﻿using AceInternship.RestApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AceInternship.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from Tbl_Blog";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);
            connection.Close();
            /* List<BlogModel> lst = new List<BlogModel>();
             foreach (DataRow dr in tbl.Rows)
             {
                 BlogModel blog = new BlogModel();
                 blog.BlogId = Convert.ToInt32(dr["BlogId"]);
                 blog.BlogTitle = Convert.ToString(dr["BlogTitle"]);
                 blog.BlogAuthor = Convert.ToString(dr["BlogAuthor"]);
                 blog.BlogContent = Convert.ToString(dr["BlogContent"]);
                 lst.Add(blog);
             }*/
            List<BlogModel> lst = tbl.AsEnumerable().Select(dr => new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            }).ToList();

            return Ok(lst);

        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            //string query = "select * from Tbl_Blog where BlogId=@BlogId";
            //SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //connection.Open();
            //SqlCommand command = new SqlCommand(query, connection);
            //command.Parameters.AddWithValue("@BlogId", id);
            //SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataTable tbl = new DataTable();
            //adapter.Fill(tbl);

            //connection.Close();
            //DataRow dr = tbl.Rows[0];
            //var item = new BlogModel
            //{
            //    BlogId = Convert.ToInt32(dr["BlogId"]),
            //    BlogTitle = Convert.ToString(dr["BlogTitle"]),
            //    BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
            //    BlogContent = Convert.ToString(dr["BlogContent"])
            //};
            //if (tbl.Rows.Count == 0)
            //{
            //    return NotFound("No data found.");
            //}
            //return Ok(tbl);

            var item = GetById(id);
            if(item is null) return NotFound("No data found.");
            return Ok(item);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel model)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]([BlogTitle],[BlogAuthor],[BlogContent]) VALUES(@BlogTitle,@BlogAuthor,@BlogContent)";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);

            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string msg = result > 0 ? "Create Success" : "Create Fail";
            return Ok(msg);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel model)
        {
            //string isExist = "Select * From Tbl_Blog where BlogId = @BlogId";
            //string query = @"UPDATE [dbo].[Tbl_Blog]SET BlogTitle=@BlogTitle,BlogAuthor=@BlogAuthor,BlogContent=@BlogContent WHERE BlogId =@BlogId";

            //SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            //connection.Open();
            //SqlCommand check = new SqlCommand(isExist, connection);
            //check.Parameters.AddWithValue("@BlogId", id);
            //SqlDataAdapter adapter = new SqlDataAdapter(check);
            //DataTable tbl = new DataTable();
            //adapter.Fill(tbl);
            //if (tbl.Rows.Count == 0)
            //{
            //    return NotFound("No data found.");
            //}

            //SqlCommand cmd = new SqlCommand(query, connection);
            //cmd.Parameters.AddWithValue("@BlogId", id);
            //cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            //cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogTitle);
            //cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            //int result = cmd.ExecuteNonQuery();
            //connection.Close();
            //string msg = result > 0 ? "Update Success" : "Update Fail";
            //return Ok(msg);

            var item = GetById(id);
            if (item is null) return NotFound("No data found.");

            string query = @"UPDATE [dbo].[Tbl_Blog] SET BlogTitle=@BlogTitle,BlogAuthor=@BlogAuthor,BlogContent=@BlogContent WHERE BlogId =@BlogId";

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(model.BlogTitle))
            {
               cmd.Parameters.AddWithValue("@BlogTitle", model.BlogTitle);
            }
            if (!string.IsNullOrEmpty(model.BlogAuthor))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", model.BlogAuthor);
            }
            if (!string.IsNullOrEmpty(model.BlogContent))
            {
                cmd.Parameters.AddWithValue("@BlogContent", model.BlogContent);
            }
            
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string msg = result > 0 ? "Update Success" : "Update Fail";

            return Ok(msg);
        }

        private BlogModel? GetById(int id)
        {
            string query = "select * from Tbl_Blog where BlogId = @BlogId";
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable tbl = new DataTable();
            adapter.Fill(tbl);

            connection.Close();
            if (tbl.Rows.Count == 0) return default;

            DataRow dr = tbl.Rows[0];
            var item = new BlogModel
            {
                BlogId = Convert.ToInt32(dr["BlogId"]),
                BlogTitle = Convert.ToString(dr["BlogTitle"]),
                BlogAuthor = Convert.ToString(dr["BlogAuthor"]),
                BlogContent = Convert.ToString(dr["BlogContent"])
            };
            return item;
        }
    }
}
