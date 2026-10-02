using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace KooliProjekt.Application.Data
{
    public class Asset
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Required, StringLength(20)]
        public string Ticker { get; set; }
        public int PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
        public int AssetClassId { get; set; }
        public AssetClass AssetClass { get; set; }
        public ICollection<MonthlyValue> MonthlyValues { get; set; } = new List<MonthlyValue>();
    }
}
