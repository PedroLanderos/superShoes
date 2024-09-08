using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SuperZapatos.Models
{
    public class ArticlesModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int TotalInShelf { get; set; }

        [Required]
        public int TotalInVault { get; set; }

        // Foreign key to StoresModel
        [ForeignKey("Store")]
        public int StoreId { get; set; }

        // Navigation property to StoresModel
        public StoresModel? Store { get; set; }
    }
}
