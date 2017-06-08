using System.IO;
using Newtonsoft.Json;

namespace SFML_Engine.Engine.IO
{
	public class JSONManager : GenericIOManager
	{

		public static T LoadObject<T>(string filename)
		{
			var data = LoadFileAsString(filename);
			return JsonConvert.DeserializeObject<T>(data);
		}

		public static string SaveObject<T>(string filename, T data)
		{
			var serializedData = JsonConvert.SerializeObject(data, Formatting.Indented);
			File.WriteAllText(filename, serializedData);
			return serializedData;
		}

		public static string SaveObjectUnformatted<T>(string filename, T data)
		{
			var serializedData = JsonConvert.SerializeObject(data, Formatting.None);
			File.WriteAllText(filename, serializedData);
			return serializedData;
		}

		public override T Load<T>(string path)
		{
			return LoadObject<T>(path);
		}

		public override void Save<T>(string path, T data)
		{
			SaveObject(path, data);
		}
	}
}