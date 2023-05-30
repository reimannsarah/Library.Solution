using System.Collections.Generic;

namespace Library.Models;

public class Genre
{
  public int GenreId { get; set; }
  public string Name { get; set; }
  public List<BookGenre> GenreJoinEntities { get; set; }
}