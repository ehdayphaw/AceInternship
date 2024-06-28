using Microsoft.EntityFrameworkCore;

namespace AceInternship.RestApiWithNLayer.Features.Blog
{
    public class Bl_Blog
    {
        private readonly DA_Blog _daBlog;
        public Bl_Blog()
        {
            _daBlog = new DA_Blog();
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _daBlog.GetBlogs();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _daBlog.GetBlog(id);
            return item;
        }
        public int CreateBlog(BlogModel requestModel)
        {
            var item= _daBlog.CreateBlog(requestModel);
            return item;
        }
        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var result= _daBlog.UpdateBlog(id, requestModel);
            return result;

        }
        public int DeleteBlog(int id)
        {
            var result=_daBlog.DeleteBlog(id);
            return result;
        }
    }
}
