using System;

namespace Shipwreck.Phash.Imaging
{
    internal struct GenericImageAccessor<T> : IImageAccessor<T>
        where T : struct, IEquatable<T>
    {
        private static readonly Func<IImage<T>, int, int, IImageAccessor<T>> _Creator;

        static GenericImageAccessor()
        {
            if (typeof(T) == typeof(byte))
            {
                _Creator = (img, w, h) => CreateNew(img, w, h) ?? (IImageAccessor<T>)(object)new ByteImageAccessor(new ByteImage(w, h));
            }
            else if (typeof(T) == typeof(float))
            {
                _Creator = (img, w, h) => CreateNew(img, w, h) ?? (IImageAccessor<T>)(object)new FloatImageAccessor(new FloatImage(w, h));
            }
            else
            {
                _Creator = (img, w, h) => CreateNew(img, w, h, true);
            }
        }

        public GenericImageAccessor(IImage<T> image)
        {
            Image = image;
        }

        public IImage<T> Image { get; }

        public int Width => Image.Width;
        public int Height => Image.Height;

        public T this[int x, int y]
        {
            get => Image[x, y];
            set => Image[x, y] = value;
        }

        public IImageAccessor<T> CreateNew(int width, int height)
            => _Creator(Image, width, height);

        private static IImageAccessor<T> CreateNew(IImage<T> current, int width, int height, bool thrownOnNotFound = false)
        {
            var ia = current as IImageAccessor<T>;
            if (ia != null)
            {
                return ia.CreateNew(width, height);
            }
            if (thrownOnNotFound)
            {
                throw new PlatformNotSupportedException($"Generic image of element type \"{typeof(T)}\" is not supported");
            }
            return null;
        }
    }
}