using System.ComponentModel.DataAnnotations;

namespace AbySalto.Mid.WebApi.Models
{
    public class BasketItem
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 0;
    }
}
