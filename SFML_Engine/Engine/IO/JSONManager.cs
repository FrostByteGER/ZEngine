using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;

namespace SFML_Engine.Engine.IO
{
	public class JSONManager
	{

		public static T LoadObject<T>(string filename)
		{
			return LoadObject<T>(filename, false);
		}

		public static T LoadObject<T>(string filename, bool validate)
		{
			var data = GenericIOManager.LoadFileAsString(filename);

			T loadedObject;
			if (validate)
			{
				var schema = GenericIOManager.LoadFileAsString(filename);
				var dataReader = new JsonTextReader(new StringReader(data));
				var validatingReader = new JSchemaValidatingReader(dataReader);
				validatingReader.Schema = JSchema.Parse(schema);

				IList<string> messages = new List<string>();
				validatingReader.ValidationEventHandler += (o, a) => messages.Add(a.Message);
				foreach (var message in messages)
				{
					Console.WriteLine("ERROR VALIDATING JSON " + filename + ": " + message);
				}
				var serializer = new JsonSerializer();
				loadedObject = serializer.Deserialize<T>(validatingReader);
			}
			else
			{
				loadedObject = JsonConvert.DeserializeObject<T>(data);
			}
			return loadedObject;
		}

		public static string SaveObject<T>(string filename, T data)
		{
			var serializedData = JsonConvert.SerializeObject(data, Formatting.Indented);
			File.WriteAllText(filename, serializedData);
			return serializedData;
		}

		public static string SaveObject<T>(string filename, T data, JsonSerializerSettings settings)
		{
			var serializedData = JsonConvert.SerializeObject(data, Formatting.Indented, settings);
			File.WriteAllText(filename, serializedData);
			return serializedData;
		}

		public static string SaveObjectUnformatted<T>(string filename, T data)
		{
			var serializedData = JsonConvert.SerializeObject(data, Formatting.None);
			File.WriteAllText(filename, serializedData);
			return serializedData;
		}

		public static string SaveObjectUnformatted<T>(string filename, T data, JsonSerializerSettings settings)
		{
			var serializedData = JsonConvert.SerializeObject(data, Formatting.None, settings);
			File.WriteAllText(filename, serializedData);
			return serializedData;
		}
	}
}