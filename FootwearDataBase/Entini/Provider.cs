namespace FootwearDataBase.Entini
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Provider")]
    public partial class Provider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProviderID { get; set; }

        [Required]
        [StringLength(50)]
        public string ProviderName { get; set; }
    }
}
