using System;

namespace Shipwreck.Phash.Imaging
{
    public interface IImageOperatorProvider<T>
        where T : struct, IEquatable<T>
    {
        IImageOperator<T> GetOperator();
    }
}