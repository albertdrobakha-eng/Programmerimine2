namespace KooliProjekt.Application.Dto
{
    public class AssetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ticker { get; set; }
        public int PortfolioId { get; set; }
        public string PortfolioName { get; set; }
        public int AssetClassId { get; set; }
        public string AssetClassName { get; set; }
    }
}
