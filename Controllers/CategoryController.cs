using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TestFisrtProjectNonCore.Models;

namespace TestFisrtProjectNonCore.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        private readonly BookStoreEntity context;
        public CategoryController()
        {
            context = new BookStoreEntity();
        }
        public ActionResult Index()
        {
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }


        public ActionResult BooksByCategory(byte categoryId)
        {

            var books = context.GetBooksByCategories(categoryId);
            return PartialView("BooksList", books);

        }

    }
}