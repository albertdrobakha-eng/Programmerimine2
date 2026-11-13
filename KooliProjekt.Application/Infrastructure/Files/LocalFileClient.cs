using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Infrastructure.Files
{
    public class LocalFileClient : IFileClient
    {
        private readonly string _filesRoot;
        public LocalFileClient(string filesRoot) => _filesRoot = filesRoot;

        public async Task Save(string fileName, string fileStore, Stream inputStream)
        {
            var path = GetFilePath(fileName, fileStore);
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            using var output = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            await inputStream.CopyToAsync(output);
        }

        public Task Delete(string fileName, string fileStore)
        {
            var path = GetFilePath(fileName, fileStore);
            if (File.Exists(path)) File.Delete(path);
            return Task.CompletedTask;
        }

        public Task<Stream> Get(string fileName, string fileStore)
        {
            var path = GetFilePath(fileName, fileStore);
            return Task.FromResult<Stream>(File.Exists(path) ? File.OpenRead(path) : null);
        }

        public Task<IReadOnlyList<string>> List(string fileStore)
        {
            var path = GetStorePath(fileStore);
            IReadOnlyList<string> names = Directory.Exists(path)
                ? Directory.GetFiles(path).Select(Path.GetFileName).OrderBy(x => x).ToList()
                : new List<string>();
            return Task.FromResult(names);
        }

        private string GetFilePath(string fileName, string fileStore)
        {
            if (string.IsNullOrWhiteSpace(fileName) || Path.GetFileName(fileName) != fileName || fileName.Contains('/') || fileName.Contains('\\'))
                throw new InvalidOperationException("File name must be a plain file name.");
            return Path.Combine(GetStorePath(fileStore), fileName);
        }

        private string GetStorePath(string fileStore)
        {
            if (string.IsNullOrWhiteSpace(fileStore) || Path.IsPathRooted(fileStore))
                throw new InvalidOperationException("File store must be a relative path.");
            var root = Path.GetFullPath(_filesRoot);
            var fullPath = Path.GetFullPath(Path.Combine(root, fileStore));
            if (!fullPath.StartsWith(root.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("File store is outside FilesRoot.");
            return fullPath;
        }
    }
}
