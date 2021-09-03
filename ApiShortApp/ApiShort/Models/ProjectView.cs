using ApiShort.DAL.EFDbModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiShort.Models
{
    public class ProjectView
    {
        public ProjectView()
        {
            Facilities = new HashSet<FacilityView>();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        public bool IsNew { get; } = true;

        public int Id { get; set; }

        [Required]
        public string Cipher { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; protected set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }

        public virtual ICollection<FacilityView> Facilities { get; set; }

    }
}