using System;

namespace Zaap_CSharp_Client
{
	// Token: 0x02000050 RID: 80
	public class ZaapClientParameters
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00009A88 File Offset: 0x00007C88
		public bool Valid()
		{
			return this.port != 0 && !string.IsNullOrEmpty(this.name) && !string.IsNullOrEmpty(this.release) && !string.IsNullOrEmpty(this.hash);
		}

		// Token: 0x0400013F RID: 319
		public int port;

		// Token: 0x04000140 RID: 320
		public string name;

		// Token: 0x04000141 RID: 321
		public string release;

		// Token: 0x04000142 RID: 322
		public int instanceId;

		// Token: 0x04000143 RID: 323
		public string hash;
	}
}
