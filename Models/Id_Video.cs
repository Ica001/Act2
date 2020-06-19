using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CainAbel.Models
{
    public class Id_Video
    {
        public int Id { get;  }
        public string Video { get; }
        public string Type { get; }
        public Id_Video(int id,string video,string type)
        {
            Type = type;
            Id = id;
            Video = video;
        }
    }
}
