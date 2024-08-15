using System;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	// Token: 0x0200002A RID: 42
	public class TSimpleServer : TServer
	{
		// Token: 0x0600018B RID: 395 RVA: 0x00005D70 File Offset: 0x00003F70
		public TSimpleServer(TProcessor processor, TServerTransport serverTransport)
			: base(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00005DB8 File Offset: 0x00003FB8
		public TSimpleServer(TProcessor processor, TServerTransport serverTransport, TServer.LogDelegate logDel)
			: base(processor, serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), logDel)
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005DD8 File Offset: 0x00003FD8
		public TSimpleServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory)
			: base(processor, serverTransport, transportFactory, transportFactory, new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005E18 File Offset: 0x00004018
		public TSimpleServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: base(processor, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005E50 File Offset: 0x00004050
		public TSimpleServer(TProcessorFactory processorFactory, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: base(processorFactory, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005E88 File Offset: 0x00004088
		public override void Serve()
		{
			try
			{
				this.serverTransport.Listen();
			}
			catch (TTransportException ex)
			{
				base.logDelegate(ex.ToString());
				return;
			}
			if (this.serverEventHandler != null)
			{
				this.serverEventHandler.preServe();
			}
			while (!this.stop)
			{
				TProtocol tprotocol = null;
				TProtocol tprotocol2 = null;
				object obj = null;
				try
				{
					TTransport ttransport2;
					TTransport ttransport = (ttransport2 = this.serverTransport.Accept());
					try
					{
						TProcessor processor = this.processorFactory.GetProcessor(ttransport, null);
						if (ttransport != null)
						{
							TTransport transport;
							TTransport ttransport3 = (transport = this.inputTransportFactory.GetTransport(ttransport));
							try
							{
								TTransport transport2;
								TTransport ttransport4 = (transport2 = this.outputTransportFactory.GetTransport(ttransport));
								try
								{
									tprotocol = this.inputProtocolFactory.GetProtocol(ttransport3);
									tprotocol2 = this.outputProtocolFactory.GetProtocol(ttransport4);
									if (this.serverEventHandler != null)
									{
										obj = this.serverEventHandler.createContext(tprotocol, tprotocol2);
									}
									while (!this.stop)
									{
										if (!ttransport3.Peek())
										{
											break;
										}
										if (this.serverEventHandler != null)
										{
											this.serverEventHandler.processContext(obj, ttransport3);
										}
										if (!processor.Process(tprotocol, tprotocol2))
										{
											break;
										}
									}
								}
								finally
								{
									if (transport2 != null)
									{
										((IDisposable)transport2).Dispose();
									}
								}
							}
							finally
							{
								if (transport != null)
								{
									((IDisposable)transport).Dispose();
								}
							}
						}
					}
					finally
					{
						if (ttransport2 != null)
						{
							((IDisposable)ttransport2).Dispose();
						}
					}
				}
				catch (TTransportException ex2)
				{
					if (!this.stop || ex2.Type != TTransportException.ExceptionType.Interrupted)
					{
						base.logDelegate(ex2.ToString());
					}
				}
				catch (Exception ex3)
				{
					base.logDelegate(ex3.ToString());
				}
				if (this.serverEventHandler != null)
				{
					this.serverEventHandler.deleteContext(obj, tprotocol, tprotocol2);
				}
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000060FC File Offset: 0x000042FC
		public override void Stop()
		{
			this.stop = true;
			this.serverTransport.Close();
		}

		// Token: 0x040000A2 RID: 162
		private bool stop;
	}
}
