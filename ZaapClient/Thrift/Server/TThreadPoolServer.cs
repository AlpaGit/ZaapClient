using System;
using System.Threading;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	// Token: 0x0200002B RID: 43
	public class TThreadPoolServer : TServer
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00006110 File Offset: 0x00004310
		public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport)
			: this(new TSingletonProcessorFactory(processor), serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 10, 100, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006160 File Offset: 0x00004360
		public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport, TServer.LogDelegate logDelegate)
			: this(new TSingletonProcessorFactory(processor), serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 10, 100, logDelegate)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006194 File Offset: 0x00004394
		public TThreadPoolServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(new TSingletonProcessorFactory(processor), serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, 10, 100, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000061D8 File Offset: 0x000043D8
		public TThreadPoolServer(TProcessorFactory processorFactory, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(processorFactory, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, 10, 100, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006214 File Offset: 0x00004414
		public TThreadPoolServer(TProcessorFactory processorFactory, TServerTransport serverTransport, TTransportFactory inputTransportFactory, TTransportFactory outputTransportFactory, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory, int minThreadPoolThreads, int maxThreadPoolThreads, TServer.LogDelegate logDel)
			: base(processorFactory, serverTransport, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, logDel)
		{
			object typeFromHandle = typeof(TThreadPoolServer);
			lock (typeFromHandle)
			{
				if (!ThreadPool.SetMaxThreads(maxThreadPoolThreads, maxThreadPoolThreads))
				{
					throw new Exception("Error: could not SetMaxThreads in ThreadPool");
				}
				if (!ThreadPool.SetMinThreads(minThreadPoolThreads, minThreadPoolThreads))
				{
					throw new Exception("Error: could not SetMinThreads in ThreadPool");
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006294 File Offset: 0x00004494
		public override void Serve()
		{
			try
			{
				this.serverTransport.Listen();
			}
			catch (TTransportException ex)
			{
				base.logDelegate("Error, could not listen on ServerTransport: " + ex);
				return;
			}
			if (this.serverEventHandler != null)
			{
				this.serverEventHandler.preServe();
			}
			while (!this.stop)
			{
				int num = 0;
				try
				{
					TTransport ttransport = this.serverTransport.Accept();
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.Execute), ttransport);
				}
				catch (TTransportException ex2)
				{
					if (!this.stop || ex2.Type != TTransportException.ExceptionType.Interrupted)
					{
						num++;
						base.logDelegate(ex2.ToString());
					}
				}
			}
			if (this.stop)
			{
				try
				{
					this.serverTransport.Close();
				}
				catch (TTransportException ex3)
				{
					base.logDelegate("TServerTransport failed on close: " + ex3.Message);
				}
				this.stop = false;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000063C0 File Offset: 0x000045C0
		private void Execute(object threadContext)
		{
			TTransport ttransport = (TTransport)threadContext;
			TProcessor processor = this.processorFactory.GetProcessor(ttransport, this);
			TTransport ttransport2 = null;
			TTransport ttransport3 = null;
			TProtocol tprotocol = null;
			TProtocol tprotocol2 = null;
			object obj = null;
			try
			{
				ttransport2 = this.inputTransportFactory.GetTransport(ttransport);
				ttransport3 = this.outputTransportFactory.GetTransport(ttransport);
				tprotocol = this.inputProtocolFactory.GetProtocol(ttransport2);
				tprotocol2 = this.outputProtocolFactory.GetProtocol(ttransport3);
				if (this.serverEventHandler != null)
				{
					obj = this.serverEventHandler.createContext(tprotocol, tprotocol2);
				}
				while (!this.stop)
				{
					if (!ttransport2.Peek())
					{
						break;
					}
					if (this.serverEventHandler != null)
					{
						this.serverEventHandler.processContext(obj, ttransport2);
					}
					if (!processor.Process(tprotocol, tprotocol2))
					{
						break;
					}
				}
			}
			catch (TTransportException)
			{
			}
			catch (Exception ex)
			{
				base.logDelegate("Error: " + ex);
			}
			if (this.serverEventHandler != null)
			{
				this.serverEventHandler.deleteContext(obj, tprotocol, tprotocol2);
			}
			if (ttransport2 != null)
			{
				ttransport2.Close();
			}
			if (ttransport3 != null)
			{
				ttransport3.Close();
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000650C File Offset: 0x0000470C
		public override void Stop()
		{
			this.stop = true;
			this.serverTransport.Close();
		}

		// Token: 0x040000A7 RID: 167
		private const int DEFAULT_MIN_THREADS = 10;

		// Token: 0x040000A8 RID: 168
		private const int DEFAULT_MAX_THREADS = 100;

		// Token: 0x040000A9 RID: 169
		private volatile bool stop;
	}
}
