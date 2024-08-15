using System;

namespace Thrift.Protocol
{
	// Token: 0x0200001E RID: 30
	public class TProtocolException : TException
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000524C File Offset: 0x0000344C
		public TProtocolException()
		{
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00005254 File Offset: 0x00003454
		public TProtocolException(int type)
		{
			this.type_ = type;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005264 File Offset: 0x00003464
		public TProtocolException(int type, string message)
			: base(message)
		{
			this.type_ = type;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00005274 File Offset: 0x00003474
		public TProtocolException(string message)
			: base(message)
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00005280 File Offset: 0x00003480
		public int getType()
		{
			return this.type_;
		}

		// Token: 0x0400006F RID: 111
		public const int UNKNOWN = 0;

		// Token: 0x04000070 RID: 112
		public const int INVALID_DATA = 1;

		// Token: 0x04000071 RID: 113
		public const int NEGATIVE_SIZE = 2;

		// Token: 0x04000072 RID: 114
		public const int SIZE_LIMIT = 3;

		// Token: 0x04000073 RID: 115
		public const int BAD_VERSION = 4;

		// Token: 0x04000074 RID: 116
		public const int NOT_IMPLEMENTED = 5;

		// Token: 0x04000075 RID: 117
		public const int DEPTH_LIMIT = 6;

		// Token: 0x04000076 RID: 118
		protected int type_;
	}
}
