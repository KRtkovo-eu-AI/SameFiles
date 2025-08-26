using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace SameFiles.WinForms
{
    public static class DuplicateFinder
    {
        public static Task<List<DuplicateFile>> FindDuplicatesAsync(string rootPath, IProgress<ScanProgress>? progress = null)
            => Task.Run(() => FindDuplicates(rootPath, progress));

        private static List<DuplicateFile> FindDuplicates(string rootPath, IProgress<ScanProgress>? progress)
        {
            var result = new List<DuplicateFile>();
            if (!Directory.Exists(rootPath)) return result;

            var allFiles = EnumerateFilesSafe(rootPath).ToList();
            var sizeGroups = allFiles.GroupBy(f => new FileInfo(f).Length);
            int total = allFiles.Count;
            int scanned = 0;
            progress?.Report(new ScanProgress(scanned, total));

            var hashGroups = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
            foreach (var sizeGroup in sizeGroups)
            {
                if (sizeGroup.Count() < 2)
                {
                    scanned += sizeGroup.Count();
                    progress?.Report(new ScanProgress(scanned, total));
                    continue;
                }

                foreach (var file in sizeGroup)
                {
                    string hash;
                    try
                    {
                        hash = ComputeHash(file);
                    }
                    catch (IOException)
                    {
                        scanned++;
                        progress?.Report(new ScanProgress(scanned, total));
                        continue;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        scanned++;
                        progress?.Report(new ScanProgress(scanned, total));
                        continue;
                    }

                    if (!hashGroups.TryGetValue(hash, out var list))
                    {
                        list = new List<string>();
                        hashGroups[hash] = list;
                    }
                    list.Add(file);

                    scanned++;
                    progress?.Report(new ScanProgress(scanned, total));
                }
            }

            foreach (var pair in hashGroups)
            {
                if (pair.Value.Count > 1)
                {
                    var count = pair.Value.Count;
                    foreach (var file in pair.Value)
                        result.Add(new DuplicateFile(file, pair.Key, count));
                }
            }

            return result;
        }

        private static IEnumerable<string> EnumerateFilesSafe(string root)
        {
            var stack = new Stack<string>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                var current = stack.Pop();
                string[] files = Array.Empty<string>();
                try
                {
                    files = Directory.GetFiles(current);
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                foreach (var file in files)
                    yield return file;

                string[] dirs = Array.Empty<string>();
                try
                {
                    dirs = Directory.GetDirectories(current);
                }
                catch (IOException) { }
                catch (UnauthorizedAccessException) { }
                foreach (var dir in dirs)
                    stack.Push(dir);
            }
        }

        private static string ComputeHash(string path)
        {
            using var sha = SHA256.Create();
            using var stream = File.OpenRead(path);
            var hash = sha.ComputeHash(stream);
            return Convert.ToHexString(hash);
        }
    }

    public record ScanProgress(int Scanned, int Total);
}
