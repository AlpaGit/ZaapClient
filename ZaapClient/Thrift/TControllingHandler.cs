using System;
using Thrift.Server;

namespace Thrift
{
	// Token: 0x02000004 RID: 4
	public interface TControllingHandler
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14
		// (set) Token: 0x0600000F RID: 15
		TServer server { get; set; }
	}
}
