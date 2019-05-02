using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;

namespace TheGreatPatrioticWar
{
	class Settings
	{
		public const string SettingsFile = "Settings.ini";
		public const char Delimiter = '=';

		public static Settings Current { get; private set; } = new Settings();

		//--------Settings--------//


		//Server settings
		public int windowWidth = 1920;
		public int windowHeight = 1000;



		//------------------------//

		private Settings() { }

		public static void LoadFromFile()
		{
			//Create the file if it does not exist 
			if (!File.Exists(SettingsFile)) SaveToFile();

			var sType = typeof(Settings);
			var invariant = CultureInfo.InvariantCulture;

			//Create a new settings object
			Settings nSettings = new Settings();

			string[] lines = File.ReadAllLines(SettingsFile);

			for (int i = 0; i < lines.Length; ++i)
			{
				var lineNumber = i + 1;

				//Identifier + value
				string[] split = lines[i].Split(Delimiter);
				string id = split.First(), val = split.Last();

				//Check if there is more than one delimiter 
				if (split.Length != 2) throw new FileLoadException($@"{SettingsFile} - {lines[i]}: Unnecessary '{Delimiter}' (line {lineNumber})");

				//Cast string to field type
				try
				{
					switch (sType.GetField(id).FieldType.Name)
					{
						case "Boolean": sType.GetField(id).SetValue(nSettings, bool.Parse(val)); break;
						case "Int32": sType.GetField(id).SetValue(nSettings, int.Parse(val, invariant)); break;
						case "Int64": sType.GetField(id).SetValue(nSettings, long.Parse(val, invariant)); break;
						case "Single": sType.GetField(id).SetValue(nSettings, float.Parse(val, invariant)); break;
						case "Double": sType.GetField(id).SetValue(nSettings, double.Parse(val, invariant)); break;

						default: sType.GetField(id).SetValue(nSettings, val); break;
					}
				}
				catch (FormatException)
				{
					throw new FileLoadException($@"{SettingsFile} - {lines[i]}: Could not cast '{val}' to {sType.GetField(id).FieldType.Name} (line {lineNumber})");
				}
				catch (ArgumentException)
				{
					throw new FileLoadException($@"{SettingsFile} - {lines[i]}: Invalid line (line {lineNumber})");
				}

				Current = nSettings;
			}
		}

		public static void SaveToFile()
		{
			var fields = typeof(Settings).GetFields(BindingFlags.Public | BindingFlags.Instance);
			File.WriteAllLines(SettingsFile, fields.Select(x => $"{x.Name}{Delimiter}{x.GetValue(Current)}").ToArray());
		}
	}
}
