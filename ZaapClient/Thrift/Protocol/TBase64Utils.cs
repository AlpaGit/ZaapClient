using System;

namespace Thrift.Protocol
{
	// Token: 0x02000008 RID: 8
	internal static class TBase64Utils
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002250 File Offset: 0x00000450
		internal static void encode(byte[] src, int srcOff, int len, byte[] dst, int dstOff)
		{
			dst[dstOff] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(src[srcOff] >> 2) & 63];
			if (len == 3)
			{
				dst[dstOff + 1] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(((int)src[srcOff] << 4) & 48) | ((src[srcOff + 1] >> 4) & 15)];
				dst[dstOff + 2] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(((int)src[srcOff + 1] << 2) & 60) | ((src[srcOff + 2] >> 6) & 3)];
				dst[dstOff + 3] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(int)(src[srcOff + 2] & 63)];
			}
			else if (len == 2)
			{
				dst[dstOff + 1] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[(((int)src[srcOff] << 4) & 48) | ((src[srcOff + 1] >> 4) & 15)];
				dst[dstOff + 2] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[((int)src[srcOff + 1] << 2) & 60];
			}
			else
			{
				dst[dstOff + 1] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[((int)src[srcOff] << 4) & 48];
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002348 File Offset: 0x00000548
		internal static void decode(byte[] src, int srcOff, int len, byte[] dst, int dstOff)
		{
			dst[dstOff] = (byte)((TBase64Utils.DECODE_TABLE[(int)(src[srcOff] & byte.MaxValue)] << 2) | (TBase64Utils.DECODE_TABLE[(int)(src[srcOff + 1] & byte.MaxValue)] >> 4));
			if (len > 2)
			{
				dst[dstOff + 1] = (byte)(((TBase64Utils.DECODE_TABLE[(int)(src[srcOff + 1] & byte.MaxValue)] << 4) & 240) | (TBase64Utils.DECODE_TABLE[(int)(src[srcOff + 2] & byte.MaxValue)] >> 2));
				if (len > 3)
				{
					dst[dstOff + 2] = (byte)(((TBase64Utils.DECODE_TABLE[(int)(src[srcOff + 2] & byte.MaxValue)] << 6) & 192) | TBase64Utils.DECODE_TABLE[(int)(src[srcOff + 3] & byte.MaxValue)]);
				}
			}
		}

		// Token: 0x04000002 RID: 2
		internal const string ENCODE_TABLE = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

		// Token: 0x04000003 RID: 3
		private static int[] DECODE_TABLE = new int[]
		{
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, 62, -1, -1, -1, 63, 52, 53,
			54, 55, 56, 57, 58, 59, 60, 61, -1, -1,
			-1, -1, -1, -1, -1, 0, 1, 2, 3, 4,
			5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
			15, 16, 17, 18, 19, 20, 21, 22, 23, 24,
			25, -1, -1, -1, -1, -1, -1, 26, 27, 28,
			29, 30, 31, 32, 33, 34, 35, 36, 37, 38,
			39, 40, 41, 42, 43, 44, 45, 46, 47, 48,
			49, 50, 51, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
			-1, -1, -1, -1, -1, -1
		};
	}
}
