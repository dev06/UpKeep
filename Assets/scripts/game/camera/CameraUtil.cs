using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraUtil : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		Debug.DrawLine(transform.position, transform.position + transform.forward * 10, Color.white);
	}
}
