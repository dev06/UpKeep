using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent (typeof(Image))]
public class InventoryOutlineHandler : MonoBehaviour {

	public Color color;

	private Image image;
	void Start ()
	{
		image = GetComponent<Image>();
		image.color = color;
	}


	void OnValidate()
	{
		if (image == null) 	image = GetComponent<Image>();
		image.color = color;

	}
}
