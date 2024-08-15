using System;

namespace Thrift.Transport
{
	// Token: 0x0200003D RID: 61
	public abstract class TServerTransport
	{
		// Token: 0x06000206 RID: 518
		public abstract void Listen();

		// Token: 0x06000207 RID: 519
		public abstract void Close();

		// Token: 0x06000208 RID: 520
		protected abstract TTransport AcceptImpl();

		// Token: 0x06000209 RID: 521 RVA: 0x00007EC8 File Offset: 0x000060C8
		public TTransport Accept()
		{
			TTransport ttransport = this.AcceptImpl();
			if (ttransport == null)
			{
				throw new TTransportException("accept() may not return NULL");
			}
			return ttransport;
		}
	}
}
