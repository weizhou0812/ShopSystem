using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Models
{
    public class Products
    {
        [Key]
        public int PNumber { get; set; }
        public string PId { get; set; }
        public string PName { get; set; }
        public double Price { get; set; }

        [Comment("保质期")]
        public int SellbyDate { get; set; }
        [Comment("生产日期")]
        public DateTime ProduceDate { get; set; }
        [Comment("入库时间")]
        public DateTime WarehousingDate { get; set; }
        [Comment("库存")]
        public int Inventory { get; set; }
        [Comment("是否过期")]
        public bool IsExpire { get; set; }

    }
}
