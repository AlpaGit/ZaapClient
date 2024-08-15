using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	// Token: 0x02000027 RID: 39
	public abstract class TServer
	{
		// Token: 0x06000176 RID: 374 RVA: 0x00005B44 File Offset: 0x00003D44
		public TServer(TProcessor processor, TServerTransport serverTransport)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005B8C File Offset: 0x00003D8C
		public TServer(TProcessor processor, TServerTransport serverTransport, TServer.LogDelegate logDelegate)
			: this(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), logDelegate)
		{
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005BAC File Offset: 0x00003DAC
		public TServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory)
			: this(processor, serverTransport, transportFactory, transportFactory, new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005BEC File Offset: 0x00003DEC
		public TServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(processor, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00005C24 File Offset: 0x00003E24
		public TServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory inputTransportFactory, TTransportFactory outputTransportFactory, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory, TServer.LogDelegate logDelegate)
		{
			this.processorFactory = new TSingletonProcessorFactory(processor);
			this.serverTransport = serverTransport;
			this.inputTransportFactory = inputTransportFactory;
			this.outputTransportFactory = outputTransportFactory;
			this.inputProtocolFactory = inputProtocolFactory;
			this.outputProtocolFactory = outputProtocolFactory;
			TServer.LogDelegate logDelegate2;
			if (logDelegate != null)
			{
				logDelegate2 = logDelegate;
			}
			else
			{
				logDelegate2 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
			}
			this.logDelegate = logDelegate2;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00005C9C File Offset: 0x00003E9C
		public TServer(TProcessorFactory processorFactory, TServerTransport serverTransport, TTransportFactory inputTransportFactory, TTransportFactory outputTransportFactory, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory, TServer.LogDelegate logDelegate)
		{
			this.processorFactory = processorFactory;
			this.serverTransport = serverTransport;
			this.inputTransportFactory = inputTransportFactory;
			this.outputTransportFactory = outputTransportFactory;
			this.inputProtocolFactory = inputProtocolFactory;
			this.outputProtocolFactory = outputProtocolFactory;
			TServer.LogDelegate logDelegate2;
			if (logDelegate != null)
			{
				logDelegate2 = logDelegate;
			}
			else
			{
				logDelegate2 = new TServer.LogDelegate(TServer.DefaultLogDelegate);
			}
			this.logDelegate = logDelegate2;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00005D10 File Offset: 0x00003F10
		public void setEventHandler(TServerEventHandler seh)
		{
			this.serverEventHandler = seh;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005D1C File Offset: 0x00003F1C
		public TServerEventHandler getEventHandler()
		{
			return this.serverEventHandler;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00005D24 File Offset: 0x00003F24
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00005D2C File Offset: 0x00003F2C
		protected TServer.LogDelegate logDelegate
		{
			get
			{
				return this._logDelegate;
			}
			set
			{
				TServer.LogDelegate logDelegate;
				if (value != null)
				{
					logDelegate = value;
				}
				else
				{
					logDelegate = new TServer.LogDelegate(TServer.DefaultLogDelegate);
				}
				this._logDelegate = logDelegate;
			}
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00005D60 File Offset: 0x00003F60
		protected static void DefaultLogDelegate(string s)
		{
			Console.Error.WriteLine(s);
		}

		// Token: 0x06000181 RID: 385
		public abstract void Serve();

		// Token: 0x06000182 RID: 386
		public abstract void Stop();

		// Token: 0x04000094 RID: 148
		protected TProcessorFactory processorFactory;

		// Token: 0x04000095 RID: 149
		protected TServerTransport serverTransport;

		// Token: 0x04000096 RID: 150
		protected TTransportFactory inputTransportFactory;

		// Token: 0x04000097 RID: 151
		protected TTransportFactory outputTransportFactory;

		// Token: 0x04000098 RID: 152
		protected TProtocolFactory inputProtocolFactory;

		// Token: 0x04000099 RID: 153
		protected TProtocolFactory outputProtocolFactory;

		// Token: 0x0400009A RID: 154
		protected TServerEventHandler serverEventHandler;

		// Token: 0x0400009B RID: 155
		private TServer.LogDelegate _logDelegate;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x06000184 RID: 388
		public delegate void LogDelegate(string str);
	}
}
