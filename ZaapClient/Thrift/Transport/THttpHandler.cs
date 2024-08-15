using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using Thrift.Protocol;

namespace Thrift.Transport
{
	// Token: 0x02000035 RID: 53
	public class THttpHandler : IHttpHandler
	{
		// Token: 0x060001DC RID: 476 RVA: 0x000074D8 File Offset: 0x000056D8
		public THttpHandler(TProcessor processor)
			: this(processor, new TBinaryProtocol.Factory())
		{
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000074E8 File Offset: 0x000056E8
		public THttpHandler(TProcessor processor, TProtocolFactory protocolFactory)
			: this(processor, protocolFactory, protocolFactory)
		{
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000074F4 File Offset: 0x000056F4
		public THttpHandler(TProcessor processor, TProtocolFactory inputProtocolFactory, TProtocolFactory outputProtocolFactory)
		{
			this.processor = processor;
			this.inputProtocolFactory = inputProtocolFactory;
			this.outputProtocolFactory = outputProtocolFactory;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000751C File Offset: 0x0000571C
		public void ProcessRequest(HttpListenerContext context)
		{
			context.Response.ContentType = "application/x-thrift";
			context.Response.ContentEncoding = this.encoding;
			this.ProcessRequest(context.Request.InputStream, context.Response.OutputStream);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000755C File Offset: 0x0000575C
		public void ProcessRequest(HttpContext context)
		{
			context.Response.ContentType = "application/x-thrift";
			context.Response.ContentEncoding = this.encoding;
			this.ProcessRequest(context.Request.InputStream, context.Response.OutputStream);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000759C File Offset: 0x0000579C
		public void ProcessRequest(Stream input, Stream output)
		{
			TTransport ttransport = new TStreamTransport(input, output);
			try
			{
				TProtocol protocol = this.inputProtocolFactory.GetProtocol(ttransport);
				TProtocol protocol2 = this.outputProtocolFactory.GetProtocol(ttransport);
				while (this.processor.Process(protocol, protocol2))
				{
				}
			}
			catch (TTransportException)
			{
			}
			finally
			{
				ttransport.Close();
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00007610 File Offset: 0x00005810
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040000D6 RID: 214
		protected TProcessor processor;

		// Token: 0x040000D7 RID: 215
		protected TProtocolFactory inputProtocolFactory;

		// Token: 0x040000D8 RID: 216
		protected TProtocolFactory outputProtocolFactory;

		// Token: 0x040000D9 RID: 217
		protected const string contentType = "application/x-thrift";

		// Token: 0x040000DA RID: 218
		protected Encoding encoding = Encoding.UTF8;
	}
}
