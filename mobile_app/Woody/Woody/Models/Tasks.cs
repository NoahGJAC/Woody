using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Woody.Enums;

namespace Woody.Models
{
    public class Tasks
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public TaskType Tag { get; set; }
    }
}
