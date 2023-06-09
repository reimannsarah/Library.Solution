using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Models;
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

  [Authorize(Roles = "librarian")]
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
  public ActionResult Create(Book book, string author)
  {
    string AuthorName = author;
#nullable enable
    Author? thisAuthor = _db.Authors.FirstOrDefault(author => author.Name == AuthorName);
#nullable disable
    if (thisAuthor == null)
    {
      thisAuthor = new Author() { Name = AuthorName };
      _db.Authors.Add(thisAuthor);
      _db.SaveChanges();
    }
#nullable enable
    AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => (join.AuthorId == thisAuthor.AuthorId && join.BookId == book.BookId));
#nullable disable
    if (joinEntity == null && thisAuthor.AuthorId != 0)
    {
      _db.Books.Add(book);
      _db.SaveChanges();
      _db.AuthorBooks.Add(new AuthorBook() { AuthorId = thisAuthor.AuthorId, BookId = book.BookId });
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

  public ActionResult Edit(int id)
  {
    ViewBag.Authors = _db.Authors.Select(a => new SelectListItem
      {
        Value = a.AuthorId.ToString(),
        Text = a.Name
      }).ToList();
    Book thisBook = _db.Books.Include(book => book.AuthorJoinEntities)
                        .ThenInclude(join => join.Author)
                        .FirstOrDefault(book => book.BookId == id);
    return View(thisBook);
  }

  [HttpPost]
  public ActionResult Edit(Book book, string author)
  {
    string AuthorName = author;
#nullable enable
    Author? thisAuthor = _db.Authors.FirstOrDefault(author => author.Name == AuthorName);
#nullable disable
    if (thisAuthor == null)
    {
      thisAuthor = new Author() { Name = AuthorName };
      _db.Authors.Add(thisAuthor);
      _db.SaveChanges();
    }
#nullable enable
    AuthorBook? joinEntity = _db.AuthorBooks.FirstOrDefault(join => (join.AuthorId == thisAuthor.AuthorId && join.BookId == book.BookId));
#nullable disable
    if (joinEntity == null && thisAuthor.AuthorId != 0)
    {
      _db.Books.Update(book);
      _db.SaveChanges();
      _db.AuthorBooks.Add(new AuthorBook() { AuthorId = thisAuthor.AuthorId, BookId = book.BookId });
      _db.SaveChanges();
    }
    return RedirectToAction("Details", new { id = book.BookId});
  }
}
