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

        #region Arithmetic Operations

        public void AddedBy(TValue value)
        {
            var w = _Image.Width;
            var h = _Image.Height;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    _Image[x, y] = _Image.Add(_Image[x, y], value);
                }
            }
        }

        public void SubtractedBy(TValue value)
        {
            var w = _Image.Width;
            var h = _Image.Height;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    _Image[x, y] = _Image.Subtract(_Image[x, y], value);
                }
            }
        }

        public void MultipliedBy(TValue value)
        {
            var w = _Image.Width;
            var h = _Image.Height;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    _Image[x, y] = _Image.Multiply(_Image[x, y], value);
                }
            }
        }

        public void DividedBy(TValue value)
        {
            if (_Image.SupportsReciprocal)
            {
                MultipliedBy(_Image.Divide(_Image.One, value));
                return;
            }

            var w = _Image.Width;
            var h = _Image.Height;

            for (var y = 0; y < h; y++)
            {
                for (var x = 0; x < w; x++)
                {
                    _Image[x, y] = _Image.Divide(_Image[x, y], value);
                }
            }
        }

        #endregion Arithmetic Operations

        #region Transpose

        public IImage<TValue> Transpose()
        {
            var w = _Image.Width;
            var h = _Image.Height;
            var dest = _Image.CreateNew(h, w).GetAccessor();

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
                if (dest is TAccessor)
                {
                    TransposeCore((TAccessor)dest);
                }
                else
                {
                    TransposeCore(dest);
                }
            }

            return dest;
        }

        private void TransposeCore<TDest>(TDest dest)
            where TDest : IImageAccessor<TValue>
        {
            var w = _Image.Width;
            var h = _Image.Height;

            for (var sy = 0; sy < h; sy++)
            {
                for (var sx = 0; sx < w; sx++)
                {
                    dest[sy, sx] = _Image[sx, sy];
                }
            }
        }

        #endregion Transpose
    }
}