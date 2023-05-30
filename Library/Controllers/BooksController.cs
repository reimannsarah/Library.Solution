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
  public ActionResult Create(Book book, string[] author)
  {
    // if (author.AuthorId == 0)
    // {

    // }
    string AuthorName = author[0];
#nullable enable
    Author? thisAuthor = _db.Authors.FirstOrDefault(author => author.Name == AuthorName);
#nullable disable
		if (thisAuthor == null)
		{
			_db.Authors.Add( new Author() { Name = AuthorName });
			_db.SaveChanges();
		}
// #nullable enable
//       AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => (join.AuthorId == thisAuthor.AuthorId && join.BookId == book.BookId));
// #nullable disable
//       if (joinEntity == null && thisAuthor.AuthorId != 0)
//       {
//         _db.Books.Add(book);
//         _db.SaveChanges();
//         _db.AuthorBooks.Add(new AuthorBook() { AuthorId = thisAuthor.AuthorId, BookId = book.BookId });
//         _db.SaveChanges();
//       }
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
