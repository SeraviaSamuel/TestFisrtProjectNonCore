using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestFisrtProjectNonCore.Models;
using TestFisrtProjectNonCore.View_Models;
namespace TestFisrtProjectNonCore.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        private readonly BookStoreEntity context;
        public BooksController()
        {
            context = new BookStoreEntity();
        }
        public ActionResult Index()
        {
            List<Book> books = context.Books.Include(b => b.Category).ToList();
            return View(books);
        }
        public ActionResult Create()
        {
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.Categories = context.Categories.Where(b => b.IsActive).ToList();
            return View("BookForm", bookViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryTokenAttribute]
        public ActionResult Save(BookViewModel bookViewModel)
        {
            if (!ModelState.IsValid)
            {
                bookViewModel.Categories = context.Categories.Where(b => b.IsActive).ToList();
                return View("BookForm", bookViewModel);
                //return RedirectToAction("Create", bookViewModel);
            }
            if (bookViewModel.Id == 0)
            {
                Book book = new Book();
                book.Title = bookViewModel.Title;
                book.Auther = bookViewModel.Auther;
                book.CategoryId = bookViewModel.CategoryId;
                book.Description = bookViewModel.Description;
                context.Books.Add(book);

            }
            else
            {
                Book book = new Book();
                book = context.Books.FirstOrDefault(b => b.Id == bookViewModel.Id);
                if (book == null)
                {
                    return HttpNotFound();
                }
                book.Title = bookViewModel.Title;
                book.Auther = bookViewModel.Auther;
                book.CategoryId = bookViewModel.CategoryId;
                book.Description = bookViewModel.Description;

            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = new Book();
            book = context.Books.Include(b => b.Category).FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = new Book();
            book = context.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            BookViewModel bookViewModel = new BookViewModel();
            bookViewModel.Id = book.Id;
            bookViewModel.Auther = book.Auther;
            bookViewModel.Title = book.Title;
            bookViewModel.Description = book.Description;
            bookViewModel.Categories = context.Categories.Where(b => b.IsActive).ToList();
            bookViewModel.CategoryId = book.CategoryId;
            return View("BookForm", bookViewModel);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = await context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return HttpNotFound();
            }
            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}