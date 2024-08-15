using System;
using Thrift.Server;
using Thrift.Transport;

namespace Thrift
{
	// Token: 0x02000005 RID: 5
	public interface TProcessorFactory
	{
		// Token: 0x06000010 RID: 16
		TProcessor GetProcessor(TTransport trans, TServer server = null);
	}
}
