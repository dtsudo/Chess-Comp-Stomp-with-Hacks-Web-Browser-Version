
namespace DTLibrary
{
	using System;
	using System.Collections.Generic;

	public class ByteList : IEquatable<ByteList>
	{
		public class Builder
		{
			private List<byte> list;

			public Builder()
			{
				this.list = new List<byte>();
			}

			public ByteList ToByteList()
			{
				return new ByteList(this.list);
			}

			public void Add(byte b)
			{
				this.list.Add(b);
			}

			public void AddBool(bool b)
			{
				if (b)
					this.list.Add(1);
				else
					this.list.Add(0);
			}

			public void AddNullableBool(bool? b)
			{
				if (b == null)
					this.AddBool(false);
				else
				{
					this.AddBool(true);
					this.AddBool(b.Value);
				}
			}

			public void AddInt(int i)
			{
				int b1 = i & 0xff;
				int b2 = (i >> 8) & 0xff;
				int b3 = (i >> 16) & 0xff;
				int b4 = (i >> 24) & 0xff;

				this.list.Add((byte)b1);
				this.list.Add((byte)b2);
				this.list.Add((byte)b3);
				this.list.Add((byte)b4);
			}

			public void AddLong(long l)
			{
				long b1 = l & 0xff;
				long b2 = (l >> 8) & 0xff;
				long b3 = (l >> 16) & 0xff;
				long b4 = (l >> 24) & 0xff;
				long b5 = (l >> 32) & 0xff;
				long b6 = (l >> 40) & 0xff;
				long b7 = (l >> 48) & 0xff;
				long b8 = (l >> 56) & 0xff;

				this.list.Add((byte)b1);
				this.list.Add((byte)b2);
				this.list.Add((byte)b3);
				this.list.Add((byte)b4);
				this.list.Add((byte)b5);
				this.list.Add((byte)b6);
				this.list.Add((byte)b7);
				this.list.Add((byte)b8);
			}

			public void AddNullableInt(int? i)
			{
				if (i == null)
					this.AddBool(false);
				else
				{
					this.AddBool(true);
					this.AddInt(i.Value);
				}
			}

			public void AddNullableLong(long? l)
			{
				if (l == null)
					this.AddBool(false);
				else
				{
					this.AddBool(true);
					this.AddLong(l.Value);
				}
			}

			public void AddIntList(List<int> list)
			{
				if (list == null)
				{
					this.AddBool(false);
					return;
				}

				this.AddBool(true);

				this.AddInt(list.Count);

				foreach (int i in list)
				{
					this.AddInt(i);
				}
			}

			public void AddIntSet(HashSet<int> set)
			{
				if (set == null)
				{
					this.AddBool(false);
					return;
				}

				this.AddBool(true);

				List<int> list = new List<int>(set);
				list.Sort();
				this.AddIntList(list);
			}

			public void AddString(string str)
			{
				if (str == null)
				{
					this.AddBool(false);
					return;
				}

				this.AddBool(true);

				this.AddInt(str.Length);

				foreach (char c in str)
				{
					int cAsInt = c;
					this.AddInt(cAsInt);
				}
			}
		}

		public class Iterator
		{
			private ByteList byteList;
			private int index;

			public Iterator(ByteList byteList)
			{
				this.byteList = byteList;
				this.index = 0;
			}

			public bool HasNextByte()
			{
				return this.index < this.byteList.GetCount();
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public byte TryPop()
			{
				if (this.index >= this.byteList.GetCount())
					throw new DTDeserializationException();

				byte b = this.byteList.GetByte(this.index);

				this.index++;

				return b;
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public bool TryPopBool()
			{
				if (this.index >= this.byteList.GetCount())
					throw new DTDeserializationException();

				byte b = this.byteList.GetByte(this.index);

				this.index++;

				if (b == 1)
					return true;
				if (b == 0)
					return false;

				throw new DTDeserializationException();
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public bool? TryPopNullableBool()
			{
				bool b = this.TryPopBool();

				if (!b)
					return null;

				return this.TryPopBool();
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public int TryPopInt()
			{
				if (this.index + 3 >= this.byteList.GetCount())
					throw new DTDeserializationException();

				byte b1 = this.byteList.GetByte(this.index);
				byte b2 = this.byteList.GetByte(this.index + 1);
				byte b3 = this.byteList.GetByte(this.index + 2);
				byte b4 = this.byteList.GetByte(this.index + 3);

				this.index += 4;

				int i1 = b1;
				int i2 = b2 << 8;
				int i3 = b3 << 16;
				int i4 = b4 << 24;

				return i1 | i2 | i3 | i4;
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public long TryPopLong()
			{
				if (this.index + 7 >= this.byteList.GetCount())
					throw new DTDeserializationException();

				long b1 = this.byteList.GetByte(this.index);
				long b2 = this.byteList.GetByte(this.index + 1);
				long b3 = this.byteList.GetByte(this.index + 2);
				long b4 = this.byteList.GetByte(this.index + 3);
				long b5 = this.byteList.GetByte(this.index + 4);
				long b6 = this.byteList.GetByte(this.index + 5);
				long b7 = this.byteList.GetByte(this.index + 6);
				long b8 = this.byteList.GetByte(this.index + 7);

				this.index += 8;

				long l1 = b1;
				long l2 = b2 << 8;
				long l3 = b3 << 16;
				long l4 = b4 << 24;
				long l5 = b5 << 32;
				long l6 = b6 << 40;
				long l7 = b7 << 48;
				long l8 = b8 << 56;

				return l1 | l2 | l3 | l4 | l5 | l6 | l7 | l8;
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public int? TryPopNullableInt()
			{
				bool b = this.TryPopBool();

				if (!b)
					return null;

				return this.TryPopInt();
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public long? TryPopNullableLong()
			{
				bool b = this.TryPopBool();

				if (!b)
					return null;

				return this.TryPopLong();
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public List<int> TryPopIntList()
			{
				bool b = this.TryPopBool();

				if (!b)
					return null;

				int count = this.TryPopInt();

				List<int> list = new List<int>();

				for (int i = 0; i < count; i++)
					list.Add(this.TryPopInt());

				return list;
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public HashSet<int> TryPopIntSet()
			{
				bool b = this.TryPopBool();

				if (!b)
					return null;

				List<int> list = this.TryPopIntList();
				
				if (list == null)
					throw new DTDeserializationException();
				
				return new HashSet<int>(list);
			}

			/// <summary>
			/// Can possibly throw DTDeserializationException
			/// </summary>
			public string TryPopString()
			{
				bool b = this.TryPopBool();

				if (!b)
					return null;

				int count = this.TryPopInt();

				char[] array = new char[count];

				for (int i = 0; i < count; i++)
				{
					int cAsInt = this.TryPopInt();
					array[i] = (char)cAsInt;
				}

				return new string(array);
			}
		}

		private List<byte> list;

		private ByteList(List<byte> list)
		{
			this.list = new List<byte>();
			foreach (byte b in list)
				this.list.Add(b);
		}

		private byte GetByte(int index)
		{
			return this.list[index];
		}

		private int GetCount()
		{
			return this.list.Count;
		}

		public Iterator GetIterator()
		{
			return new Iterator(byteList: this);
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as ByteList);
		}

		public bool Equals(ByteList other)
		{
			if (other == null)
				return false;

			if (this == other)
				return true;

			if (other.list.Count != this.list.Count)
				return false;

			for (int i = 0; i < this.list.Count; i++)
			{
				byte b1 = this.list[i];
				byte b2 = other.list[i];
				if (b1 != b2)
					return false;
			}

			return true;
		}

		public override int GetHashCode()
		{
			int hashCode = 0;
			for (int i = 0; i < this.list.Count; i++)
			{
				byte b = this.list[i];
				int bAsInt = b;

				hashCode = unchecked(hashCode * 17 + bAsInt);
			}

			return hashCode;
		}
	}
}
