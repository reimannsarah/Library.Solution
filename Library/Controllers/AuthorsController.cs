using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Models;
using System.Collections.Generic;
using System.Linq;


namespace Library.Controllers;

public class AuthorsController : Controller
{
  private readonly LibraryContext _db;
  public AuthorsController(LibraryContext db)
  {
    _db = db;
  }

  public ActionResult Index()
  {
    List<Author> model = _db.Authors.ToList();
    return View(model);
  }

  [Authorize(Roles = "librarian")]
  public ActionResult Create()
  {
    return View();
  }

  [Authorize(Roles = "librarian")]
  [HttpPost]
  public ActionResult Create(Author author)
  {
    _db.Authors.Add(author);
    _db.SaveChanges();
    return RedirectToAction("Index");
  }
}