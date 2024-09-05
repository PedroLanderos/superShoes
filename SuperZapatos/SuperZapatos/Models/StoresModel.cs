using System.ComponentModel.DataAnnotations;

namespace SuperZapatos.Models
{
    public class StoresModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Address { get; set; }

        //relation with the other model
        public ICollection<ArticlesModel> Articles { get; set; }
    }
}
