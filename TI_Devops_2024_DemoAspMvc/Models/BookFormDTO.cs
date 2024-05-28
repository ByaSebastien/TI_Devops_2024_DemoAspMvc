using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TI_Devops_2024_DemoAspMvc.Models
{
    public class BookFormDTO
    {
        [DisplayName("ISBN")]
        [Required]
        [StringLength(13,MinimumLength = 11,ErrorMessage = "Isbn doit faire 11 ou 13 caractère")]
        public string ISBN { get; set; }

        [DisplayName("Titre")]
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [DisplayName("Date de publication")]
        [DataType(DataType.DateTime)]
        public DateTime PublishDate { get; set; }

        public int AuthorId { get; set; }
    }
}
