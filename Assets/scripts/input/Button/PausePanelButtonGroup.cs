using UnityEngine;
using System.Collections;
using Game;
using system;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UpkeepInput
{
	public class PausePanelButtonGroup : ButtonGroup {

		public enum ButtonId
		{
			RESUME,
			QUIT
		}

		public ButtonId buttonId;

		public void Start () {
			base.Start();
		}


		void Update () {

		}

		public override void OnPointerEnter(PointerEventData data)
		{
			base.OnPointerEnter(data);

		}

		public override void OnPointerExit(PointerEventData data)
		{
			base.OnPointerExit(data);

		}

		public override void OnPointerClick(PointerEventData data)
		{
			base.OnPointerClick(data);
			RegisterClick();
		}


		private void RegisterClick()
		{

			switch (buttonId)
			{
				case ButtonId.QUIT:
				{
#if UNITY_EDITOR
					UnityEditor.EditorApplication.isPlaying = false;
#else
					Application.Quit();

#endif
					break;
				}

				case ButtonId.RESUME:
				{
					StateManager.Instance.SetState(StateManager.State.GAME);
					break;
				}
			}
		}
	}

}

