using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using System.Threading.Tasks;
using Library.ViewModels;

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
      return View();
    }
  }
}