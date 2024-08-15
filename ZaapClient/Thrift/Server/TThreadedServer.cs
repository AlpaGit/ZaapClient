using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Thrift.Collections;
using Thrift.Protocol;
using Thrift.Transport;

namespace Thrift.Server
{
	// Token: 0x02000026 RID: 38
	public class TThreadedServer : TServer
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00005578 File Offset: 0x00003778
		public TThreadedServer(TProcessor processor, TServerTransport serverTransport)
			: this(new TSingletonProcessorFactory(processor), serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 100, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000055C8 File Offset: 0x000037C8
		public TThreadedServer(TProcessor processor, TServerTransport serverTransport, TServer.LogDelegate logDelegate)
			: this(new TSingletonProcessorFactory(processor), serverTransport, new TTransportFactory(), new TTransportFactory(), new TBinaryProtocol.Factory(), new TBinaryProtocol.Factory(), 100, logDelegate)
		{
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000055FC File Offset: 0x000037FC
		public TThreadedServer(TProcessor processor, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(new TSingletonProcessorFactory(processor), serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, 100, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000563C File Offset: 0x0000383C
		public TThreadedServer(TProcessorFactory processorFactory, TServerTransport serverTransport, TTransportFactory transportFactory, TProtocolFactory protocolFactory)
			: this(processorFactory, serverTransport, transportFactory, transportFactory, protocolFactory, protocolFactory, 100, new TServer.LogDelegate(TServer.DefaultLogDelegate))
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005678 File Offset: 0x00003878
		public TThreadedServer(TProcessorFactory processorFactory, TServerTransport serverTransport, TTransportFactory inputTransportFactory, TTransportFactory outputTransportFactory, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory, int maxThreads, TServer.LogDelegate logDel)
			: base(processorFactory, serverTransport, inputTransportFactory, outputTransportFactory, inputProtocolFactory, outputProtocolFactory, logDel)
		{
			this.maxThreads = maxThreads;
			this.clientQueue = new Queue<TTransport>();
			this.clientLock = new object();
			this.clientThreads = new THashSet<Thread>();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000056B4 File Offset: 0x000038B4
		public int ClientThreadsCount
		{
			get
			{
				return this.clientThreads.Count;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000056C4 File Offset: 0x000038C4
		public override void Serve()
		{
			try
			{
				this.workerThread = new Thread(new ThreadStart(this.Execute));
				this.workerThread.Start();
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
					object obj = this.clientLock;
					lock (obj)
					{
						this.clientQueue.Enqueue(ttransport);
						Monitor.Pulse(this.clientLock);
					}
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
					base.logDelegate("TServeTransport failed on close: " + ex3.Message);
				}
				this.stop = false;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005840 File Offset: 0x00003A40
		private void Execute()
		{
			while (!this.stop)
			{
				object obj = this.clientLock;
				TTransport ttransport;
				Thread thread;
				lock (obj)
				{
					while (this.clientThreads.Count >= this.maxThreads)
					{
						Monitor.Wait(this.clientLock);
					}
					while (this.clientQueue.Count == 0)
					{
						Monitor.Wait(this.clientLock);
					}
					ttransport = this.clientQueue.Dequeue();
					thread = new Thread(new ParameterizedThreadStart(this.ClientWorker));
					this.clientThreads.Add(thread);
				}
				thread.Start(ttransport);
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005904 File Offset: 0x00003B04
		private void ClientWorker(object context)
		{
			TTransport ttransport = (TTransport)context;
			TProcessor processor = this.processorFactory.GetProcessor(ttransport, null);
			TProtocol tprotocol = null;
			TProtocol tprotocol2 = null;
			object obj = null;
			try
			{
				TTransport transport;
				TTransport ttransport2 = (transport = this.inputTransportFactory.GetTransport(ttransport));
				try
				{
					TTransport transport2;
					TTransport ttransport3 = (transport2 = this.outputTransportFactory.GetTransport(ttransport));
					try
					{
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
			object obj2 = this.clientLock;
			lock (obj2)
			{
				this.clientThreads.Remove(Thread.CurrentThread);
				Monitor.Pulse(this.clientLock);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public override void Stop()
		{
			this.stop = true;
			this.serverTransport.Close();
			this.workerThread.Abort();
			IEnumerator enumerator = this.clientThreads.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Thread thread = (Thread)obj;
					thread.Abort();
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = enumerator as IDisposable) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0400008A RID: 138
		private const int DEFAULT_MAX_THREADS = 100;

		// Token: 0x0400008B RID: 139
		private volatile bool stop;

		// Token: 0x0400008C RID: 140
		private readonly int maxThreads;

		// Token: 0x0400008D RID: 141
		private Queue<TTransport> clientQueue;

		// Token: 0x0400008E RID: 142
		private THashSet<Thread> clientThreads;

		// Token: 0x0400008F RID: 143
		private object clientLock;

		// Token: 0x04000090 RID: 144
		private Thread workerThread;
	}
}
