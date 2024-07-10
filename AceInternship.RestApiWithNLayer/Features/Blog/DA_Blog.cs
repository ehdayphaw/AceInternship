 using AceInternship.RestApiWithNLayer.Db;

namespace AceInternship.RestApiWithNLayer.Features.Blog
{
    public class DA_Blog
    {
        private readonly AppDbContext _context;

        public DA_Blog()
        {
            _context = new AppDbContext();
        }
        public List<BlogModel> GetBlogs()
        {
            var lst = _context.Blogs.ToList();
            return lst;
        }
        public BlogModel GetBlog(int id)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            return item;
        }
        public int CreateBlog(BlogModel requestModel)
        {
            _context.Blogs.Add(requestModel);
            var item = _context.SaveChanges();
            return item;
        }
        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null) return 0;
            item.BlogTitle = requestModel.BlogTitle;
            item.BlogAuthor = requestModel.BlogAuthor;
            item.BlogContent = requestModel.BlogContent;
            int result = _context.SaveChanges();
            return result;

        }
        public int DeleteBlog(int id) 
        {
            var item = _context.Blogs.FirstOrDefault(x => x.BlogId == id);
            if(item is null) return 0;
            _context.Blogs.Remove(item);
            int result =_context.SaveChanges();
            return result;
        } 
    }
}
