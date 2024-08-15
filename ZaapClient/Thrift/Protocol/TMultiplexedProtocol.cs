using System;

namespace Thrift.Protocol
{
	// Token: 0x0200001B RID: 27
	public class TMultiplexedProtocol : TProtocolDecorator
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004E28 File Offset: 0x00003028
		public TMultiplexedProtocol(TProtocol protocol, string serviceName)
			: base(protocol)
		{
			this.ServiceName = serviceName;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004E38 File Offset: 0x00003038
		public override void WriteMessageBegin(TMessage tMessage)
		{
			TMessageType type = tMessage.Type;
			if (type != TMessageType.Call && type != TMessageType.Oneway)
			{
				base.WriteMessageBegin(tMessage);
			}
			else
			{
				base.WriteMessageBegin(new TMessage(this.ServiceName + TMultiplexedProtocol.SEPARATOR + tMessage.Name, tMessage.Type, tMessage.SeqID));
			}
		}

		// Token: 0x04000067 RID: 103
		public static string SEPARATOR = ":";

		// Token: 0x04000068 RID: 104
		private string ServiceName;
	}
}
