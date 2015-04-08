namespace Utility.Security
{
    public class MD5
    {
        const int S11 = 7;
        const int S12 = 12;
        const int S13 = 17;
        const int S14 = 22;

        const int S21 = 5;
        const int S22 = 9;
        const int S23 = 14;
        const int S24 = 20;

        const int S31 = 4;
        const int S32 = 11;
        const int S33 = 16;
        const int S34 = 23;

        const int S41 = 6;
        const int S42 = 10;
        const int S43 = 15;
        const int S44 = 21;

        static readonly sbyte[] PADDING = {
            -128, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
            0, 0, 0, 0 };

        private long[] state;
        private long[] count;
        private sbyte[] buffer;
        private sbyte[] digest;

        private static MD5 instance = new MD5();

        private MD5()
        {
            state = new long[4];
            count = new long[2];
            buffer = new sbyte[64];
            digest = new sbyte[16];
        }

        public static MD5 getInstance()
        {
            return instance;
        }

        //If you want 32 bits MD5 password, change the code from "for(int i = 0; i<8; i++)" to "for(int i = 0; i < 16; i++)"
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public string getMD516(string str)
        {
            //类型转换
            byte[] myByte = System.Text.Encoding.ASCII.GetBytes(str);

            sbyte[] mySByte = new sbyte[myByte.Length];

            for (int i = 0; i < myByte.Length; i++)
            {
                if (myByte[i] > 127)
                    mySByte[i] = (sbyte)(myByte[i] - 256);
                else
                    mySByte[i] = (sbyte)myByte[i];
            }

            md5Init();
            md5Update(mySByte, str.Length);
            md5Final();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < 8; i++)
            {

                sb.Append(byteHEX(digest[i]));
            }
            return sb.ToString();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public string getMD532(string str)
        {
            //类型转换
            byte[] myByte = System.Text.Encoding.ASCII.GetBytes(str);

            sbyte[] mySByte = new sbyte[myByte.Length];

            for (int i = 0; i < myByte.Length; i++)
            {
                if (myByte[i] > 127)
                    mySByte[i] = (sbyte)(myByte[i] - 256);
                else
                    mySByte[i] = (sbyte)myByte[i];
            }

            md5Init();
            md5Update(mySByte, str.Length);
            md5Final();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < 16; i++)
            {

                sb.Append(byteHEX(digest[i]));
            }
            return sb.ToString();
        }

        private void md5Init()
        {
            count[0] = 0L;
            count[1] = 0L;
            state[0] = 0x67452301L;
            state[1] = 0xefcdab89L;
            state[2] = 0x98badcfeL;
            state[3] = 0x10325476L;
        }

        private long F(long l, long l1, long l2)
        {
            return l & l1 | ~l & l2;
        }

        private long G(long l, long l1, long l2)
        {
            return l & l2 | l1 & ~l2;
        }

        private long H(long l, long l1, long l2)
        {
            return l ^ l1 ^ l2;
        }

        private long I(long l, long l1, long l2)
        {
            return l1 ^ (l | ~l2);
        }

        private long FF(long l, long l1, long l2, long l3, long l4, long l5, long l6)
        {
            l += F(l1, l2, l3) + l4 + l6;
            l = (uint)l << (int)l5 | (uint)l >> (int)(32L - l5);
            l += l1;
            return l;
        }

        private long GG(long l, long l1, long l2, long l3, long l4, long l5, long l6)
        {
            l += G(l1, l2, l3) + l4 + l6;
            l = (uint)l << (int)l5 | (uint)l >> (int)(32L - l5);
            l += l1;
            return l;
        }

        private long HH(long l, long l1, long l2, long l3, long l4, long l5, long l6)
        {
            l += H(l1, l2, l3) + l4 + l6;
            l = (uint)l << (int)l5 | (uint)l >> (int)(32L - l5);
            l += l1;
            return l;
        }

        private long II(long l, long l1, long l2, long l3, long l4, long l5, long l6)
        {
            l += I(l1, l2, l3) + l4 + l6;
            l = (uint)l << (int)l5 | (uint)l >> (int)(32L - l5);
            l += l1;
            return l;
        }

        private void md5Update(sbyte[] abyte0, int i)
        {
            sbyte[] abyte1 = new sbyte[64];
            int k = (int)((uint)count[0] >> 3) & 0x3f;
            if ((count[0] += i << 3) < (long)(i << 3))
                count[1]++;
            count[1] += (uint)i >> 29;
            int l = 64 - k;
            int j;
            if (i >= l)
            {
                md5Memcpy(buffer, abyte0, k, 0, l);
                md5Transform(buffer);
                for (j = l; j + 63 < i; j += 64)
                {
                    md5Memcpy(abyte1, abyte0, 0, j, 64);
                    md5Transform(abyte1);
                }

                k = 0;
            }
            else
            {
                j = 0;
            }
            md5Memcpy(buffer, abyte0, k, j, i - j);
        }

        private void md5Final()
        {
            sbyte[] abyte0 = new sbyte[8];
            Encode(abyte0, count, 8);
            int i = (int)((uint)count[0] >> 3) & 0x3f;
            int j = i >= 56 ? 120 - i : 56 - i;
            md5Update(PADDING, j);
            md5Update(abyte0, 8);
            Encode(digest, state, 16);
        }

        private void md5Memcpy(sbyte[] abyte0, sbyte[] abyte1, int i, int j, int k)
        {
            for (int l = 0; l < k; l++)
                abyte0[i + l] = abyte1[j + l];

        }

        private void md5Transform(sbyte[] abyte0)
        {
            long l = state[0];
            long l1 = state[1];
            long l2 = state[2];
            long l3 = state[3];
            long[] al = new long[16];
            Decode(al, abyte0, 64);
            l = FF(l, l1, l2, l3, al[0], S11, 0xd76aa478L);
            l3 = FF(l3, l, l1, l2, al[1], S12, 0xe8c7b756L);
            l2 = FF(l2, l3, l, l1, al[2], S13, 0x242070dbL);
            l1 = FF(l1, l2, l3, l, al[3], S14, 0xc1bdceeeL);
            l = FF(l, l1, l2, l3, al[4], S11, 0xf57c0fafL);
            l3 = FF(l3, l, l1, l2, al[5], S12, 0x4787c62aL);
            l2 = FF(l2, l3, l, l1, al[6], S13, 0xa8304613L);
            l1 = FF(l1, l2, l3, l, al[7], S14, 0xfd469501L);
            l = FF(l, l1, l2, l3, al[8], S11, 0x698098d8L);
            l3 = FF(l3, l, l1, l2, al[9], S12, 0x8b44f7afL);
            l2 = FF(l2, l3, l, l1, al[10], S13, 0xffff5bb1L);
            l1 = FF(l1, l2, l3, l, al[11], S14, 0x895cd7beL);
            l = FF(l, l1, l2, l3, al[12], S11, 0x6b901122L);
            l3 = FF(l3, l, l1, l2, al[13], S12, 0xfd987193L);
            l2 = FF(l2, l3, l, l1, al[14], S13, 0xa679438eL);
            l1 = FF(l1, l2, l3, l, al[15], S14, 0x49b40821L);
            l = GG(l, l1, l2, l3, al[1], S21, 0xf61e2562L);
            l3 = GG(l3, l, l1, l2, al[6], S22, 0xc040b340L);
            l2 = GG(l2, l3, l, l1, al[11], S23, 0x265e5a51L);
            l1 = GG(l1, l2, l3, l, al[0], S24, 0xe9b6c7aaL);
            l = GG(l, l1, l2, l3, al[5], S21, 0xd62f105dL);
            l3 = GG(l3, l, l1, l2, al[10], S22, 0x2441453L);
            l2 = GG(l2, l3, l, l1, al[15], S23, 0xd8a1e681L);
            l1 = GG(l1, l2, l3, l, al[4], S24, 0xe7d3fbc8L);
            l = GG(l, l1, l2, l3, al[9], S21, 0x21e1cde6L);
            l3 = GG(l3, l, l1, l2, al[14], S22, 0xc33707d6L);
            l2 = GG(l2, l3, l, l1, al[3], S23, 0xf4d50d87L);
            l1 = GG(l1, l2, l3, l, al[8], S24, 0x455a14edL);
            l = GG(l, l1, l2, l3, al[13], S21, 0xa9e3e905L);
            l3 = GG(l3, l, l1, l2, al[2], S22, 0xfcefa3f8L);
            l2 = GG(l2, l3, l, l1, al[7], S23, 0x676f02d9L);
            l1 = GG(l1, l2, l3, l, al[12], S24, 0x8d2a4c8aL);
            l = HH(l, l1, l2, l3, al[5], S31, 0xfffa3942L);
            l3 = HH(l3, l, l1, l2, al[8], S32, 0x8771f681L);
            l2 = HH(l2, l3, l, l1, al[11], S33, 0x6d9d6122L);
            l1 = HH(l1, l2, l3, l, al[14], S34, 0xfde5380cL);
            l = HH(l, l1, l2, l3, al[1], S31, 0xa4beea44L);
            l3 = HH(l3, l, l1, l2, al[4], S32, 0x4bdecfa9L);
            l2 = HH(l2, l3, l, l1, al[7], S33, 0xf6bb4b60L);
            l1 = HH(l1, l2, l3, l, al[10], S34, 0xbebfbc70L);
            l = HH(l, l1, l2, l3, al[13], S31, 0x289b7ec6L);
            l3 = HH(l3, l, l1, l2, al[0], S32, 0xeaa127faL);
            l2 = HH(l2, l3, l, l1, al[3], S33, 0xd4ef3085L);
            l1 = HH(l1, l2, l3, l, al[6], S34, 0x4881d05L);
            l = HH(l, l1, l2, l3, al[9], S31, 0xd9d4d039L);
            l3 = HH(l3, l, l1, l2, al[12], S32, 0xe6db99e5L);
            l2 = HH(l2, l3, l, l1, al[15], S33, 0x1fa27cf8L);
            l1 = HH(l1, l2, l3, l, al[2], S34, 0xc4ac5665L);
            l = II(l, l1, l2, l3, al[0], S41, 0xf4292244L);
            l3 = II(l3, l, l1, l2, al[7], S42, 0x432aff97L);
            l2 = II(l2, l3, l, l1, al[14], S43, 0xab9423a7L);
            l1 = II(l1, l2, l3, l, al[5], S44, 0xfc93a039L);
            l = II(l, l1, l2, l3, al[12], S41, 0x655b59c3L);
            l3 = II(l3, l, l1, l2, al[3], S42, 0x8f0ccc92L);
            l2 = II(l2, l3, l, l1, al[10], S43, 0xffeff47dL);
            l1 = II(l1, l2, l3, l, al[1], S44, 0x85845dd1L);
            l = II(l, l1, l2, l3, al[8], S41, 0x6fa87e4fL);
            l3 = II(l3, l, l1, l2, al[15], S42, 0xfe2ce6e0L);
            l2 = II(l2, l3, l, l1, al[6], S43, 0xa3014314L);
            l1 = II(l1, l2, l3, l, al[13], S44, 0x4e0811a1L);
            l = II(l, l1, l2, l3, al[4], S41, 0xf7537e82L);
            l3 = II(l3, l, l1, l2, al[11], S42, 0xbd3af235L);
            l2 = II(l2, l3, l, l1, al[2], S43, 0x2ad7d2bbL);
            l1 = II(l1, l2, l3, l, al[9], S44, 0xeb86d391L);
            state[0] += l;
            state[1] += l1;
            state[2] += l2;
            state[3] += l3;
        }

        private void Encode(sbyte[] abyte0, long[] al, int i)
        {
            int j = 0;
            for (int k = 0; k < i; k += 4)
            {
                abyte0[k] = (sbyte)(int)(al[j] & 255L);
                abyte0[k + 1] = (sbyte)(uint)(al[j] >> 8 & 255L);
                abyte0[k + 2] = (sbyte)(uint)(al[j] >> 16 & 255L);
                abyte0[k + 3] = (sbyte)(uint)(al[j] >> 24 & 255L);
                j++;
            }

        }

        private void Decode(long[] al, sbyte[] abyte0, int i)
        {
            int j = 0;
            for (int k = 0; k < i; k += 4)
            {
                al[j] = b2iu(abyte0[k]) | b2iu(abyte0[k + 1]) << 8 | b2iu(abyte0[k + 2]) << 16 | b2iu(abyte0[k + 3]) << 24;
                j++;
            }

        }

        public static long b2iu(sbyte byte0)
        {
            return byte0 >= 0 ? byte0 : byte0 & 0xff;
        }

        public static string byteHEX(sbyte byte0)
        {
            char[] ac = {
                '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 
                'A', 'B', 'C', 'D', 'E', 'F'
            };
            char[] ac1 = new char[2];
            ac1[0] = ac[byte0 >> 4 & 0xf];
            ac1[1] = ac[byte0 & 0xf];
            string s = new string(ac1);
            return s;
        }

        //public static void Main(String[] args)
        //{
        //    string str =  HYH.Utility.Security.MD5.getInstance().getMD516("1269846204269;1095115;1000;QLC;2010031;1268365200000;1271043900000;1270193400000;5DP2P7KEFP3E7RH8");
        //    System.Console.WriteLine(str);
        //}
    }
}
