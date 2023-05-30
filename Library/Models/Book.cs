using System.Collections.Generic;

namespace Library.Models;

public class Book
{
  public int BookId { get; set; }
  public string Author { get; set; }
  public string Title { get; set; }
  public int PubYear { get; set; }
  public bool OnLoan { get; set; }
  public List<BookGenre> GenreJoinEntities { get; set; }
  public List<BookSubject> SubjectJoinEntities { get; set; }
}