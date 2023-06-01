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
      List<Book> foundBooks = _db.Books
                                .Include(book => book.AuthorJoinEntities)
                                .ThenInclude(join => join.Author)
                                .Where(book => book.Title.Contains(title)).ToList();
      return View("Results",foundBooks);
    }

    public ActionResult AuthorSearch() => View();

    [HttpPost]
    public ActionResult AuthorSearch(string author)
    {
      ViewBag.SearchGuy = author;
      List<Book> foundBooks = _db.Books
                                .Include(book => book.AuthorJoinEntities)
                                .ThenInclude(join => join.Author)
                                .ToList();
      List<Book> results = new List<Book>();
      foreach (Book book in foundBooks)
      {
        foreach (AuthorBook join in book.AuthorJoinEntities)
        {
          if (join.Author.Name.Contains(author))
          {
            results.Add(book);
          }
        }
      }
      return View("Results", results);
    }

    public ActionResult Results(List<Book> results)
    {
      return View(results);
    }
  }
}