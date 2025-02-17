using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data;
using Web.Models.Genres;
using Web.Models;

namespace Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: BooksController
        public ActionResult Index()
        {
            var genres = _context.Genres.ToList();

            return View(genres);
        }

        // GET: BooksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genre = new Genre
                {
                    Name = model.Name   
                };

                _context.Genres.Add(genre);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            var genre = _context.Genres.FirstOrDefault(model => model.Id == id);

            if (genre == null)
            {
                return NotFound(0);
            }

            var model = new EditGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return View(model);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditGenreViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound(0);
            }
            if (ModelState.IsValid)
            {
                var genre = _context.Genres.Find(id);

                genre.Name = model.Name;

                _context.Genres.Update(genre);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            var genre = _context.Genres.FirstOrDefault(n => n.Id == id);

            if (genre == null) return NotFound(0);

            var model = new DeleteGenreViewModel
            {
                Id = genre.Id,
                Name = genre.Name
            };

            return View(model);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var genre = _context.Genres.Find(id);

            if (genre == null) return NotFound();

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
