using UnityEngine;
using System.Collections;
using UI;

namespace Game
{
	[RequireComponent (typeof(Camera))]
	public class CameraController : MonoBehaviour {

		public Camera camera;
		public LayerMask detectionMask;
		private FocusedObject focusedObject;

		private float timer;
		public float xSpeed = 10f;
		public float ySpeed = 7f;
		public float amplitude = .001f;

		private Vector3 defaultPosition;
		private Vector3 bobPosition;
		void Start ()
		{
			camera = transform.GetComponent<Camera>();
			defaultPosition = transform.localPosition;
			focusedObject = FindObjectOfType<FocusedObject>();
		}

		void Update()
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, bobPosition, Time.deltaTime * 3.4f);

		}


		public void Bob(float multiplier)
		{

			timer += Time.deltaTime;

			if (timer > Mathf.PI * 2f)
			{
				timer = timer - Mathf.PI * 2f;
			}

			float xCam = Mathf.Sin(timer * xSpeed) * amplitude;
			float yCam = -Mathf.Abs(Mathf.Cos(timer * ySpeed)) * amplitude;
			bobPosition = defaultPosition + new Vector3(xCam * multiplier, yCam * multiplier, 0);
		}

		public void ResetBob()
		{
			timer = 0;
			float xCam = Mathf.Sin(timer * xSpeed) * amplitude;
			float yCam = -Mathf.Abs(Mathf.Cos(timer * ySpeed)) * amplitude;
			bobPosition = defaultPosition + new Vector3(xCam , yCam, 0);
		}





		void FixedUpdate ()
		{
			DetectFocusedObject();
		}



		private void DetectFocusedObject()
		{

			Ray ray = new Ray(transform.position, transform.forward);
			RaycastHit hitinfo;
			if (Physics.Raycast(ray.origin, transform.forward , out hitinfo, 4, detectionMask))
			{
				GameObject hitObject = hitinfo.transform.gameObject;
				ObjectIdentifier component = hitObject.GetComponent<ObjectIdentifier>();

				if (focusedObject != null)
				{
					if (component != null)
					{
						focusedObject.Notify(component);

					} else
					{
						focusedObject.Hide();
					}


				}

			} else
			{
				if (focusedObject != null)
				{
					focusedObject.Hide();
				}
			}

		}
	}

}
