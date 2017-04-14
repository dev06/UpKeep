using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameUtility;
using Game;
using System.IO;
namespace SystemTools
{
	/// <summary>
	/// Generic FileIO class used for parsing objects from file
	/// </summary>
	public class FileIO : MonoBehaviour {


		public static string OBJECTS;
		public static string CONSUMABLES;
		public static string WEAPONS;
		public static string MISC;


		private void InitializeDatapath()
		{
			OBJECTS = Application.dataPath + "/package/objects";
			CONSUMABLES = OBJECTS + "/consumables";
			WEAPONS = OBJECTS + "/weapons";
			MISC = OBJECTS + "/misc";
		}


		void Awake ()
		{
			InitializeDatapath();

			CreateProgramFiles();

		}


		private void CreateProgramFiles()
		{

			foreach (string folder in GetAllDirectories(OBJECTS))
			{
				foreach (string file in System.IO.Directory.GetFiles(folder))
				{
					if ((file.Contains(".dat") || file.Contains(".txt")) && !file.Contains(".meta"))
					{
						string flippedName = file.Replace(@"\", @"/");
						CreateObject( FetchObjectContents(GetFileContents(flippedName)));
					}
				}
			}
		}


		public static List<string> GetAllDirectories(string directory)
		{
			List<string> directories = new List<string>();
			foreach (string f in Directory.GetDirectories(directory, "*.*", SearchOption.AllDirectories))
			{
				directories.Add(f);
			}
			return directories;
		}

		private static List<string> GetAllFiles(string directory)
		{
			List<string> files = new List<string>();
			foreach (string f in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
			{
				if ((f.Contains(".dat") || f.Contains(".txt")) && !f.Contains(".meta"))
					files.Add(f);
			}
			return files;
		}



		public bool DoesFileExists(string path)
		{
			return File.Exists(path);
		}

		public string GetFileContents(string path)
		{
			string text = "";
			StreamReader reader = new StreamReader(path);

			while (!reader.EndOfStream)
			{
				text += reader.ReadLine() + "\n";
			}

			reader.Close( );

			return text;
		}



		private List<KeyValuePair<string, string>> FetchObjectContents(string text)
		{
			string[] lines = text.Split('\n');
			List <KeyValuePair<string, string>> pair = new List<KeyValuePair<string, string>>();
			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i] == "") { continue; }
				string[] data = lines[i].Split(':');
				pair.Add(new KeyValuePair<string, string>(data[0], data[1]));
			}
			return pair;
		}

		private void CreateObject(List<KeyValuePair<string, string>> pair)
		{
			string objectType = pair[1].Value;
			switch (objectType)
			{
				case "Consumable":
					{
						ObjectDatabase.CreateConsumableObject(pair);
						break;
					}

				case "Weapon":
					{
						ObjectDatabase.CreateWeaponObject(pair);
						break;
					}

				case "Misc":
					{
						ObjectDatabase.CreateMiscObject(pair);
						break;
					}
			}
		}
	}
}
