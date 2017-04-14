using UnityEngine;
using System.Collections;


namespace Game
{

	[RequireComponent (typeof(CanvasGroup))]
	public class Container : MonoBehaviour {

		public bool ShowInEdit;

		private CanvasGroup canvasGroup;


		public virtual void OnValidate()
		{
			if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
			canvasGroup.alpha = ShowInEdit ? 1 : 0;
		}


		public virtual void Enable()
		{

		}

	}

}
