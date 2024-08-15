using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Thrift.Transport
{
	// Token: 0x02000033 RID: 51
	public class THttpClient : TTransport, IDisposable
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00006D68 File Offset: 0x00004F68
		public THttpClient(Uri u)
			: this(u, Enumerable.Empty<X509Certificate>())
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00006D78 File Offset: 0x00004F78
		public THttpClient(Uri u, IEnumerable<X509Certificate> certificates)
		{
			this.uri = u;
			this.certificates = (certificates ?? Enumerable.Empty<X509Certificate>()).ToArray<X509Certificate>();
		}

		// Token: 0x17000019 RID: 25
		// (set) Token: 0x060001BD RID: 445 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public int ConnectTimeout
		{
			set
			{
				this.connectTimeout = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00006DF0 File Offset: 0x00004FF0
		public int ReadTimeout
		{
			set
			{
				this.readTimeout = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00006DFC File Offset: 0x00004FFC
		public IDictionary<string, string> CustomHeaders
		{
			get
			{
				return this.customHeaders;
			}
		}

		// Token: 0x1700001C RID: 28
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00006E04 File Offset: 0x00005004
		public IWebProxy Proxy
		{
			set
			{
				this.proxy = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00006E10 File Offset: 0x00005010
		public override bool IsOpen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006E14 File Offset: 0x00005014
		public override void Open()
		{
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00006E18 File Offset: 0x00005018
		public override void Close()
		{
			if (this.inputStream != null)
			{
				this.inputStream.Close();
				this.inputStream = null;
			}
			if (this.outputStream != null)
			{
				this.outputStream.Close();
				this.outputStream = null;
			}
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00006E54 File Offset: 0x00005054
		public override int Read(byte[] buf, int off, int len)
		{
			if (this.inputStream == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "No request has been sent");
			}
			int num2;
			try
			{
				int num = this.inputStream.Read(buf, off, len);
				if (num == -1)
				{
					throw new TTransportException(TTransportException.ExceptionType.EndOfFile, "No more data available");
				}
				num2 = num;
			}
			catch (IOException ex)
			{
				throw new TTransportException(TTransportException.ExceptionType.Unknown, ex.ToString());
			}
			return num2;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006EC0 File Offset: 0x000050C0
		public override void Write(byte[] buf, int off, int len)
		{
			this.outputStream.Write(buf, off, len);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00006ED0 File Offset: 0x000050D0
		public override void Flush()
		{
			try
			{
				this.SendRequest();
			}
			finally
			{
				this.outputStream = new MemoryStream();
			}
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006F04 File Offset: 0x00005104
		private void SendRequest()
		{
			try
			{
				HttpWebRequest httpWebRequest = this.CreateRequest();
				byte[] array = this.outputStream.ToArray();
				httpWebRequest.ContentLength = (long)array.Length;
				using (Stream requestStream = httpWebRequest.GetRequestStream())
				{
					requestStream.Write(array, 0, array.Length);
					using (WebResponse response = httpWebRequest.GetResponse())
					{
						using (Stream responseStream = response.GetResponseStream())
						{
							this.inputStream = new MemoryStream();
							byte[] array2 = new byte[8096];
							int num;
							while ((num = responseStream.Read(array2, 0, array2.Length)) > 0)
							{
								this.inputStream.Write(array2, 0, num);
							}
							this.inputStream.Seek(0L, SeekOrigin.Begin);
						}
					}
				}
			}
			catch (IOException ex)
			{
				throw new TTransportException(TTransportException.ExceptionType.Unknown, ex.ToString());
			}
			catch (WebException ex2)
			{
				throw new TTransportException(TTransportException.ExceptionType.Unknown, "Couldn't connect to server: " + ex2);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00007044 File Offset: 0x00005244
		private HttpWebRequest CreateRequest()
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(this.uri);
			httpWebRequest.ClientCertificates.AddRange(this.certificates);
			if (this.connectTimeout > 0)
			{
				httpWebRequest.Timeout = this.connectTimeout;
			}
			if (this.readTimeout > 0)
			{
				httpWebRequest.ReadWriteTimeout = this.readTimeout;
			}
			httpWebRequest.ContentType = "application/x-thrift";
			httpWebRequest.Accept = "application/x-thrift";
			httpWebRequest.UserAgent = "C#/THttpClient";
			httpWebRequest.Method = "POST";
			httpWebRequest.ProtocolVersion = HttpVersion.Version10;
			foreach (KeyValuePair<string, string> keyValuePair in this.customHeaders)
			{
				httpWebRequest.Headers.Add(keyValuePair.Key, keyValuePair.Value);
			}
			httpWebRequest.Proxy = this.proxy;
			return httpWebRequest;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00007148 File Offset: 0x00005348
		public override IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			byte[] array = this.outputStream.ToArray();
			IAsyncResult asyncResult;
			try
			{
				THttpClient.FlushAsyncResult flushAsyncResult = new THttpClient.FlushAsyncResult(callback, state);
				flushAsyncResult.Connection = this.CreateRequest();
				flushAsyncResult.Data = array;
				flushAsyncResult.Connection.BeginGetRequestStream(new AsyncCallback(this.GetRequestStreamCallback), flushAsyncResult);
				asyncResult = flushAsyncResult;
			}
			catch (IOException ex)
			{
				throw new TTransportException(ex.ToString());
			}
			return asyncResult;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000071BC File Offset: 0x000053BC
		public override void EndFlush(IAsyncResult asyncResult)
		{
			try
			{
				THttpClient.FlushAsyncResult flushAsyncResult = (THttpClient.FlushAsyncResult)asyncResult;
				if (!flushAsyncResult.IsCompleted)
				{
					WaitHandle asyncWaitHandle = flushAsyncResult.AsyncWaitHandle;
					asyncWaitHandle.WaitOne();
					asyncWaitHandle.Close();
				}
				if (flushAsyncResult.AsyncException != null)
				{
					throw flushAsyncResult.AsyncException;
				}
			}
			finally
			{
				this.outputStream = new MemoryStream();
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00007224 File Offset: 0x00005424
		private void GetRequestStreamCallback(IAsyncResult asynchronousResult)
		{
			THttpClient.FlushAsyncResult flushAsyncResult = (THttpClient.FlushAsyncResult)asynchronousResult.AsyncState;
			try
			{
				Stream stream = flushAsyncResult.Connection.EndGetRequestStream(asynchronousResult);
				stream.Write(flushAsyncResult.Data, 0, flushAsyncResult.Data.Length);
				stream.Flush();
				stream.Close();
				flushAsyncResult.Connection.BeginGetResponse(new AsyncCallback(this.GetResponseCallback), flushAsyncResult);
			}
			catch (Exception ex)
			{
				flushAsyncResult.AsyncException = new TTransportException(ex.ToString());
				flushAsyncResult.UpdateStatusToComplete();
				flushAsyncResult.NotifyCallbackWhenAvailable();
			}
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000072BC File Offset: 0x000054BC
		private void GetResponseCallback(IAsyncResult asynchronousResult)
		{
			THttpClient.FlushAsyncResult flushAsyncResult = (THttpClient.FlushAsyncResult)asynchronousResult.AsyncState;
			try
			{
				this.inputStream = flushAsyncResult.Connection.EndGetResponse(asynchronousResult).GetResponseStream();
			}
			catch (Exception ex)
			{
				flushAsyncResult.AsyncException = new TTransportException(ex.ToString());
			}
			flushAsyncResult.UpdateStatusToComplete();
			flushAsyncResult.NotifyCallbackWhenAvailable();
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00007324 File Offset: 0x00005524
		protected override void Dispose(bool disposing)
		{
			if (!this._IsDisposed && disposing)
			{
				if (this.inputStream != null)
				{
					this.inputStream.Dispose();
				}
				if (this.outputStream != null)
				{
					this.outputStream.Dispose();
				}
			}
			this._IsDisposed = true;
		}

		// Token: 0x040000C5 RID: 197
		private readonly Uri uri;

		// Token: 0x040000C6 RID: 198
		private readonly X509Certificate[] certificates;

		// Token: 0x040000C7 RID: 199
		private Stream inputStream;

		// Token: 0x040000C8 RID: 200
		private MemoryStream outputStream = new MemoryStream();

		// Token: 0x040000C9 RID: 201
		private int connectTimeout = 30000;

		// Token: 0x040000CA RID: 202
		private int readTimeout = 30000;

		// Token: 0x040000CB RID: 203
		private IDictionary<string, string> customHeaders = new Dictionary<string, string>();

		// Token: 0x040000CC RID: 204
		private IWebProxy proxy = WebRequest.DefaultWebProxy;

		// Token: 0x040000CD RID: 205
		private bool _IsDisposed;

		// Token: 0x02000034 RID: 52
		private class FlushAsyncResult : IAsyncResult
		{
			// Token: 0x060001CE RID: 462 RVA: 0x00007378 File Offset: 0x00005578
			public FlushAsyncResult(AsyncCallback cbMethod, object state)
			{
				this._cbMethod = cbMethod;
				this._state = state;
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060001CF RID: 463 RVA: 0x0000739C File Offset: 0x0000559C
			// (set) Token: 0x060001D0 RID: 464 RVA: 0x000073A4 File Offset: 0x000055A4
			internal byte[] Data { get; set; }

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x000073B0 File Offset: 0x000055B0
			// (set) Token: 0x060001D2 RID: 466 RVA: 0x000073B8 File Offset: 0x000055B8
			internal HttpWebRequest Connection { get; set; }

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060001D3 RID: 467 RVA: 0x000073C4 File Offset: 0x000055C4
			// (set) Token: 0x060001D4 RID: 468 RVA: 0x000073CC File Offset: 0x000055CC
			internal TTransportException AsyncException { get; set; }

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060001D5 RID: 469 RVA: 0x000073D8 File Offset: 0x000055D8
			public object AsyncState
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060001D6 RID: 470 RVA: 0x000073E0 File Offset: 0x000055E0
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.GetEvtHandle();
				}
			}

			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060001D7 RID: 471 RVA: 0x000073E8 File Offset: 0x000055E8
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000024 RID: 36
			// (get) Token: 0x060001D8 RID: 472 RVA: 0x000073EC File Offset: 0x000055EC
			public bool IsCompleted
			{
				get
				{
					return this._isCompleted;
				}
			}

			// Token: 0x060001D9 RID: 473 RVA: 0x000073F8 File Offset: 0x000055F8
			private ManualResetEvent GetEvtHandle()
			{
				object locker = this._locker;
				lock (locker)
				{
					if (this._evt == null)
					{
						this._evt = new ManualResetEvent(false);
					}
					if (this._isCompleted)
					{
						this._evt.Set();
					}
				}
				return this._evt;
			}

			// Token: 0x060001DA RID: 474 RVA: 0x00007464 File Offset: 0x00005664
			internal void UpdateStatusToComplete()
			{
				this._isCompleted = true;
				object locker = this._locker;
				lock (locker)
				{
					if (this._evt != null)
					{
						this._evt.Set();
					}
				}
			}

			// Token: 0x060001DB RID: 475 RVA: 0x000074BC File Offset: 0x000056BC
			internal void NotifyCallbackWhenAvailable()
			{
				if (this._cbMethod != null)
				{
					this._cbMethod(this);
				}
			}

			// Token: 0x040000CE RID: 206
			private volatile bool _isCompleted;

			// Token: 0x040000CF RID: 207
			private ManualResetEvent _evt;

			// Token: 0x040000D0 RID: 208
			private readonly AsyncCallback _cbMethod;

			// Token: 0x040000D1 RID: 209
			private readonly object _state;

			// Token: 0x040000D5 RID: 213
			private readonly object _locker = new object();
		}
	}
}
