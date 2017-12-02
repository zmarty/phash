using System;

namespace Shipwreck.Phash.Imaging
{
    internal sealed class ImageAccessorOperator<TValue, TAccessor> : IImageOperator<TValue>
        where TValue : struct, IEquatable<TValue>
        where TAccessor : IImageAccessor<TValue>
    {
        private readonly TAccessor _Image;

        public ImageAccessorOperator(TAccessor image)
        {
            _Image = image;
        }

        public IImage<TValue> Transpose()
        {
            var w = _Image.Width;
            var h = _Image.Height;

            var dest = (TAccessor)_Image.CreateNew(h, w);

            if (dest.Height != w || dest.Width != h)
            {
                throw new ArgumentException();
            }

            var sa = (_Image as IArrayImage<TValue>)?.Array;
            var da = (dest as IArrayImage<TValue>)?.Array;

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
                        dest[sy, sx] = _Image[sx, sy];
                    }
                }
            }

            return dest;
        }
    }
}