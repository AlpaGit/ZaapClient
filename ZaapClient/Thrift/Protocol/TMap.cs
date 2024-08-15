using System;

namespace Thrift.Protocol
{
	// Token: 0x02000016 RID: 22
	public struct TMap
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x00004BB4 File Offset: 0x00002DB4
		public TMap(TType keyType, TType valueType, int count)
		{
			this = default(TMap);
			this.keyType = keyType;
			this.valueType = valueType;
			this.count = count;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004BD4 File Offset: 0x00002DD4
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004BDC File Offset: 0x00002DDC
		public TType KeyType
		{
			get
			{
				return this.keyType;
			}
			set
			{
				this.keyType = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004BE8 File Offset: 0x00002DE8
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004BF0 File Offset: 0x00002DF0
		public TType ValueType
		{
			get
			{
				return this.valueType;
			}
			set
			{
				this.valueType = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004BFC File Offset: 0x00002DFC
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00004C04 File Offset: 0x00002E04
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

		// Token: 0x0400005A RID: 90
		private TType keyType;

		// Token: 0x0400005B RID: 91
		private TType valueType;

		// Token: 0x0400005C RID: 92
		private int count;
	}
}
