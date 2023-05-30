using System.Collections.Generic;

namespace Library.Models;

public class Book
{
  public int BookId { get; set; }
  public List<AuthorBook> AuthorJoinEntities { get; }
  public string Title { get; set; }
  public int PubYear { get; set; }
  public bool OnLoan { get; set; }
  public List<BookGenre> GenreJoinEntities { get; }
  public List<BookSubject> SubjectJoinEntities { get; }
}