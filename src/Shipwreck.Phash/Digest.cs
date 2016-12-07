using Shipwreck.Phash.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash
{
    /// <summary>
    /// Digest info
    /// </summary>
    public class Digest
    {
        /// <summary>
        /// hash id
        /// </summary>
        public char[] id;

        /// <summary>
        /// the digest integer coefficient array
        /// </summary>
        public byte[] coeffs;

        public override string ToString()
        {
            var sb = new StringBuilder(coeffs.Length * 2 + 2);
            sb.Append("0x");
            foreach (var b in coeffs)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}