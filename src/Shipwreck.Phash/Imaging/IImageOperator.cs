using System;

namespace Shipwreck.Phash.Imaging
{
    /// <summary>
    /// Supports known image transformations
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IImageOperator<T>
        where T : struct, IEquatable<T>
    {
        #region Arithmetic Operations

        void AddedBy(T value);

        void SubtractedBy(T value);

        void MultipliedBy(T value);

        void DividedBy(T value);

        #endregion Arithmetic Operations

        IImage<T> Transpose();
    }
}