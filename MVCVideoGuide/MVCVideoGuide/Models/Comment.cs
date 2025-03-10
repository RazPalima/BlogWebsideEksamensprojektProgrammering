using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }
        [Required]
        public string User { get; set; } = "none";
        [Required]
        public string Text { get; set; } = "none";
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;
    }
}
