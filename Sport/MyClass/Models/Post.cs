using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyClass.Models
{
    [Table("Posts")]
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public int? TopicId { get; set; }
        [Required]
        public String Title { get; set; }
        public String Slug { get; set; }
        [Required]
        public String Detail { get; set; }
        public String Description { get; set; }
        public String Img { get; set; }
        public String PostType { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_At { get; set; }
        public int? Updated_By { get; set; }
        public DateTime? Updated_At { get; set; }
        public int Status { get; set; }

        //public void Update(Post post)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
