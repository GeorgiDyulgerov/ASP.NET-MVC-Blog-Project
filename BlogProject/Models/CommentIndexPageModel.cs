using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogProject.Models
{
    public class CommentIndexPageModel
    {
        public Comment Comments { get; set; }

        public Post Posts { get; set; }

    }
}