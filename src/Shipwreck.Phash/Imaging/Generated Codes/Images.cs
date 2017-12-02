using System;
namespace Shipwreck.Phash.Imaging
{
	using EX = ImageExtensions;
	partial class FloatImage : IArrayImage<System.Single>, IImageOperatorProvider<System.Single>
	{
        private readonly int _Width;
        private readonly int _Height;
        private readonly System.Single[] _Data;
		
        public FloatImage(int width, int height)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Single[width * height];
        }

        public FloatImage(int width, int height, System.Single value)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Single[width * height];
            for (var i = 0; i < _Data.Length; i++)
            {
                _Data[i] = value;
            }
        }

        public FloatImage(int width, int height, System.Single[] data)
        {
            _Width = width;
            _Height = height;
            _Data = data;
        }
		
        public int Width => _Width;
        public int Height => _Height;
		public System.Single[] Array => _Data;

        public System.Single this[int x, int y]
        {
            get
            {
                var i = x + y * _Width;
                return _Data[i];
            }
            set
            {
                var i = x + y * _Width;
                _Data[i] = value;
            }
        }
		
		public System.Single Max()
		{
			System.Single r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Max(_Data[i], r);
			}
			return r;
		}
		public System.Single Min()
		{
			System.Single r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Min(_Data[i], r);
			}
			return r;
		}
		public System.Single Sum()
		{
			System.Single r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r += _Data[i];
			}
			return r;
		}

		IImageOperator<System.Single> IImageOperatorProvider<System.Single>.GetOperator()
			=> new ImageAccessorOperator<System.Single, FloatImageAccessor>(new FloatImageAccessor(this));
	}

	internal struct FloatImageAccessor : IImageAccessor<System.Single>, IArrayImage<System.Single>, IImageOperatorProvider<System.Single>
	{
		public FloatImageAccessor(FloatImage image)
		{
			Image = image;
		}
		
        public IImage<System.Single> Image { get; }
        System.Single[] IArrayImage<System.Single>.Array => ((IArrayImage<System.Single>)Image).Array;

        public int Width => Image.Width;
        public int Height => Image.Height;

        public System.Single this[int x, int y]
        {
            get => Image[x, y];
            set => Image[x, y] = value;
        }
		
        public IImageAccessor<System.Single> CreateNew(int width, int height)
			=> new FloatImageAccessor(new FloatImage(width, height));

		IImageOperator<System.Single> IImageOperatorProvider<System.Single>.GetOperator()
			=> new ImageAccessorOperator<System.Single, FloatImageAccessor>(this);

		#region Arithmetic Operators

        public System.Single Zero => 0;

        public System.Single One => 1;

        public bool SupportsReciprocal => true;

        public System.Single Add(System.Single left, System.Single right)
			=> (System.Single)Math.Max(System.Single.MinValue, Math.Min(left + right, System.Single.MaxValue));

        public System.Single Subtract(System.Single left, System.Single right)
			=> (System.Single)Math.Max(System.Single.MinValue, Math.Min(left - right, System.Single.MaxValue));

        public System.Single Multiply(System.Single left, System.Single right)
			=> (System.Single)Math.Max(System.Single.MinValue, Math.Min(left * right, System.Single.MaxValue));

        public System.Single Divide(System.Single left, System.Single right)
			=> (System.Single)Math.Max(System.Single.MinValue, Math.Min(left / right, System.Single.MaxValue));

        #endregion Arithmetic Operators
	}
	partial class ByteImage : IArrayImage<System.Byte>, IImageOperatorProvider<System.Byte>
	{
        private readonly int _Width;
        private readonly int _Height;
        private readonly System.Byte[] _Data;
		
        public ByteImage(int width, int height)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Byte[width * height];
        }

        public ByteImage(int width, int height, System.Byte value)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Byte[width * height];
            for (var i = 0; i < _Data.Length; i++)
            {
                _Data[i] = value;
            }
        }

        public ByteImage(int width, int height, System.Byte[] data)
        {
            _Width = width;
            _Height = height;
            _Data = data;
        }
		
        public int Width => _Width;
        public int Height => _Height;
		public System.Byte[] Array => _Data;

        public System.Byte this[int x, int y]
        {
            get
            {
                var i = x + y * _Width;
                return _Data[i];
            }
            set
            {
                var i = x + y * _Width;
                _Data[i] = value;
            }
        }
		
		public System.Int32 Max()
		{
			System.Int32 r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Max(_Data[i], r);
			}
			return r;
		}
		public System.Int32 Min()
		{
			System.Int32 r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Min(_Data[i], r);
			}
			return r;
		}
		public System.Int32 Sum()
		{
			System.Int32 r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r += _Data[i];
			}
			return r;
		}

		IImageOperator<System.Byte> IImageOperatorProvider<System.Byte>.GetOperator()
			=> new ImageAccessorOperator<System.Byte, ByteImageAccessor>(new ByteImageAccessor(this));
	}

	internal struct ByteImageAccessor : IImageAccessor<System.Byte>, IArrayImage<System.Byte>, IImageOperatorProvider<System.Byte>
	{
		public ByteImageAccessor(ByteImage image)
		{
			Image = image;
		}
		
        public IImage<System.Byte> Image { get; }
        System.Byte[] IArrayImage<System.Byte>.Array => ((IArrayImage<System.Byte>)Image).Array;

        public int Width => Image.Width;
        public int Height => Image.Height;

        public System.Byte this[int x, int y]
        {
            get => Image[x, y];
            set => Image[x, y] = value;
        }
		
        public IImageAccessor<System.Byte> CreateNew(int width, int height)
			=> new ByteImageAccessor(new ByteImage(width, height));

		IImageOperator<System.Byte> IImageOperatorProvider<System.Byte>.GetOperator()
			=> new ImageAccessorOperator<System.Byte, ByteImageAccessor>(this);

		#region Arithmetic Operators

        public System.Byte Zero => 0;

        public System.Byte One => 1;

        public bool SupportsReciprocal => false;

        public System.Byte Add(System.Byte left, System.Byte right)
			=> (System.Byte)Math.Max(System.Byte.MinValue, Math.Min(left + right, System.Byte.MaxValue));

        public System.Byte Subtract(System.Byte left, System.Byte right)
			=> (System.Byte)Math.Max(System.Byte.MinValue, Math.Min(left - right, System.Byte.MaxValue));

        public System.Byte Multiply(System.Byte left, System.Byte right)
			=> (System.Byte)Math.Max(System.Byte.MinValue, Math.Min(left * right, System.Byte.MaxValue));

        public System.Byte Divide(System.Byte left, System.Byte right)
			=> (System.Byte)Math.Max(System.Byte.MinValue, Math.Min(left / right, System.Byte.MaxValue));

        #endregion Arithmetic Operators
	}
}
