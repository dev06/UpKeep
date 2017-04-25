using UnityEngine;
using System.Collections;

public class gizmo : MonoBehaviour {

	[System.Serializable]
	public class GizmoTransform
	{
		public Vector3 pos;
		public Vector3 scale;
	}

	public GizmoTransform gt;

	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(gt.pos, gt.scale);
	}
}
