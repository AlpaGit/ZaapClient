using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	// Token: 0x02000029 RID: 41
	public interface TServerEventHandler
	{
		// Token: 0x06000187 RID: 391
		void preServe();

		// Token: 0x06000188 RID: 392
		object createContext(TProtocol input, TProtocol output);

		// Token: 0x06000189 RID: 393
		void deleteContext(object serverContext, TProtocol input, TProtocol output);

		// Token: 0x0600018A RID: 394
		void processContext(object serverContext, TTransport transport);
	}
}
