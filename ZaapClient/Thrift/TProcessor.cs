using System;
using Thrift.Protocol;

namespace Thrift
{
	// Token: 0x0200002F RID: 47
	public interface TProcessor
	{
		// Token: 0x060001A1 RID: 417
		bool Process(TProtocol iprot, TProtocol oprot);
	}
}
