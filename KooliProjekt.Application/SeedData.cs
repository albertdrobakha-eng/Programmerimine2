using System;
using System.Collections.Generic;
using System.Linq;

namespace KooliProjekt.Application.Data
{
    public static class SeedData
    {
        public static void Generate(ApplicationDbContext context)
        {
            if (!context.Portfolios.Any())
            {
                SeedPortfolios(context);
            }

            if (!context.AssetClasses.Any())
            {
                SeedAssetClasses(context);
            }

            if (!context.Assets.Any())
            {
                SeedAssets(context);
            }

            if (!context.MonthlyValues.Any())
            {
                SeedMonthlyValues(context);
            }
        }

        private static void SeedPortfolios(ApplicationDbContext context)
        {
            var portfolios = new List<Portfolio>
            {
                new Portfolio { Name = "Pension Growth" },
                new Portfolio { Name = "Baltic Balanced" },
                new Portfolio { Name = "Global Dividend" },
                new Portfolio { Name = "Nordic Equity" },
                new Portfolio { Name = "European Value" },
                new Portfolio { Name = "Technology Leaders" },
                new Portfolio { Name = "Sustainable Future" },
                new Portfolio { Name = "Emerging Markets" },
                new Portfolio { Name = "US Blue Chip" },
                new Portfolio { Name = "Global Bond" },
                new Portfolio { Name = "Healthcare Growth" },
                new Portfolio { Name = "Clean Energy" },
                new Portfolio { Name = "Real Estate Income" },
                new Portfolio { Name = "Small Companies" },
                new Portfolio { Name = "Monthly Income" },
                new Portfolio { Name = "Capital Preservation" },
                new Portfolio { Name = "Asia Pacific Growth" },
                new Portfolio { Name = "Quality Stocks" },
                new Portfolio { Name = "Infrastructure Income" },
                new Portfolio { Name = "Long Term Growth" },
                new Portfolio { Name = "Euro Stability" },
                new Portfolio { Name = "Global Innovation" },
                new Portfolio { Name = "Climate Transition" },
                new Portfolio { Name = "Consumer Leaders" },
                new Portfolio { Name = "Financial Sector" },
                new Portfolio { Name = "Industrial Growth" },
                new Portfolio { Name = "Low Volatility" },
                new Portfolio { Name = "International Equity" },
                new Portfolio { Name = "Retirement 2040" },
                new Portfolio { Name = "Retirement 2050" },
                new Portfolio { Name = "Family Reserve" },
                new Portfolio { Name = "Education Fund" },
                new Portfolio { Name = "Balanced Income" },
                new Portfolio { Name = "Digital Economy" },
                new Portfolio { Name = "World Markets" }
            };

            context.Portfolios.AddRange(portfolios);
            context.SaveChanges();
        }

        private static void SeedAssetClasses(ApplicationDbContext context)
        {
            var assetClasses = new List<AssetClass>
            {
                new AssetClass { Name = "US Large Cap" },
                new AssetClass { Name = "US Mid Cap" },
                new AssetClass { Name = "US Small Cap" },
                new AssetClass { Name = "Canadian Equity" },
                new AssetClass { Name = "UK Equity" },
                new AssetClass { Name = "German Equity" },
                new AssetClass { Name = "French Equity" },
                new AssetClass { Name = "Nordic Equity" },
                new AssetClass { Name = "Japanese Equity" },
                new AssetClass { Name = "Chinese Equity" },
                new AssetClass { Name = "Indian Equity" },
                new AssetClass { Name = "Emerging Market Equity" },
                new AssetClass { Name = "Global Equity" },
                new AssetClass { Name = "Technology" },
                new AssetClass { Name = "Healthcare" },
                new AssetClass { Name = "Financial Services" },
                new AssetClass { Name = "Consumer Staples" },
                new AssetClass { Name = "Consumer Discretionary" },
                new AssetClass { Name = "Industrials" },
                new AssetClass { Name = "Energy" },
                new AssetClass { Name = "Utilities" },
                new AssetClass { Name = "Real Estate" },
                new AssetClass { Name = "Clean Energy" },
                new AssetClass { Name = "Government Bonds" },
                new AssetClass { Name = "Corporate Bonds" },
                new AssetClass { Name = "Municipal Bonds" },
                new AssetClass { Name = "Inflation-Protected Bonds" },
                new AssetClass { Name = "High Yield Bonds" },
                new AssetClass { Name = "Investment Grade Bonds" },
                new AssetClass { Name = "Global Bonds" },
                new AssetClass { Name = "Gold" },
                new AssetClass { Name = "Commodities" },
                new AssetClass { Name = "Infrastructure" },
                new AssetClass { Name = "Cash" },
                new AssetClass { Name = "Exchange-Traded Funds" }
            };

            context.AssetClasses.AddRange(assetClasses);
            context.SaveChanges();
        }

        private static void SeedAssets(ApplicationDbContext context)
        {
            var assetData = new[]
            {
                ("Apple Inc.", "AAPL"),
                ("Microsoft Corporation", "MSFT"),
                ("NVIDIA Corporation", "NVDA"),
                ("Amazon.com Inc.", "AMZN"),
                ("Alphabet Inc.", "GOOGL"),
                ("Meta Platforms Inc.", "META"),
                ("Berkshire Hathaway", "BRK.B"),
                ("JPMorgan Chase", "JPM"),
                ("Visa Inc.", "V"),
                ("Johnson & Johnson", "JNJ"),
                ("Procter & Gamble", "PG"),
                ("Coca-Cola Company", "KO"),
                ("Exxon Mobil Corporation", "XOM"),
                ("Walmart Inc.", "WMT"),
                ("Broadcom Inc.", "AVGO"),
                ("Tesla Inc.", "TSLA"),
                ("Netflix Inc.", "NFLX"),
                ("PepsiCo Inc.", "PEP"),
                ("Oracle Corporation", "ORCL"),
                ("Cisco Systems", "CSCO"),
                ("Adobe Inc.", "ADBE"),
                ("Salesforce Inc.", "CRM"),
                ("Merck & Co.", "MRK"),
                ("Chevron Corporation", "CVX"),
                ("McDonald's Corporation", "MCD"),
                ("Qualcomm Inc.", "QCOM"),
                ("Texas Instruments", "TXN"),
                ("IBM Corporation", "IBM"),
                ("Nike Inc.", "NKE"),
                ("The Home Depot", "HD"),
                ("ASML Holding", "ASML"),
                ("Toyota Motor Corporation", "TM"),
                ("Novo Nordisk", "NVO"),
                ("SAP SE", "SAP"),
                ("iShares Core S&P 500 ETF", "IVV")
            };

            var portfolios = context.Portfolios.OrderBy(p => p.Id).ToList();
            var assetClasses = context.AssetClasses.OrderBy(c => c.Id).ToList();

            var assets = new List<Asset>();

            for (var i = 0; i < assetData.Length; i++)
            {
                assets.Add(new Asset
                {
                    Name = assetData[i].Item1,
                    Ticker = assetData[i].Item2,
                    PortfolioId = portfolios[i].Id,
                    AssetClassId = assetClasses[i].Id
                });
            }

            context.Assets.AddRange(assets);
            context.SaveChanges();
        }

        private static void SeedMonthlyValues(ApplicationDbContext context)
        {
            var assets = context.Assets.OrderBy(a => a.Id).ToList();
            var monthlyValues = new List<MonthlyValue>();

            for (var i = 0; i < 35; i++)
            {
                var quantity = 2m + (i % 20);
                var unitPrice = 50m + (i * 17.35m);

                monthlyValues.Add(new MonthlyValue
                {
                    AssetId = assets[i].Id,
                    Year = 2026,
                    Month = 9,
                    Quantity = quantity,
                    Value = unitPrice,
                    Total = Math.Round(quantity * unitPrice, 2)
                });
            }

            context.MonthlyValues.AddRange(monthlyValues);
            context.SaveChanges();
        }
    }
}
