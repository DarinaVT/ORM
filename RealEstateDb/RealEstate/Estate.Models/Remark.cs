using System.Buffers.Text;

namespace Estate.Models;

public class Remark : BaseEntity
{
    public string AssessorRemarks { get; set; }
    public string OPM { get; set; }
    public int PropertyId { get; set; }
    public PropertyInfo Property { get; set; }
}