using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace BlogProject.Models
{
    public class HomePageModel
    {
        public List<Post> Post { get; set; }

        public SortedDictionary<string,int> TagCount { get; set; }
    }
}