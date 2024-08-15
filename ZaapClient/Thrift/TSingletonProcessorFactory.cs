using System;
using Thrift.Server;
using Thrift.Transport;

namespace Thrift
{
	// Token: 0x02000025 RID: 37
	public class TSingletonProcessorFactory : TProcessorFactory
	{
		// Token: 0x0600016A RID: 362 RVA: 0x00005560 File Offset: 0x00003760
		public TSingletonProcessorFactory(TProcessor processor)
		{
			this.processor_ = processor;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005570 File Offset: 0x00003770
		public TProcessor GetProcessor(TTransport trans, TServer server = null)
		{
			return this.processor_;
		}

		// Token: 0x04000089 RID: 137
		private readonly TProcessor processor_;
	}
}
