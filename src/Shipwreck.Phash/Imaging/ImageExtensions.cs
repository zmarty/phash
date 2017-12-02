using System;

namespace Shipwreck.Phash.Imaging
{
    public static class ImageExtensions
    {
        internal static IImageAccessor<T> GetAccessor<T>(this IImage<T> image)
            where T : struct, IEquatable<T>
            => image as IImageAccessor<T>
                ?? new GenericImageAccessor<T>(image);

        internal static IImageOperator<T> GetOperator<T>(this IImage<T> image)
            where T : struct, IEquatable<T>
            => (image as IImageOperatorProvider<T>)?.GetOperator()
                ?? new ImageAccessorOperator<T, GenericImageAccessor<T>>(new GenericImageAccessor<T>(image));

        #region Arithmetic Operations

        public static void AddedBy<T>(this IImage<T> image, T value)
            where T : struct, IEquatable<T>
            => image.GetOperator().AddedBy(value);

        public static void SubtractedBy<T>(this IImage<T> image, T value)
            where T : struct, IEquatable<T>
            => image.GetOperator().SubtractedBy(value);

        public static void MultipliedBy<T>(this IImage<T> image, T value)
            where T : struct, IEquatable<T>
            => image.GetOperator().MultipliedBy(value);

        public static void DividedBy<T>(this IImage<T> image, T value)
            where T : struct, IEquatable<T>
            => image.GetOperator().DividedBy(value);

        #endregion Arithmetic Operations

        public static IImage<T> Transpose<T>(this IImage<T> image)
            where T : struct, IEquatable<T>
            => image.GetOperator().Transpose();
    }
}