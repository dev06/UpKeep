using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	NavMeshAgent agent;
	public Transform target;
	bool b;
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update () {

	}
}
