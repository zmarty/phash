using System;

namespace Shipwreck.Phash.Imaging
{
    internal interface IImageAccessor<T> : IImage<T>
        where T : struct, IEquatable<T>
    {
        IImage<T> Image { get; }

        IImageAccessor<T> CreateNew(int width, int height);
    }
}