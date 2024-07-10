using Microsoft.EntityFrameworkCore;

namespace AceInternship.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly Bl_Blog _blBlog;
        public BlogController()
        {
            _blBlog = new Bl_Blog();
        }
        [HttpGet]
        public IActionResult Read()
        {
            var lst =_blBlog.GetBlogs();

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("Data not found.");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blogModel)
        {
            
            int result = _blBlog.CreateBlog(blogModel);
            string msg = result > 0 ? "Create Success" : "Create Fail";
            return Ok(msg);
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blogModel)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("Data not found");
            }
           var result =_blBlog.UpdateBlog(id, blogModel);   
            string msg = result > 0 ? "Update Success" : "Update Fail";
            return Ok(msg);
        }

      

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item is null)
            {
                return NotFound("data not found");
            }
           int result = _blBlog.DeleteBlog(id); 
            string msg = result > 0 ? "Delete Success" : "Delete Fail";
            return Ok(msg);
        }
    }
}
