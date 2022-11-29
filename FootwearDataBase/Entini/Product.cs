namespace FootwearDataBase.Entini
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        [StringLength(100)]
        public string ProductArticle { get; set; }

        [Required]
        [StringLength(100)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string ProductDecription { get; set; }

        [StringLength(100)]
        public string ProductPhoto { get; set; }

        public double ProductDiscount { get; set; }

        public double ProductCost { get; set; }

        public int ProductManufactureId { get; set; }

        public int ProductProviderId { get; set; }

        public int ProductCategoryId { get; set; }

        public int ProductUnitID { get; set; }

        public int ProductCount { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
    }
}
