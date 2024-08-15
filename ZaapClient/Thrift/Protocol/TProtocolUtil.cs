using System;

namespace Thrift.Protocol
{
	// Token: 0x02000020 RID: 32
	public static class TProtocolUtil
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00005288 File Offset: 0x00003488
		public static void Skip(TProtocol prot, TType type)
		{
			prot.IncrementRecursionDepth();
			try
			{
				switch (type)
				{
				case TType.Bool:
					prot.ReadBool();
					break;
				case TType.Byte:
					prot.ReadByte();
					break;
				case TType.Double:
					prot.ReadDouble();
					break;
				case TType.I16:
					prot.ReadI16();
					break;
				case TType.I32:
					prot.ReadI32();
					break;
				case TType.I64:
					prot.ReadI64();
					break;
				case TType.String:
					prot.ReadBinary();
					break;
				case TType.Struct:
					prot.ReadStructBegin();
					for (;;)
					{
						TField tfield = prot.ReadFieldBegin();
						if (tfield.Type == TType.Stop)
						{
							break;
						}
						TProtocolUtil.Skip(prot, tfield.Type);
						prot.ReadFieldEnd();
					}
					prot.ReadStructEnd();
					break;
				case TType.Map:
				{
					TMap tmap = prot.ReadMapBegin();
					for (int i = 0; i < tmap.Count; i++)
					{
						TProtocolUtil.Skip(prot, tmap.KeyType);
						TProtocolUtil.Skip(prot, tmap.ValueType);
					}
					prot.ReadMapEnd();
					break;
				}
				case TType.Set:
				{
					TSet tset = prot.ReadSetBegin();
					for (int j = 0; j < tset.Count; j++)
					{
						TProtocolUtil.Skip(prot, tset.ElementType);
					}
					prot.ReadSetEnd();
					break;
				}
				case TType.List:
				{
					TList tlist = prot.ReadListBegin();
					for (int k = 0; k < tlist.Count; k++)
					{
						TProtocolUtil.Skip(prot, tlist.ElementType);
					}
					prot.ReadListEnd();
					break;
				}
				}
			}
			finally
			{
				prot.DecrementRecursionDepth();
			}
		}
	}
}
