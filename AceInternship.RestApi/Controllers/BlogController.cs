using AceInternship.RestApi.Db;
using AceInternship.RestApi.Models;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace AceInternship.RestApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogController : ControllerBase
{
    private readonly AppDbContext _context;
    public BlogController()
    {
        _context = new AppDbContext();
    }
    [HttpGet]
    public IActionResult Read()
    {
        var lst=_context.Blogs.ToList();
      
        return Ok(lst);
    }
    [HttpGet("pageNo/{pageNo}/pageSize/{pageSize}")]
    public IActionResult Reads(int pageNo,int pageSize)
    {
        var lst = _context.Blogs
            .OrderByDescending(x => x.BlogId)
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize) 
            .ToList();

        int rowCount = _context.Blogs.Count();
        int pageCount =rowCount/pageSize;
        if (rowCount % pageSize > 0)
            pageCount++;

        BlogResponseModel model = new();
        model.PageNo = pageNo;
        model.PageSize = pageSize;
        model.PageCount = pageCount;
        return Ok(model);  
    }

    [HttpGet("{id}")]
    public IActionResult Edit(int id)
    {
       var item= _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return NotFound("Data not found.");
        }

        return Ok(item);
    }

    [HttpPost]
    public IActionResult Create(BlogModel blogModel)
    {
            _context.Blogs.Add(blogModel);
            int result = _context.SaveChanges();
            string msg = result > 0 ? "Create Success" : "Create Fail";
            return Ok(msg);
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id,BlogModel blogModel)
    {
       var item= _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return NotFound("Data not found");
        }
        item.BlogTitle = blogModel.BlogTitle;
        item.BlogAuthor = blogModel.BlogAuthor;
        item.BlogContent = blogModel.BlogContent;
        int result = _context.SaveChanges();
        string msg = result > 0 ? "Update Success" : "Update Fail";
        return Ok(msg);
    }

    [HttpPatch]
    public IActionResult Patch(int id,BlogModel blogModel)
    {
        var item=_context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if (item is null)
        {
            return NotFound("Data not found.");
        }
        if (!string.IsNullOrEmpty(blogModel.BlogTitle))
        {
            item.BlogTitle=blogModel.BlogTitle;
        }
        if (!string.IsNullOrEmpty(blogModel.BlogAuthor))
        {
            item.BlogAuthor = blogModel.BlogAuthor;
        }
        if (!string.IsNullOrEmpty(blogModel.BlogContent))
        {
            item.BlogContent = blogModel.BlogContent;
        }

        int result = _context.SaveChanges();
        string msg = result > 0 ? "Update Success" : "Update Fail";
        return Ok(msg);
    }

    [HttpDelete]
    public IActionResult Delete(int id)
    {
        var item= _context.Blogs.FirstOrDefault(x => x.BlogId == id);
        if(item is null)
        {
            return NotFound("data not found");
        }
      _context.Remove(item);
        int result =_context.SaveChanges();
      string msg = result > 0 ? "Delete Success" : "Delete Fail";
        return Ok(msg);
    }





}


