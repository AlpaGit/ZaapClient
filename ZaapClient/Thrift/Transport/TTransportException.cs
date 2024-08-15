using System;

namespace Thrift.Transport
{
	// Token: 0x02000044 RID: 68
	public class TTransportException : TException
	{
		// Token: 0x06000246 RID: 582 RVA: 0x00008B74 File Offset: 0x00006D74
		public TTransportException()
		{
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00008B7C File Offset: 0x00006D7C
		public TTransportException(TTransportException.ExceptionType type)
			: this()
		{
			this.type = type;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00008B8C File Offset: 0x00006D8C
		public TTransportException(TTransportException.ExceptionType type, string message)
			: base(message)
		{
			this.type = type;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00008B9C File Offset: 0x00006D9C
		public TTransportException(string message)
			: base(message)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00008BA8 File Offset: 0x00006DA8
		public TTransportException.ExceptionType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x04000111 RID: 273
		protected TTransportException.ExceptionType type;

		// Token: 0x02000045 RID: 69
		public enum ExceptionType
		{
			// Token: 0x04000113 RID: 275
			Unknown,
			// Token: 0x04000114 RID: 276
			NotOpen,
			// Token: 0x04000115 RID: 277
			AlreadyOpen,
			// Token: 0x04000116 RID: 278
			TimedOut,
			// Token: 0x04000117 RID: 279
			EndOfFile,
			// Token: 0x04000118 RID: 280
			Interrupted
		}
	}
}
