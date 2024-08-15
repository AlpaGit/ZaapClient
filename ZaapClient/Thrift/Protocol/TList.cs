using System;

namespace Thrift.Protocol
{
	// Token: 0x02000015 RID: 21
	public struct TList
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00004B74 File Offset: 0x00002D74
		public TList(TType elementType, int count)
		{
			this = default(TList);
			this.elementType = elementType;
			this.count = count;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00004B8C File Offset: 0x00002D8C
		// (set) Token: 0x060000E2 RID: 226 RVA: 0x00004B94 File Offset: 0x00002D94
		public TType ElementType
		{
			get
			{
				return this.elementType;
			}
			set
			{
				this.elementType = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00004BA0 File Offset: 0x00002DA0
		// (set) Token: 0x060000E4 RID: 228 RVA: 0x00004BA8 File Offset: 0x00002DA8
		public int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				this.count = value;
			}
		}

		// Token: 0x04000058 RID: 88
		private TType elementType;

		// Token: 0x04000059 RID: 89
		private int count;
	}
}
