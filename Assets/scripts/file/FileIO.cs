using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using GameUtility;
using Game;
namespace SystemTools
{
	/// <summary>
	/// Generic FileIO class used for parsing objects from file
	/// </summary>
	public class FileIO : MonoBehaviour {


		public static string OBJECTS;
		public static string CONSUMABLES;
		public static string WEAPONS;


		private void InitializeDatapath()
		{
			OBJECTS = Application.dataPath + "/package/objects";
			CONSUMABLES = OBJECTS + "/consumables";
			WEAPONS = OBJECTS + "/weapons";
		}


		void Awake ()
		{
			InitializeDatapath();
			CreateObject( FetchObjectContents(GetFileContents(CONSUMABLES + "/snackbar/program.dat")));
			CreateObject( FetchObjectContents(GetFileContents(CONSUMABLES + "/soda/program.dat")));
			CreateObject( FetchObjectContents(GetFileContents(CONSUMABLES + "/healthpack/program.dat")));
			CreateObject( FetchObjectContents(GetFileContents(WEAPONS + "/pistol/program.dat")));
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
			}
		}
	}
}
