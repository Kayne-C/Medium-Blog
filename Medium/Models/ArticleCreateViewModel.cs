using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Medium.Models
{
    public class ArticleCreateViewModel
    {
        [Required(ErrorMessage="Başlık boş geçilemez.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "İçerik boş geçilemez.")]
        public string Content { get; set; }
        [Display(Name = "Article Picture")]
        public IFormFile ArticlePicture { get; set; }
    }
}
