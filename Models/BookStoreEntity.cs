using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace TestFisrtProjectNonCore.Models
{
    public class BookStoreEntity : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public BookStoreEntity() : base("firstconnection")
        {

        }

        public List<Book> GetBooksByCategories(byte categoryId)
        {
            return this.Database.SqlQuery<Book>("EXEC GetBooksByCategories @CategoryId",
           new SqlParameter("@CategoryId", categoryId)).ToList();
        }
    }
}