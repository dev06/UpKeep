using UnityEngine;
using System.Collections;
using UI;
using UnityStandardAssets.ImageEffects;
namespace Game
{
	[RequireComponent (typeof(Camera))]
	public class CameraController : MonoBehaviour
	{

		private Player player;
		public Camera camera;
		public LayerMask detectionMask;
		private FocusedObject focusedObject;

		private float timer;
		public float xSpeed = 10f;
		public float ySpeed = 7f;
		public float amplitude = .001f;

		private Vector3 defaultPosition;
		private Vector3 bobPosition;


		private float recoilValue;
		private float recoil;
		private float speed ;
		private float fowardMultiplier;
		private float headRotateMultiplier;
		private float acceleration;

		private BlurOptimized blur;

		public void Initialize()
		{
			camera = transform.GetComponent<Camera>();
			blur = GetComponent<BlurOptimized>();
			defaultPosition = transform.localPosition;
			focusedObject = FindObjectOfType<FocusedObject>();

		}

		void Update()
		{
			//camera.enabled = !DebugController.DEBUG_MODE;

			if (player == null)
			{

				return;
			}

			UpdateCameraTransform();
			blur.enabled = player.stateManager.IsState(StateManager.State.DEBUG);
			UpdateCameraRecoil();
		}

		public void SetPlayer(Player player)
		{
			this.player = player;
		}

		private void UpdateCameraTransform()
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition, bobPosition + Vector3.forward * recoilValue * fowardMultiplier, Time.deltaTime * 3.4f);
			transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new Vector3(-recoilValue * headRotateMultiplier , 0, 0)), Time.deltaTime * speed);
		}

		private void UpdateCameraRecoil()
		{
			recoilValue = recoilValue > 0 ? recoilValue - (Time.deltaTime * speed) : 0;
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

		public void TriggerRecoil()
		{

			recoilValue = recoil;
		}


		public void SetRecoilWeaponValue(Weapon w)
		{
			float reset = 0;
			if (w != null)
			{
				recoil = w.recoil;
				speed = w.recoilSpeed;
				fowardMultiplier = w.forwardRecoilMult;
				headRotateMultiplier = w.upwardRecoilMult;

			} else
			{
				recoil = reset;
				speed = reset;
				fowardMultiplier = reset;
				headRotateMultiplier = reset;

			}

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
