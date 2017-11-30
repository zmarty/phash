using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shipwreck.Phash.Imaging
{
    public static class ImageExtensions
    {
        public static void TransposeTo<T>(this IImage<T> source, IImage<T> dest)
            where T : struct, IEquatable<T>
        {
            var w = source.Width;
            var h = source.Height;

            if (dest.Height != w || dest.Width != h)
            {
                throw new ArgumentException();
            }

            var sa = (source as IArrayImage<T>)?.Array;
            var da = (dest as IArrayImage<T>)?.Array;

            if (sa != null && da != null)
            {
                var i = 0;
                for (var sy = 0; sy < h; sy++)
                {
                    for (var sx = 0; sx < w; sx++)
                    {
                        da[sy + h * sx] = sa[i++];
                    }
                }
            }
            else
            {
                for (var sy = 0; sy < h; sy++)
                {
                    for (var sx = 0; sx < w; sx++)
                    {
                        dest[sy, sx] = source[sx, sy];
                    }
                }
            }
        }

        internal static TAccessor TransposeCore<T, TAccessor>(this TAccessor source)
          where T : struct, IEquatable<T>
            where TAccessor : IImageAccessor<T>
        {
            var w = source.Width;
            var h = source.Height;

            var dest = (TAccessor)source.CreateNew(h, w);

            if (dest.Height != w || dest.Width != h)
            {
                throw new ArgumentException();
            }

            var sa = (source as IArrayImage<T>)?.Array;
            var da = (dest as IArrayImage<T>)?.Array;

            if (sa != null && da != null)
            {
                var i = 0;
                for (var sy = 0; sy < h; sy++)
                {
                    for (var sx = 0; sx < w; sx++)
                    {
                        da[sy + h * sx] = sa[i++];
                    }
                }
            }
            else
            {
                for (var sy = 0; sy < h; sy++)
                {
                    for (var sx = 0; sx < w; sx++)
                    {
                        dest[sy, sx] = source[sx, sy];
                    }
                }
            }

            return dest;
        }
    }
}
