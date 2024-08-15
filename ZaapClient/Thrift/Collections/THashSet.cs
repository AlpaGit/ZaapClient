using System;
using System.Collections;
using System.Collections.Generic;

namespace Thrift.Collections
{
	// Token: 0x02000003 RID: 3
	[Serializable]
	public class THashSet<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000021C8 File Offset: 0x000003C8
		public int Count
		{
			get
			{
				return this.set.Count;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000021D8 File Offset: 0x000003D8
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021DC File Offset: 0x000003DC
		public void Add(T item)
		{
			this.set.Add(item);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021EC File Offset: 0x000003EC
		public void Clear()
		{
			this.set.Clear();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021FC File Offset: 0x000003FC
		public bool Contains(T item)
		{
			return this.set.Contains(item);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000220C File Offset: 0x0000040C
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.set.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000221C File Offset: 0x0000041C
		public IEnumerator GetEnumerator()
		{
			return this.set.GetEnumerator();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002230 File Offset: 0x00000430
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return ((IEnumerable<T>)this.set).GetEnumerator();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002240 File Offset: 0x00000440
		public bool Remove(T item)
		{
			return this.set.Remove(item);
		}

		// Token: 0x04000001 RID: 1
		private HashSet<T> set = new HashSet<T>();
	}
}
