using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Models
{
    public class LessonType
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedAt { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
