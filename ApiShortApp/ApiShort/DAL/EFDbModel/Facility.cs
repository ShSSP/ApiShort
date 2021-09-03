using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiShort.DAL.EFDbModel
{
    public class Facility
    {
        public Facility()
        {
            ChildFacilities = new HashSet<Facility>();
            ParentFacilities = new HashSet<Facility>();
        }

        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }

        public int? ProjectId { get; set; }

        public virtual Project Project { get; set; }

        public virtual ICollection<Facility> ChildFacilities { get; set; }

        public virtual ICollection<Facility> ParentFacilities { get; set; }
    }
}