using System.IO;

namespace SFML_Engine.Engine.IO
{
	public static class GenericIOManager
	{

		public static string[] LoadFileAsArray(string path)
		{
			return File.ReadAllLines(path);
		}

		public static string LoadFileAsString(string path)
		{
			return File.ReadAllText(path);
		}

		public static void SaveFile(string path, string[] data)
		{
			File.WriteAllLines(path, data);
		}

		public static void SaveFile(string path, string data)
		{
			File.WriteAllText(path, data);
		}
	}
}