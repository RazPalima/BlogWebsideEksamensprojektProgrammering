using System.ComponentModel.DataAnnotations;

namespace MVCVideoGuide.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public string User { get; set; }
        [Required]
        public string Text { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
