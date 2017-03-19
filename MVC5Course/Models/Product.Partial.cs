namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Validations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [DisplayName("商品名稱")]
        [商品名稱不能有Test]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 1000, ErrorMessage = "({0})最低100!!")]
        [DisplayName("商品價格")]
        [DisplayFormat(DataFormatString = "NT$ {0:N0}")] //顯示NT$，但不顯示小數點
        public Nullable<decimal> Price { get; set; }

        public Nullable<bool> Active { get; set; }

        [Required]
        [DisplayName("商品庫存量")]
        [Range(0,9999)]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
