using System;

namespace Thrift.Protocol
{
	// Token: 0x0200000E RID: 14
	public struct TField
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000036E4 File Offset: 0x000018E4
		public TField(string name, TType type, short id)
		{
			this = default(TField);
			this.name = name;
			this.type = type;
			this.id = id;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003704 File Offset: 0x00001904
		// (set) Token: 0x06000087 RID: 135 RVA: 0x0000370C File Offset: 0x0000190C
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003718 File Offset: 0x00001918
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00003720 File Offset: 0x00001920
		public TType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000372C File Offset: 0x0000192C
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00003734 File Offset: 0x00001934
		public short ID
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x04000030 RID: 48
		private string name;

		// Token: 0x04000031 RID: 49
		private TType type;

		// Token: 0x04000032 RID: 50
		private short id;
	}
}
