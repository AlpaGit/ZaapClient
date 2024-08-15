using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Zaap_CSharp_Client
{
	// Token: 0x0200004F RID: 79
	public static class JSONParser
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00009090 File Offset: 0x00007290
		public static T FromJson<T>(this string json)
		{
			JSONParser.stringBuilder.Length = 0;
			for (int i = 0; i < json.Length; i++)
			{
				char c = json[i];
				if (c == '"')
				{
					i = JSONParser.AppendUntilStringEnd(true, i, json);
				}
				else if (!char.IsWhiteSpace(c))
				{
					JSONParser.stringBuilder.Append(c);
				}
			}
			return (T)((object)JSONParser.ParseValue(typeof(T), JSONParser.stringBuilder.ToString()));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009118 File Offset: 0x00007318
		private static int AppendUntilStringEnd(bool appendEscapeCharacter, int startIdx, string json)
		{
			JSONParser.stringBuilder.Append(json[startIdx]);
			for (int i = startIdx + 1; i < json.Length; i++)
			{
				if (json[i] == '\\')
				{
					if (appendEscapeCharacter)
					{
						JSONParser.stringBuilder.Append(json[i]);
					}
					JSONParser.stringBuilder.Append(json[i + 1]);
					i++;
				}
				else
				{
					if (json[i] == '"')
					{
						JSONParser.stringBuilder.Append(json[i]);
						return i;
					}
					JSONParser.stringBuilder.Append(json[i]);
				}
			}
			return json.Length - 1;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000091D0 File Offset: 0x000073D0
		private static List<string> Split(string json)
		{
			List<string> list = ((JSONParser.splitArrayPool.Count <= 0) ? new List<string>() : JSONParser.splitArrayPool.Pop());
			list.Clear();
			int num = 0;
			JSONParser.stringBuilder.Length = 0;
			int i = 1;
			while (i < json.Length - 1)
			{
				char c = json[i];
				switch (c)
				{
				case '[':
					goto IL_8E;
				default:
					switch (c)
					{
					case '{':
						goto IL_8E;
					default:
						if (c != '"')
						{
							if (c != ',' && c != ':')
							{
								goto IL_D9;
							}
							if (num != 0)
							{
								goto IL_D9;
							}
							list.Add(JSONParser.stringBuilder.ToString());
							JSONParser.stringBuilder.Length = 0;
						}
						else
						{
							i = JSONParser.AppendUntilStringEnd(true, i, json);
						}
						break;
					case '}':
						goto IL_97;
					}
					break;
				case ']':
					goto IL_97;
				}
				IL_EB:
				i++;
				continue;
				IL_D9:
				JSONParser.stringBuilder.Append(json[i]);
				goto IL_EB;
				IL_97:
				num--;
				goto IL_D9;
				IL_8E:
				num++;
				goto IL_D9;
			}
			list.Add(JSONParser.stringBuilder.ToString());
			return list;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000092EC File Offset: 0x000074EC
		internal static object ParseValue(Type type, string json)
		{
			if (type == typeof(string))
			{
				if (json.Length <= 2)
				{
					return string.Empty;
				}
				string text = json.Substring(1, json.Length - 2);
				return text.Replace("\\\\", "\"\"").Replace("\\", string.Empty).Replace("\"\"", "\\");
			}
			else
			{
				if (type == typeof(int))
				{
					int num;
					int.TryParse(json, out num);
					return num;
				}
				if (type == typeof(float))
				{
					float num2;
					float.TryParse(json, out num2);
					return num2;
				}
				if (type == typeof(double))
				{
					double num3;
					double.TryParse(json, out num3);
					return num3;
				}
				if (type == typeof(bool))
				{
					return json.ToLower() == "true";
				}
				if (json == "null")
				{
					return null;
				}
				if (type.IsArray)
				{
					Type elementType = type.GetElementType();
					if (json[0] != '[' || json[json.Length - 1] != ']')
					{
						return null;
					}
					List<string> list = JSONParser.Split(json);
					Array array = Array.CreateInstance(elementType, list.Count);
					for (int i = 0; i < list.Count; i++)
					{
						array.SetValue(JSONParser.ParseValue(elementType, list[i]), i);
					}
					JSONParser.splitArrayPool.Push(list);
					return array;
				}
				else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
				{
					Type type2 = type.GetGenericArguments()[0];
					if (json[0] != '[' || json[json.Length - 1] != ']')
					{
						return null;
					}
					List<string> list2 = JSONParser.Split(json);
					IList list3 = (IList)type.GetConstructor(new Type[] { typeof(int) }).Invoke(new object[] { list2.Count });
					for (int j = 0; j < list2.Count; j++)
					{
						list3.Add(JSONParser.ParseValue(type2, list2[j]));
					}
					JSONParser.splitArrayPool.Push(list2);
					return list3;
				}
				else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<, >))
				{
					Type[] genericArguments = type.GetGenericArguments();
					Type type3 = genericArguments[0];
					Type type4 = genericArguments[1];
					if (type3 != typeof(string))
					{
						return null;
					}
					if (json[0] != '{' || json[json.Length - 1] != '}')
					{
						return null;
					}
					List<string> list4 = JSONParser.Split(json);
					if (list4.Count % 2 != 0)
					{
						return null;
					}
					IDictionary dictionary = (IDictionary)type.GetConstructor(new Type[] { typeof(int) }).Invoke(new object[] { list4.Count / 2 });
					for (int k = 0; k < list4.Count; k += 2)
					{
						if (list4[k].Length > 2)
						{
							string text2 = list4[k].Substring(1, list4[k].Length - 2);
							object obj = JSONParser.ParseValue(type4, list4[k + 1]);
							dictionary.Add(text2, obj);
						}
					}
					return dictionary;
				}
				else
				{
					if (type == typeof(object))
					{
						return JSONParser.ParseAnonymousValue(json);
					}
					if (json[0] == '{' && json[json.Length - 1] == '}')
					{
						return JSONParser.ParseObject(type, json);
					}
					return null;
				}
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000096D8 File Offset: 0x000078D8
		private static object ParseAnonymousValue(string json)
		{
			if (json.Length == 0)
			{
				return null;
			}
			if (json[0] == '{' && json[json.Length - 1] == '}')
			{
				List<string> list = JSONParser.Split(json);
				if (list.Count % 2 != 0)
				{
					return null;
				}
				Dictionary<string, object> dictionary = new Dictionary<string, object>(list.Count / 2);
				for (int i = 0; i < list.Count; i += 2)
				{
					dictionary.Add(list[i].Substring(1, list[i].Length - 2), JSONParser.ParseAnonymousValue(list[i + 1]));
				}
				return dictionary;
			}
			else
			{
				if (json[0] == '[' && json[json.Length - 1] == ']')
				{
					List<string> list2 = JSONParser.Split(json);
					List<object> list3 = new List<object>(list2.Count);
					for (int j = 0; j < list2.Count; j++)
					{
						list3.Add(JSONParser.ParseAnonymousValue(list2[j]));
					}
					return list3;
				}
				if (json[0] == '"' && json[json.Length - 1] == '"')
				{
					string text = json.Substring(1, json.Length - 2);
					return text.Replace("\\", string.Empty);
				}
				if (char.IsDigit(json[0]) || json[0] == '-')
				{
					if (json.Contains("."))
					{
						double num;
						double.TryParse(json, out num);
						return num;
					}
					int num2;
					int.TryParse(json, out num2);
					return num2;
				}
				else
				{
					if (json == "true")
					{
						return true;
					}
					if (json == "false")
					{
						return false;
					}
					return null;
				}
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000098B0 File Offset: 0x00007AB0
		private static object ParseObject(Type type, string json)
		{
			object uninitializedObject = FormatterServices.GetUninitializedObject(type);
			List<string> list = JSONParser.Split(json);
			if (list.Count % 2 != 0)
			{
				return uninitializedObject;
			}
			Dictionary<string, FieldInfo> dictionary;
			if (!JSONParser.fieldInfoCache.TryGetValue(type, out dictionary))
			{
				dictionary = (from field in type.GetFields()
					where field.IsPublic
					select field).ToDictionary((FieldInfo field) => field.Name);
				JSONParser.fieldInfoCache.Add(type, dictionary);
			}
			Dictionary<string, PropertyInfo> dictionary2;
			if (!JSONParser.propertyInfoCache.TryGetValue(type, out dictionary2))
			{
				dictionary2 = type.GetProperties().ToDictionary((PropertyInfo p) => p.Name);
				JSONParser.propertyInfoCache.Add(type, dictionary2);
			}
			for (int i = 0; i < list.Count; i += 2)
			{
				if (list[i].Length > 2)
				{
					string text = list[i].Substring(1, list[i].Length - 2);
					string text2 = list[i + 1];
					FieldInfo fieldInfo;
					PropertyInfo propertyInfo;
					if (dictionary.TryGetValue(text, out fieldInfo))
					{
						fieldInfo.SetValue(uninitializedObject, JSONParser.ParseValue(fieldInfo.FieldType, text2));
					}
					else if (dictionary2.TryGetValue(text, out propertyInfo))
					{
						propertyInfo.SetValue(uninitializedObject, JSONParser.ParseValue(propertyInfo.PropertyType, text2), null);
					}
				}
			}
			return uninitializedObject;
		}

		// Token: 0x04000138 RID: 312
		private static Stack<List<string>> splitArrayPool = new Stack<List<string>>();

		// Token: 0x04000139 RID: 313
		private static StringBuilder stringBuilder = new StringBuilder();

		// Token: 0x0400013A RID: 314
		private static readonly Dictionary<Type, Dictionary<string, FieldInfo>> fieldInfoCache = new Dictionary<Type, Dictionary<string, FieldInfo>>();

		// Token: 0x0400013B RID: 315
		private static readonly Dictionary<Type, Dictionary<string, PropertyInfo>> propertyInfoCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
	}
}
