﻿using System;
using System.IO;
using Newtonsoft.Json;

namespace ZEngine.Engine.IO
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
				//TODO: Implement
				/*
				var schemaString = GenericIOManager.LoadFileAsString(filename);
				var validatingReader = new JsonSchemaValidator();
                var schema = JsonSchema.FromJsonAsync(schemaString);
				var result = validatingReader.Validate(data, schema.Result);
                if (result.Count != 0)
                {
                    foreach (var error in result)
                    {
						Debug.LogError("Failed to validate JSON of file " + filename + ": " + error, DebugLogCategories.Engine);
					}
                }
				 loadedObject = JsonConvert.DeserializeObject<T>(data);
				*/
				throw new NotImplementedException();
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