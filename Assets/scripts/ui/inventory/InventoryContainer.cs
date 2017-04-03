using UnityEngine;
using System.Collections;
using Game;
namespace UI
{
	public class InventoryContainer : MonoBehaviour {

		private GameObject mainContainer;
		private GameObject itemContainer;
		private GameObject descriptionContainer;
		private GameObject characterContainer;
		private GameObject actionContainer;

		void Start ()
		{
			mainContainer = transform.GetChild(0).gameObject;
			itemContainer = mainContainer.transform.GetChild(0).gameObject;
			descriptionContainer = mainContainer.transform.GetChild(1).gameObject;
			characterContainer = mainContainer.transform.GetChild(2).gameObject;
			actionContainer = mainContainer.transform.GetChild(3).gameObject;
		}
	}
}
