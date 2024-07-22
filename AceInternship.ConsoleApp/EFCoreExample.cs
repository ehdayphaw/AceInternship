using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceInternship.ConsoleApp
{
    internal class EFCoreExample
    {
        public readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Read();
            // Edit(40);
            //Create("Your Wings", "Chagn", "Loving you is enough.");
            //Update(47,"Your Wings", "Chagn", "Loving you is enough.");
            Delete(49);
        }

        private void Read()
        {

            var lst = db.Blogs.ToList();
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
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine("--------------");
        }
        private void Create(string title, string author, string content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            db.Blogs.Add(item);
            int result = db.SaveChanges();
            string msg = result > 0 ? "Create Success" : "Create Fail";

            Console.WriteLine(msg);
        }

        private void Update(int id,string title,string author,string content)
        {
            var item=db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }
            item.BlogTitle= title;
            item.BlogAuthor= author;
            item.BlogContent= content;
            int result =db.SaveChanges();
            string msg = result > 0 ? "Update Success" : "Update Fail";

            Console.WriteLine(msg);

        }

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("Data not found.");
                return;
            }
            db.Remove(item);
            int result = db.SaveChanges();
            string msg = result > 0 ? "Delete Success" : "Delete Fail";

            Console.WriteLine(msg);

        }

        public void Generate(int count)
        {
            for (int i = 0; i < count; i++)
            {
                int rowNo = (i + 1);
                Create("Title"+ rowNo, "Author"+ rowNo, "Content"+ rowNo);
            }
        }
    }
}
