namespace KooliProjekt.Application.Dto
{
    public class MonthlyValueDto
    {
        public int Id { get; set; }
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string Ticker { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Quantity { get; set; }
        public decimal Value { get; set; }
        public decimal Total { get; set; }
    }
}
