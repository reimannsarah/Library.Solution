using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using System.Threading.Tasks;
using Library.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
  public class SearchController: Controller
  {
    private readonly LibraryContext _db;
    public SearchController(LibraryContext db)
    {
      _db = db;
    }
    public ActionResult Index()
    {
      List<Book> allBooks = _db.Books
                                  .Include(book => book.AuthorJoinEntities)
                                  .ThenInclude(join => join.Author)
                                  .ToList();
      return View(allBooks);
    }

    public ActionResult TitleSearch()
    {
      return View();
    }

    [HttpPost]
    public ActionResult TitleSearch(string title)
    {
      List<Book> foundBooks = _db.Books.Where(book => book.Title == title).ToList();
                                // .Include(book => book.AuthorJoinEntities)
                                // .ThenInclude(join => join.Author)
      return RedirectToAction("Results",foundBooks);
    }

    public ActionResult Results(List<Book> results)
    {
      return View(results);
    }
  }
}