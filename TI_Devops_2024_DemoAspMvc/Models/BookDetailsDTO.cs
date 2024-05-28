using System.ComponentModel;
using TI_Devops_2024_DemoAspMvc.Domain.Entities;

namespace TI_Devops_2024_DemoAspMvc.Models
{
    public class BookDetailsDTO
    {
        [DisplayName("ISBN")]
        public string ISBN { get; set; } = null!;
        [DisplayName("Titre")]
        public string Title { get; set; } = null!;
        [DisplayName("Description")]
        public string? Description { get; set; }
        [DisplayName("Date de publication")]
        public DateTime PublishDate { get; set; }
        [DisplayName("Auteur")]
        public Author Author { get; set; } = null!;
    }
}
