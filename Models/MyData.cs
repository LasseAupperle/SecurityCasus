using System.ComponentModel.DataAnnotations;

namespace SecurityCasus.Models
{
    public class MyData
    {
        public int Id { get; set; }

        [Required]
        public string Test { get; set; }
    }
}
