using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiShort.DAL.EFDbModel
{
    public class Project
    {        public Project()
        {
            Facilities = new HashSet<Facility>();
        }

        public int Id { get; set; }

        [Required]
        public string Cipher { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<Facility> Facilities { get; set; }
    }
}