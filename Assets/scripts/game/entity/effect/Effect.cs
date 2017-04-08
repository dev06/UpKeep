using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour {

	public float life = 5;

	void Start()
	{
		transform.SetParent(GameObject.FindWithTag("Objects/Effects").transform);
	}

	void Update ()
	{
		Destroy(gameObject, life);
	}
}
