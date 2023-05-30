namespace Library.Controllers;

public class HomeController : Controller
{
  private readonly LibraryContext _db;
  public HomeController(LibraryContext db)
  {
    _db = db;
  }
}