using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SFML_Engine.Engine.IO
{
	public class Config
	{
		private readonly Dictionary<string, string> _configData = new Dictionary<string, string>();

		public ReadOnlyDictionary<string, string> ConfigData => new ReadOnlyDictionary<string, string>(_configData);

		public Config()
		{
		}

		public Config(Dictionary<string, string> configData)
		{
			_configData = configData;
		}

		public bool ContainsConfigEntry(string configKey)
		{
			return _configData.ContainsKey(configKey);
		}

		public void AddConfigEntry(string configKey, string configValue)
		{
			_configData.Add(configKey, configValue);
		}

		public string GetConfigValue(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return configValue;
		}

		public long GetConfigValueLong(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToInt64(configValue);
		}

		public ulong GetConfigValueULong(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToUInt64(configValue);
		}

		public int GetConfigValueInt(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToInt32(configValue);
		}

		public uint GetConfigValueUInt(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToUInt32(configValue);
		}

		public short GetConfigValueShort(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToInt16(configValue);
		}

		public ushort GetConfigValueUShort(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToUInt16(configValue);
		}

		public float GetConfigValueFloat(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToSingle(configValue);
		}

		public double GetConfigValueDouble(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToDouble(configValue);
		}

		public bool GetConfigValueBool(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToBoolean(configValue);
		}

		public byte GetConfigValueByte(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToByte(configValue);
		}

		public sbyte GetConfigValueSByte(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return Convert.ToSByte(configValue);
		}

		public char GetConfigValueChar(string configKey)
		{
			string configValue = null;
			_configData.TryGetValue(configKey, out configValue);
			return configValue == null ? char.MinValue : Convert.ToChar(configValue);
		}

		public void SetConfigValue(string configKey, string configValue)
		{
			_configData[configKey] = configValue;
		}

		public void RemoveConfigEntry(string configKey)
		{
			_configData.Remove(configKey);
		}
	}
}