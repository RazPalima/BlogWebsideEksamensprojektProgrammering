﻿namespace MVCVideoGuide.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string User { get; set; }
        public string Text { get; set; }
        public Blog? Blog { get; set; }
    }
}
