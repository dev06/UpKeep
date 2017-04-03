using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Game;


namespace UI
{
	public class UIPanelManager : MonoBehaviour {

		// put any panels that needs to be activated over here.
		// setting panels, credits panels etc

		private List<GameObject> 	gamePanels = new List < GameObject>();

		private GameObject pausePanel;
		private GameObject hudPanel;
		private GameObject inventoryPanel;


		public enum ActivePanel
		{
			NONE,
			PAUSE,
			INVENTORY,
			HUD,
		}


		void OnEnable()
		{
			EventManager.SetState += UpdatePanel;
		}

		void OnDisable()
		{
			EventManager.SetState -= UpdatePanel;
		}

		void Awake ()
		{

			foreach (Transform child in transform)
			{
				gamePanels.Add(child.gameObject);
			}


			pausePanel = transform.FindChild("PausePanel").gameObject;
			hudPanel = transform.FindChild("HUD").gameObject;
			inventoryPanel = transform.FindChild("Inventory").gameObject;

		}


		void Update ()
		{

		}

		/// <summary>
		/// Called everytime when the state of the game changes
		/// Used for calculating which game panel needs to be updated
		/// </summary>
		/// <param name="state"></param>
		private void UpdatePanel(StateManager.State state)
		{
			for (int i = 0; i < gamePanels.Count; i++)
			{
				SetPanelActive(gamePanels[i], false);
			}

			switch (state)
			{
				case StateManager.State.PAUSE:
				{
					SetPanelActive(pausePanel, true);
					break;
				}
				case StateManager.State.GAME:
				{
					SetPanelActive(hudPanel, true);
					break;
				}
				case StateManager.State.INVENTORY:
				{
					SetPanelActive(inventoryPanel, true);
					break;
				}
			}
		}


		/// <summary>
		/// Helper method that enables or disables a panel
		/// </summary>
		/// <param name="panel"></param>
		/// <param name="active"></param>
		private void SetPanelActive(GameObject panel, bool active)
		{

			panel.SetActive(active);
		}
	}

}
