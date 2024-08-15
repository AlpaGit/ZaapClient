using System;

namespace Thrift.Protocol
{
	// Token: 0x02000022 RID: 34
	public struct TStruct
	{
		// Token: 0x06000164 RID: 356 RVA: 0x000054B4 File Offset: 0x000036B4
		public TStruct(string name)
		{
			this = default(TStruct);
			this.name = name;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000165 RID: 357 RVA: 0x000054C4 File Offset: 0x000036C4
		// (set) Token: 0x06000166 RID: 358 RVA: 0x000054CC File Offset: 0x000036CC
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000079 RID: 121
		private string name;
	}
}
