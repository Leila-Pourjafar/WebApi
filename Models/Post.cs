using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class Post
    {
        public Post()
        {
            CreateDate = DateTime.Now;
            IsActive = true;
        }
        public int Id { get; set; }

        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public int ViewCount { get; set; }
        public int CreatorId { get; set; }

        public string Subject { get; set; }
        public virtual User CurrentUser { get; set; }
        public string ImgSrc { get; set; }
    }
}
