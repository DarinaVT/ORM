using System.ComponentModel.DataAnnotations;

namespace Models;

public class MakeModelYear : Base
{
    public string Make { get; set; }
    public string Model { get; set; }
    public string ModelYear { get; set; }
}
