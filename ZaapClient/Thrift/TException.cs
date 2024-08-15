using System;

namespace Thrift
{
	// Token: 0x0200002C RID: 44
	public class TException : Exception
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00006524 File Offset: 0x00004724
		public TException()
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000652C File Offset: 0x0000472C
		public TException(string message)
			: base(message)
		{
		}
	}
}
