using System;

namespace Shipwreck.Phash.Imaging
{
    /// <summary>
    /// Supports optimizations to access underlying <see cref="IImage{T}"/> without virtual call.
    /// </summary>
    /// <typeparam name="T">The type of value of elements in the image.</typeparam>
    internal interface IImageAccessor<T> : IImage<T>
        where T : struct, IEquatable<T>
    {
        IImage<T> Image { get; }

        IImageAccessor<T> CreateNew(int width, int height);

        #region Arithmetic Operators

        T Zero { get; }

        T One { get; }

        bool SupportsReciprocal { get; }

        T Add(T left, T right);

        T Subtract(T left, T right);

        T Multiply(T left, T right);

        T Divide(T left, T right);

        #endregion Arithmetic Operators
    }
}