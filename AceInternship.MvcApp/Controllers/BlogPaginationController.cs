using AceInternship.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AceInternship.MvcApp.Controllers
{
    public class BlogPaginationController : Controller
    {
        [ActionName("Index")]
        public IActionResult BlogIndex(int pageNo =1, int pageSize =10)
        {
            AppDbContext _dbContext = new AppDbContext();

            int rowCount=_dbContext.Blogs.Count();
            int pageCount = rowCount / pageSize;
            if (rowCount % pageSize > 0)
                pageCount++;

            if(pageNo > pageCount)
            {
                return Redirect("/Blog");
            }
            List<BlogModel> lst=_dbContext.Blogs
                .Take(pageSize)
                .ToList();

            BlogResponseModel model = new();
            model.Data = lst;
            model.PageNo = pageNo;
            model.PageSize = pageSize;
            model.PageCount = pageCount;

            return View("BlogIndex",model);
        }
    }
}
