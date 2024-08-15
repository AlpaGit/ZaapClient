using System;
using Thrift.Transport;

namespace Thrift.Protocol
{
	// Token: 0x0200001F RID: 31
	public interface TProtocolFactory
	{
		// Token: 0x0600015C RID: 348
		TProtocol GetProtocol(TTransport trans);
	}
}
