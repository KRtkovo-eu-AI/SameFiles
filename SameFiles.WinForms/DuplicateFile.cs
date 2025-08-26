using System.Drawing;
using System.IO;

namespace SameFiles.WinForms
{
    public class DuplicateFile
    {
        public string Path { get; }
        public string Name => System.IO.Path.GetFileName(Path);
        public long Size { get; }
        public string Hash { get; }
        public int GroupCount { get; }
        public bool Delete { get; set; }
        public Image? Preview { get; }

        public DuplicateFile(string path, string hash, int groupCount)
        {
            Path = path;
            Hash = hash;
            GroupCount = groupCount;
            Size = new FileInfo(path).Length;
            Preview = ThumbnailProvider.GetThumbnail(path, 96, 96);
        }

        public string Directory => System.IO.Path.GetDirectoryName(Path) ?? string.Empty;
    }
}
