namespace KooliProjekt.Application.Infrastructure.Files
{
    public static class FileStore
    {
        public const string Photos = "photos";
        public const string Invoices = "invoices";
        public static string AssetPhotos(int assetId) => $"{Photos}/assets/{assetId}";
    }
}
