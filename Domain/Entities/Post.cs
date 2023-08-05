using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
