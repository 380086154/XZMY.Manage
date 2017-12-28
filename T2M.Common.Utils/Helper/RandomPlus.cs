using System;
using System.Security.Cryptography;

namespace T2M.Common.Utils.Helper
{
    /// <summary>
    /// 表示了线程安全的虚拟随机数生成器。
    /// </summary>
    public static class RandomPlus
    {
        private static Random _seed = new Random();
        private static RNGCryptoServiceProvider _rngSeed = new RNGCryptoServiceProvider();
        [ThreadStatic]
        private static Random _initRand;

        /// <summary>
        /// 生成范围0到Int32.MaxValue的随机数字。
        /// </summary>
        /// <param name="enableRNGCrypto">是否使用加密随机数生成器<see cref="RNGCryptoServiceProvider"/>生成随机数。</param>
        /// <returns>范围0到整形最大值的随机数。</returns>
        public static Int32 Next(Boolean enableRNGCrypto)
        {
            return Next(0, Int32.MaxValue, enableRNGCrypto);
        }

        /// <summary>
        /// 生成范围0到Int32.MaxValue的随机数字。
        /// </summary>
        /// <param name="maxValue">随机数范围的最大值。</param>
        /// <param name="enableRNGCrypto">是否使用加密随机数生成器<see cref="RNGCryptoServiceProvider"/>生成随机数。</param>
        /// <returns>范围0到指定最大值的随机数。</returns>
        public static Int32 Next(Int32 maxValue, Boolean enableRNGCrypto)
        {
            return Next(0, maxValue, enableRNGCrypto);
        }

        /// <summary>
        /// 是否使用加密随机数生成器（RNG）生成随机数字。
        /// </summary>
        /// <param name="minValue">随机数范围的最小值。</param>
        /// <param name="maxValue">随机数范围的最大值。</param>
        /// <param name="enableRNGCrypto">是否使用加密随机数生成器<see cref="RNGCryptoServiceProvider"/>生成随机数。</param>
        /// <returns>指定范围的随机数，该范围不得超过整形类型的有效范围。</returns>
        public static Int32 Next(Int32 minValue, Int32 maxValue, Boolean enableRNGCrypto)
        {
            Random rand = _initRand;

            if (rand == null)
            {
                if (!enableRNGCrypto)
                {
                    Int32 seed = 0;

                    lock (_seed)
                        seed = _seed.Next();

                    _initRand = rand = new Random(seed);
                }
                else
                {
                    Int32 seed = 0;
                    byte[] buffer = new byte[6];
                    _rngSeed.GetNonZeroBytes(buffer);
                    seed = BitConverter.ToInt32(buffer, 0);
                    _initRand = rand = new Random(seed);

                }
            }

            return rand.Next(minValue, maxValue);
        }
    }
}
