using System;
using Thrift.Server;
using Thrift.Transport;

namespace Thrift
{
	// Token: 0x02000024 RID: 36
	public class TPrototypeProcessorFactory<P, H> : TProcessorFactory where P : TProcessor
	{
		// Token: 0x06000167 RID: 359 RVA: 0x000054D8 File Offset: 0x000036D8
		public TPrototypeProcessorFactory()
		{
			this.handlerArgs = new object[0];
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000054EC File Offset: 0x000036EC
		public TPrototypeProcessorFactory(params object[] handlerArgs)
		{
			this.handlerArgs = handlerArgs;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000054FC File Offset: 0x000036FC
		public TProcessor GetProcessor(TTransport trans, TServer server = null)
		{
			H h = (H)((object)Activator.CreateInstance(typeof(H), this.handlerArgs));
			TControllingHandler tcontrollingHandler = h as TControllingHandler;
			if (tcontrollingHandler != null)
			{
				tcontrollingHandler.server = server;
			}
			return Activator.CreateInstance(typeof(P), new object[] { h }) as TProcessor;
		}

		// Token: 0x04000088 RID: 136
		private object[] handlerArgs;
	}
}
