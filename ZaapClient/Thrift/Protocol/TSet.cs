using System;

namespace Thrift.Protocol
{
	// Token: 0x02000021 RID: 33
	public struct TSet
	{
		// Token: 0x0600015E RID: 350 RVA: 0x0000545C File Offset: 0x0000365C
		public TSet(TType elementType, int count)
		{
			this = default(TSet);
			this.elementType = elementType;
			this.count = count;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00005474 File Offset: 0x00003674
		public TSet(TList list)
		{
			this = new TSet(list.ElementType, list.Count);
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000160 RID: 352 RVA: 0x0000548C File Offset: 0x0000368C
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00005494 File Offset: 0x00003694
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000054A0 File Offset: 0x000036A0
		// (set) Token: 0x06000163 RID: 355 RVA: 0x000054A8 File Offset: 0x000036A8
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

		// Token: 0x04000077 RID: 119
		private TType elementType;

		// Token: 0x04000078 RID: 120
		private int count;
	}
}
