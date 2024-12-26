using WebApp.Core.Domain.Entities;

namespace WebApp.Core.DTOs
{
    public class SupplementOrderResponseDTO
    {
        public int SupplementOrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
    }
    public static class SupplementOrderExtension
    {
        public static SupplementOrderResponseDTO ToSupplementResponseDTO(this SupplementOrder supplementOrder)
        {
            return new SupplementOrderResponseDTO
            {
                SupplementOrderId = supplementOrder.SupplementOrderId,
                OrderDate = supplementOrder.OrderDate,
                Quantity = supplementOrder.Quantity,
                TotalPrice = supplementOrder.TotalPrice,
            };
        }
    }
}
