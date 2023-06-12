using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models
{
    public class Users
    {
        [Key]
        public int UNumber { get; set; }
        public string UId { get; set; }
        public string UName { get; set; }
        public string UPassword { get; set; }
        [Comment("身份")]
        public string UIdentity { get; set; }
        public bool IsOnJob { get; set; }
    }
}
