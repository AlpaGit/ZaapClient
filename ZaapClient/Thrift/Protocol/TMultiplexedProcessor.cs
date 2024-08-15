using System;
using System.Collections.Generic;
using System.IO;

namespace Thrift.Protocol
{
	// Token: 0x02000019 RID: 25
	public class TMultiplexedProcessor : TProcessor
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004C80 File Offset: 0x00002E80
		public void RegisterProcessor(string serviceName, TProcessor processor)
		{
			this.ServiceProcessorMap.Add(serviceName, processor);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004C90 File Offset: 0x00002E90
		private void Fail(TProtocol oprot, TMessage message, TApplicationException.ExceptionType extype, string etxt)
		{
			TApplicationException ex = new TApplicationException(extype, etxt);
			TMessage tmessage = new TMessage(message.Name, TMessageType.Exception, message.SeqID);
			oprot.WriteMessageBegin(tmessage);
			ex.Write(oprot);
			oprot.WriteMessageEnd();
			oprot.Transport.Flush();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004CDC File Offset: 0x00002EDC
		public bool Process(TProtocol iprot, TProtocol oprot)
		{
			bool flag;
			try
			{
				TMessage tmessage = iprot.ReadMessageBegin();
				if (tmessage.Type != TMessageType.Call && tmessage.Type != TMessageType.Oneway)
				{
					this.Fail(oprot, tmessage, TApplicationException.ExceptionType.InvalidMessageType, "Message type CALL or ONEWAY expected");
					flag = false;
				}
				else
				{
					int num = tmessage.Name.IndexOf(TMultiplexedProtocol.SEPARATOR);
					if (num < 0)
					{
						this.Fail(oprot, tmessage, TApplicationException.ExceptionType.InvalidProtocol, "Service name not found in message name: " + tmessage.Name + ". Did you forget to use a TMultiplexProtocol in your client?");
						flag = false;
					}
					else
					{
						string text = tmessage.Name.Substring(0, num);
						TProcessor tprocessor;
						if (!this.ServiceProcessorMap.TryGetValue(text, out tprocessor))
						{
							this.Fail(oprot, tmessage, TApplicationException.ExceptionType.InternalError, "Service name not found: " + text + ". Did you forget to call RegisterProcessor()?");
							flag = false;
						}
						else
						{
							TMessage tmessage2 = new TMessage(tmessage.Name.Substring(text.Length + TMultiplexedProtocol.SEPARATOR.Length), tmessage.Type, tmessage.SeqID);
							flag = tprocessor.Process(new TMultiplexedProcessor.StoredMessageProtocol(iprot, tmessage2), oprot);
						}
					}
				}
			}
			catch (IOException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000065 RID: 101
		private Dictionary<string, TProcessor> ServiceProcessorMap = new Dictionary<string, TProcessor>();

		// Token: 0x0200001A RID: 26
		private class StoredMessageProtocol : TProtocolDecorator
		{
			// Token: 0x060000F7 RID: 247 RVA: 0x00004E10 File Offset: 0x00003010
			public StoredMessageProtocol(TProtocol protocol, TMessage messageBegin)
				: base(protocol)
			{
				this.MsgBegin = messageBegin;
			}

			// Token: 0x060000F8 RID: 248 RVA: 0x00004E20 File Offset: 0x00003020
			public override TMessage ReadMessageBegin()
			{
				return this.MsgBegin;
			}

			// Token: 0x04000066 RID: 102
			private TMessage MsgBegin;
		}
	}
}
