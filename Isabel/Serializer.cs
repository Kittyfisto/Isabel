using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Isabel.Commands;

namespace Isabel
{
	public sealed class Serializer
	{
		private readonly Type[] _basicTypes;

		public Serializer()
		{
			_basicTypes = new[]
			{
				typeof(KeyGestureCommandTemplate)
			};
		}

		public T Deserialize<T>(byte[] data) where T : class
		{
			var serializer = new XmlSerializer(typeof(T), _basicTypes);
			using (var stream = new MemoryStream(data))
			{
				var value = serializer.Deserialize(stream);
				return (T)value;
			}
		}

		public byte[] Serialize(object value)
		{
			if (value != null)
			{
				var type = value.GetType();
				var serializer = new XmlSerializer(type, _basicTypes);
				var settings = new XmlWriterSettings
				{
					NewLineHandling = NewLineHandling.Entitize,
					NewLineChars = "\r\n",
					Indent = true,
					IndentChars = "\t"
				};
				using (var stream = new MemoryStream())
				{
					using (var writer = XmlWriter.Create(stream, settings))
					{
						serializer.Serialize(writer, value);
					}
					return stream.ToArray();
				}
			}

			// TODO: Serialize empty XML document
			return new byte[0];
		}

		public void Print(byte[] data)
		{
			string val = Encoding.UTF8.GetString(data);
			Console.WriteLine(val);
		}
	}
}