using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopSystem.Models
{
    public class Employees
    {
        [Key]
        public int ENumber { get; set; }
        public string EId { get; set; }
        public string EName { get; set; }
        public string ESex { get; set; }
        public string EAge { get; set; }
        [Column(TypeName ="date")]
        public DateTime EBirth { get; set; }
        public string EEmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        [Comment("身份")]
        public string EIdentity { get; set; }
        public DateTime CreatedDate { get; set;}
        public bool IsOnJob { get; set; }
    }
}
