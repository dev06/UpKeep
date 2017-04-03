using UnityEngine;
using System.Collections;
using Game;
using system;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UpkeepInput
{
	[RequireComponent (typeof(Image))]
	public class ButtonGroup :  MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
	{

		private Image image;

		public Color hoverColor = new Color(1, 1, 1, 1);
		public Color restColor = new Color(1, 1, 1, 1);
		private Color imageColor;
		public bool  useImageColor;


		public void Start ()
		{

			Initialize();
		}

		private void Initialize()
		{
			image = GetComponent<Image>();
			imageColor = image.color;
			if (useImageColor)
			{
				restColor = imageColor;
			}
		}

		public virtual void OnPointerEnter(PointerEventData data)
		{
			image.color = hoverColor;
		}

		public virtual void OnPointerExit(PointerEventData data)
		{
			image.color = restColor;
		}

		public virtual void OnPointerClick(PointerEventData data)
		{

		}

		public virtual void RegisterClick()
		{

		}
	}

}
