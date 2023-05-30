using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers;

public class BooksController : Controller
{
    private readonly LibraryContext _db;

    public BooksController(LibraryContext db)
    {
        _db = db;
    }

    public ActionResult Index()
    {
        return View();
    }

    public ActionResult Create()
    {
        ViewBag.Authors = _db.Authors.Select(a => new SelectListItem
        {
          Value = a.AuthorId.ToString(),
          Text = a.Name
        }).ToList();
        return View();
    }

    [HttpPost]
    public ActionResult Create(Book book, int authorId)
    {
      // if (authorId == 0)
      // {

      // }
#nullable enable
        AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => (join.AuthorId == authorId && join.BookId == book.BookId));
#nullable disable
        if (joinEntity == null && authorId != 0)
        {
            _db.Books.Add(book);
            _db.AuthorBooks.Add(new AuthorBook() { AuthorId = authorId, BookId = book.BookId });
            _db.SaveChanges();
        }
        return RedirectToAction("Details", new { id = book.BookId });
    }

    public ActionResult Details(int id)
    {
      Book thisBook = _db.Books.Include(book => book.AuthorJoinEntities)
                              .ThenInclude(join => join.Author)
                              .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
}
