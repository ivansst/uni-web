using Schools.Data.Models;

namespace Schools.Models.SubjectModels
{
  public class SubjectModel : Subject
  {
    public int? ClassId { get; set; }
  }
}