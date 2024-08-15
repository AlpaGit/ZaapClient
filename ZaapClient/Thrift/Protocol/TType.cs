using System;

namespace Thrift.Protocol
{
	// Token: 0x02000023 RID: 35
	public enum TType : byte
	{
		// Token: 0x0400007B RID: 123
		Stop,
		// Token: 0x0400007C RID: 124
		Void,
		// Token: 0x0400007D RID: 125
		Bool,
		// Token: 0x0400007E RID: 126
		Byte,
		// Token: 0x0400007F RID: 127
		Double,
		// Token: 0x04000080 RID: 128
		I16 = 6,
		// Token: 0x04000081 RID: 129
		I32 = 8,
		// Token: 0x04000082 RID: 130
		I64 = 10,
		// Token: 0x04000083 RID: 131
		String,
		// Token: 0x04000084 RID: 132
		Struct,
		// Token: 0x04000085 RID: 133
		Map,
		// Token: 0x04000086 RID: 134
		Set,
		// Token: 0x04000087 RID: 135
		List
	}
}
