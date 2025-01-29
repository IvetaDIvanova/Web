using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Net.Sockets;
using Web.Data;
using Web.Models;
using Web.Models.Books;
using static System.Reflection.Metadata.BlobBuilder;


namespace Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BooksController
        public ActionResult Index()
        {
            var books = _context.Books.Include(b => b.Genre).ToList();

            return View(books);
        }

        // GET: BooksController/Details/5
        public ActionResult Details(int id)
        {
           if(id == null)
           {
               return NotFound(0);
           }
            var book = _context.Books.Include(b => b.Genre).FirstOrDefault(n => n.Id == id);
            return View(book);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name");

            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateBookViewModel model)
        {
            if(ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    GenreId = model.GenreId,
                    Year = model.Year,
                };

                _context.Books.Add(book);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewData["GenreId"] = new SelectList(_context.Genres, "Id", "Name", model.GenreId);
            return View(model);
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _context.Books.Include(b => b.Genre).FirstOrDefault(model => model.Id == id);

            if(book == null)
            {
                return NotFound(0);
            }

            var model = new EditBookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Year = book.Year,
                GenreId = book.GenreId,
            };

            ViewBag.GenreId = new SelectList(_context.Genres, "Id", "Name");

            return View(model);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditBookViewModel model)
        {
            if(id != model.Id)
            {
                return NotFound(0);
            }
            if(ModelState.IsValid)
            {
                var book = _context.Books.Find(id);

                book.Title = model.Title;
                book.Author = model.Author;
                book.Year = model.Year;
                book.GenreId= model.GenreId;

                _context.Books.Update(book);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _context.Books.Include(b => b.Genre).FirstOrDefault(n => n.Id == id);

            if(book == null) return NotFound(0);

            var model = new DeleteBookViewModel
            { Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Year = book.Year,
            Genre = book.Genre.Name,
            };

            return View(model);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);

            if(book == null) return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
