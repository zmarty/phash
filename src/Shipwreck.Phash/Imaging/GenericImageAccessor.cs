using System;
using System.Linq.Expressions;
using E = System.Linq.Expressions.Expression;

namespace Shipwreck.Phash.Imaging
{
    internal struct GenericImageAccessor<T> : IImageAccessor<T>
        where T : struct, IEquatable<T>
    {
        #region Initialize static fields

        private static readonly Func<IImage<T>, int, int, IImageAccessor<T>> _Creator;

        private static readonly T _Zero;
        private static readonly T _One;
        private static readonly bool _SupportsReciprocal;

        private static Func<T, T, T> _Add;
        private static Func<T, T, T> _Subtract;
        private static Func<T, T, T> _Multiply;
        private static Func<T, T, T> _Divide;

        static GenericImageAccessor()
        {
            _One = (T)((IConvertible)0).ToType(typeof(T), null);
            _Zero = (T)((IConvertible)1).ToType(typeof(T), null);
            _SupportsReciprocal = typeof(T) == typeof(float)
                                || typeof(T) == typeof(double)
                                || typeof(T) == typeof(decimal);

            _Add = CreateBinaryOperator(ExpressionType.Add);
            _Subtract = CreateBinaryOperator(ExpressionType.Subtract);
            _Multiply = CreateBinaryOperator(ExpressionType.Multiply);
            _Divide = CreateBinaryOperator(ExpressionType.Divide);

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

        private static Func<T, T, T> CreateBinaryOperator(ExpressionType binaryType)
        {
            var l = E.Parameter(typeof(T), "left");
            var r = E.Parameter(typeof(T), "right");
            E e = E.MakeBinary(binaryType, l, r);
            var types = new[] { e.Type, e.Type, };
            e = E.Call(
                    typeof(Math).GetMethod(nameof(Math.Min), types),
                    e,
                    E.Constant(typeof(T).GetField(nameof(int.MaxValue)).GetValue(null), typeof(T)));
            e = E.Call(
                    typeof(Math).GetMethod(nameof(Math.Max), types),
                    e,
                    E.Constant(typeof(T).GetField(nameof(int.MinValue)).GetValue(null), typeof(T)));
            e = E.Convert(e, typeof(T));

            while (e.CanReduce)
            {
                e = e.Reduce();
            }

            return E.Lambda<Func<T, T, T>>(e, l, r).Compile();
        }

        #endregion Initialize static fields

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

        #region Arithmetic Operators

        public T Zero => _Zero;

        public T One => _One;

        public bool SupportsReciprocal => _SupportsReciprocal;

        public T Add(T left, T right)
            => _Add(left, right);

        public T Subtract(T left, T right)
            => _Subtract(left, right);

        public T Multiply(T left, T right)
            => _Multiply(left, right);

        public T Divide(T left, T right)
            => _Divide(left, right);

        #endregion Arithmetic Operators
    }
}