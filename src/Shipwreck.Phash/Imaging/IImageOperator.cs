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
        IImage<T> Transpose();
    }
}