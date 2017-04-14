using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;
using UpkeepInput;
namespace UI
{
	[RequireComponent (typeof(CanvasGroup))]
	[RequireComponent (typeof(Image))]
	public class FocusedObject : MonoBehaviour {

		private Image image;
		private Text text;
		private CanvasGroup canvasGroup;



		public ObjectIdentifier objectIdentifier;

		void Start ()
		{
			image =  GetComponent<Image>();
			canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.blocksRaycasts = false;
			text = transform.GetChild(0).transform.GetComponent<Text>();
			Hide();
		}

		void Update()
		{
			if (StateManager.Instance.state == StateManager.State.INVENTORY || StateManager.Instance.state == StateManager.State.PAUSE)
			{
				Hide();
			}
		}


		public void Notify(ObjectIdentifier objectIdentifier )
		{
			this.objectIdentifier = objectIdentifier;
			canvasGroup.alpha = 1;
			text.text = "Press [" +  GameInputManager.PICKUPITEM_KEYCODE  +  "] to pickup " + objectIdentifier.GetObject().objectName;
		}


		public void Hide()
		{
			this.objectIdentifier = null;
			canvasGroup.alpha = 0;
			text.text = "";
		}

	}

}
