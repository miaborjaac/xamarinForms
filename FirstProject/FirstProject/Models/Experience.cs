using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstProject.Models
{
    class Experience
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
