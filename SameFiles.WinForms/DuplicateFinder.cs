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
        public static Task<List<DuplicateFile>> FindDuplicatesAsync(string rootPath)
            => Task.Run(() => FindDuplicates(rootPath));

        private static List<DuplicateFile> FindDuplicates(string rootPath)
        {
            var result = new List<DuplicateFile>();
            if (!Directory.Exists(rootPath)) return result;

            var files = Directory.EnumerateFiles(rootPath, "*", SearchOption.AllDirectories);
            var sizeGroups = files.GroupBy(f => new FileInfo(f).Length)
                                  .Where(g => g.Count() > 1);

            foreach (var sizeGroup in sizeGroups)
            {
                var hashGroups = sizeGroup.GroupBy(f => ComputeHash(f))
                                          .Where(g => g.Count() > 1);
                foreach (var hashGroup in hashGroups)
                {
                    var list = hashGroup.ToList();
                    var count = list.Count;
                    foreach (var file in list)
                        result.Add(new DuplicateFile(file, hashGroup.Key, count));
                }
            }

            return result;
        }

        private static string ComputeHash(string path)
        {
            using var sha = SHA256.Create();
            using var stream = File.OpenRead(path);
            var hash = sha.ComputeHash(stream);
            return Convert.ToHexString(hash);
        }
    }
}
