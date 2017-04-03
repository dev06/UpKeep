using UnityEngine;
using System.Collections;
using System.IO;
namespace SystemTools
{
	public class FileIO : MonoBehaviour {


		public static string OBJECTS = Application.dataPath + "/package/objects";
		public static string CONSUMABLES = OBJECTS + "/consumables";


		void Start ()
		{

			Debug.Log(GetFileContents(CONSUMABLES + "/snackbar/program.dat"));
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
	}

}
