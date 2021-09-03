using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ApiShort.Models
{
    public class FacilityView
    {
        private ICollection<FacilityView> childFacilities;
        private ICollection<FacilityView> parentFacilities;

        public FacilityView()
        {
            childFacilities = new HashSet<FacilityView>();
            parentFacilities = new HashSet<FacilityView>();
            CreationDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }

        public bool IsNew { get; } = true;

        public int Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; protected set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime UpdateDate { get; set; }

        public FacilityLevel FacilityLevel =>
            !GetParentFacilities().Any()
            ? FacilityLevel.First
            : FacilityLevel.Second;

        public int? ProjectId { get; set; }

        public ProjectView Project { get; set; }

        public virtual IEnumerable<FacilityView> GetChildFacilities()
        {
            return childFacilities;
        }

        public virtual void AddChildFacilities(FacilityView value)
        {
            if (GetParentFacilities().Any())
                throw new ArgumentException("Facility of second level can not to have child facilities.");

            childFacilities.Add(value);
        }

        public virtual ICollection<FacilityView> GetParentFacilities()
        {
            return parentFacilities;
        }

        public virtual void AddParentFacilities(FacilityView value)
        {
            if (GetChildFacilities().Any())
                throw new ArgumentException("Facility of first level can not to have parent facilities.");

            parentFacilities.Add(value);
        }
    }
}