﻿using System;

namespace Shipwreck.Phash.Imaging
{
    public static class ImageExtensions
    {
        private static IImageOperator<T> GetOperator<T>(this IImage<T> image)
            where T : struct, IEquatable<T>
            => (image as IImageOperatorProvider<T>)?.GetOperator()
                ?? new ImageAccessorOperator<T, GenericImageAccessor<T>>(new GenericImageAccessor<T>(image));

        public static IImage<T> Transpose<T>(this IImage<T> image)
            where T : struct, IEquatable<T>
            => image.GetOperator().Transpose();
    }
}