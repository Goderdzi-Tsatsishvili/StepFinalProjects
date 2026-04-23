using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Service.DTOs
{
    public class CreateBookDTO
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublishingYear { get; set; }
    }
}
