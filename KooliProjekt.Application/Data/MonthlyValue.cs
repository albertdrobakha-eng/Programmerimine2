using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
namespace KooliProjekt.Application.Data
{
    public class MonthlyValue
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        [Range(1, 9999)]
        public int Year { get; set; }
        [Range(1, 12)]
        public int Month { get; set; }
        [Precision(18, 4)]
        public decimal Quantity { get; set; }
        [Precision(18, 2)]
        public decimal Value { get; set; }
        [Precision(18, 2)]
        public decimal Total { get; set; }
    }
}
