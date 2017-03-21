using UnityEngine;
using System.Collections;
using Game;

namespace UI
{
	/// <summary>
	///  Main UI Controller
	/// </summary>
	public class UIController : MonoBehaviour {


		private void Awake ()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
		}

	}

}
