using System.Collections.Generic;

namespace Library.Models;

public class Subject
{
  public int SubjectId { get; set; }
  public string Name { get; set; }
  public List<BookSubject> SubjectJoinEntities { get; set; }
}