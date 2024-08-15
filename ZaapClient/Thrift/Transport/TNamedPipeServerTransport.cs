using System;
using System.IO.Pipes;
using System.Threading;

namespace Thrift.Transport
{
	// Token: 0x02000037 RID: 55
	public class TNamedPipeServerTransport : TServerTransport
	{
		// Token: 0x060001EB RID: 491 RVA: 0x0000772C File Offset: 0x0000592C
		public TNamedPipeServerTransport(string pipeAddress)
		{
			this.pipeAddress = pipeAddress;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00007744 File Offset: 0x00005944
		public override void Listen()
		{
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007748 File Offset: 0x00005948
		public override void Close()
		{
			if (this.stream != null)
			{
				try
				{
					this.stream.Close();
					this.stream.Dispose();
				}
				finally
				{
					this.stream = null;
				}
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00007794 File Offset: 0x00005994
		private void EnsurePipeInstance()
		{
			if (this.stream == null)
			{
				PipeDirection pipeDirection = PipeDirection.InOut;
				int num = 254;
				PipeTransmissionMode pipeTransmissionMode = PipeTransmissionMode.Byte;
				PipeOptions pipeOptions = ((!this.asyncMode) ? PipeOptions.None : PipeOptions.Asynchronous);
				int num2 = 4096;
				int num3 = 4096;
				try
				{
					this.stream = new NamedPipeServerStream(this.pipeAddress, pipeDirection, num, pipeTransmissionMode, pipeOptions, num2, num3);
				}
				catch (NotImplementedException)
				{
					if (!this.asyncMode)
					{
						throw;
					}
					pipeOptions &= ~PipeOptions.Asynchronous;
					this.stream = new NamedPipeServerStream(this.pipeAddress, pipeDirection, num, pipeTransmissionMode, pipeOptions, num2, num3);
					this.asyncMode = false;
				}
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000784C File Offset: 0x00005A4C
		protected override TTransport AcceptImpl()
		{
			TTransport ttransport;
			try
			{
				this.EnsurePipeInstance();
				if (this.asyncMode)
				{
					ManualResetEvent evt = new ManualResetEvent(false);
					Exception eOuter = null;
					this.stream.BeginWaitForConnection(delegate(IAsyncResult asyncResult)
					{
						try
						{
							if (this.stream != null)
							{
								this.stream.EndWaitForConnection(asyncResult);
							}
							else
							{
								eOuter = new TTransportException(TTransportException.ExceptionType.Interrupted);
							}
						}
						catch (Exception ex2)
						{
							if (this.stream != null)
							{
								eOuter = ex2;
							}
							else
							{
								eOuter = new TTransportException(TTransportException.ExceptionType.Interrupted, ex2.Message);
							}
						}
						evt.Set();
					}, null);
					evt.WaitOne();
					if (eOuter != null)
					{
						throw eOuter;
					}
				}
				else
				{
					this.stream.WaitForConnection();
				}
				TNamedPipeServerTransport.ServerTransport serverTransport = new TNamedPipeServerTransport.ServerTransport(this.stream, this.asyncMode);
				this.stream = null;
				ttransport = serverTransport;
			}
			catch (TTransportException)
			{
				this.Close();
				throw;
			}
			catch (Exception ex)
			{
				this.Close();
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, ex.Message);
			}
			return ttransport;
		}

		// Token: 0x040000DE RID: 222
		private readonly string pipeAddress;

		// Token: 0x040000DF RID: 223
		private NamedPipeServerStream stream;

		// Token: 0x040000E0 RID: 224
		private bool asyncMode = true;

		// Token: 0x02000038 RID: 56
		private class ServerTransport : TTransport
		{
			// Token: 0x060001F0 RID: 496 RVA: 0x0000792C File Offset: 0x00005B2C
			public ServerTransport(NamedPipeServerStream stream, bool asyncMode)
			{
				this.stream = stream;
				this.asyncMode = asyncMode;
			}

			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060001F1 RID: 497 RVA: 0x00007944 File Offset: 0x00005B44
			public override bool IsOpen
			{
				get
				{
					return this.stream != null && this.stream.IsConnected;
				}
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x00007960 File Offset: 0x00005B60
			public override void Open()
			{
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x00007964 File Offset: 0x00005B64
			public override void Close()
			{
				if (this.stream != null)
				{
					this.stream.Close();
				}
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x0000797C File Offset: 0x00005B7C
			public override int Read(byte[] buf, int off, int len)
			{
				if (this.stream == null)
				{
					throw new TTransportException(TTransportException.ExceptionType.NotOpen);
				}
				if (!this.asyncMode)
				{
					return this.stream.Read(buf, off, len);
				}
				Exception eOuter = null;
				ManualResetEvent evt = new ManualResetEvent(false);
				int retval = 0;
				this.stream.BeginRead(buf, off, len, delegate(IAsyncResult asyncResult)
				{
					try
					{
						if (this.stream != null)
						{
							retval = this.stream.EndRead(asyncResult);
						}
						else
						{
							eOuter = new TTransportException(TTransportException.ExceptionType.Interrupted);
						}
					}
					catch (Exception ex)
					{
						if (this.stream != null)
						{
							eOuter = ex;
						}
						else
						{
							eOuter = new TTransportException(TTransportException.ExceptionType.Interrupted, ex.Message);
						}
					}
					evt.Set();
				}, null);
				evt.WaitOne();
				if (eOuter != null)
				{
					throw eOuter;
				}
				return retval;
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x00007A1C File Offset: 0x00005C1C
			public override void Write(byte[] buf, int off, int len)
			{
				if (this.stream == null)
				{
					throw new TTransportException(TTransportException.ExceptionType.NotOpen);
				}
				if (this.asyncMode)
				{
					Exception eOuter = null;
					ManualResetEvent evt = new ManualResetEvent(false);
					this.stream.BeginWrite(buf, off, len, delegate(IAsyncResult asyncResult)
					{
						try
						{
							if (this.stream != null)
							{
								this.stream.EndWrite(asyncResult);
							}
							else
							{
								eOuter = new TTransportException(TTransportException.ExceptionType.Interrupted);
							}
						}
						catch (Exception ex)
						{
							if (this.stream != null)
							{
								eOuter = ex;
							}
							else
							{
								eOuter = new TTransportException(TTransportException.ExceptionType.Interrupted, ex.Message);
							}
						}
						evt.Set();
					}, null);
					evt.WaitOne();
					if (eOuter != null)
					{
						throw eOuter;
					}
				}
				else
				{
					this.stream.Write(buf, off, len);
				}
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x00007AB4 File Offset: 0x00005CB4
			protected override void Dispose(bool disposing)
			{
				if (this.stream != null)
				{
					this.stream.Dispose();
				}
			}

			// Token: 0x040000E1 RID: 225
			private NamedPipeServerStream stream;

			// Token: 0x040000E2 RID: 226
			private bool asyncMode;
		}
	}
}
