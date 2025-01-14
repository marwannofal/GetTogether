using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set;}
        public string Url { get; set;}
        public bool IsMain { get; set; }
        public string publicId { get; set; }
        public bool IsApproved { get; set; } = false;

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}