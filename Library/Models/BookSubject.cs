namespace Library.Models;

public class BookSubject
{
  public int BookSubjectId { get; set; }
  public Book Book { get; set; }
  public int BookId { get; set; }
  public Subject Subject { get; set; }
  public int SubjectId { get; set; }
}