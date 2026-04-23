using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Service.DTOs
{
    public class GetBooksDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int PublishingYear { get; set; }

        public override string ToString() => $"{Id}. {Name} by: {Author} Written in: {PublishingYear}";
    }
}
