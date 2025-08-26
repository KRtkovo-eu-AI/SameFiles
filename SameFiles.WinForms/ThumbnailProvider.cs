using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace SameFiles.WinForms
{
    public static class ThumbnailProvider
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        private static extern void SHCreateItemFromParsingName(string path, IntPtr pbc, ref Guid riid, out IShellItemImageFactory ppv);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [StructLayout(LayoutKind.Sequential)]
        private struct SIZE
        {
            public int cx;
            public int cy;
        }

        [Flags]
        private enum SIIGBF
        {
            SIIGBF_RESIZETOFIT = 0x00,
            SIIGBF_BIGGERSIZEOK = 0x01,
            SIIGBF_MEMORYONLY = 0x02,
            SIIGBF_ICONONLY = 0x04,
            SIIGBF_THUMBNAILONLY = 0x08,
            SIIGBF_INCACHEONLY = 0x10,
        }

        [ComImport]
        [Guid("bcc18b79-ba16-442f-80c4-8a59c30c463b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IShellItemImageFactory
        {
            void GetImage(SIZE size, SIIGBF flags, out IntPtr phbm);
        }

        public static Image? GetThumbnail(string path, int width, int height)
        {
            try
            {
                var guid = typeof(IShellItemImageFactory).GUID;
                SHCreateItemFromParsingName(path, IntPtr.Zero, ref guid, out var factory);
                var size = new SIZE { cx = width, cy = height };
                factory.GetImage(size, SIIGBF.SIIGBF_RESIZETOFIT, out var hBmp);
                var img = Image.FromHbitmap(hBmp);
                DeleteObject(hBmp);
                return img;
            }
            catch
            {
                try
                {
                    using var icon = Icon.ExtractAssociatedIcon(path);
                    return icon?.ToBitmap();
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
