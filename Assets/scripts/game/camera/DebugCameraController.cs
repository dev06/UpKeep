using UnityEngine;
using System.Collections;
using UpkeepInput;
namespace Game
{
	[RequireComponent (typeof(Camera))]
	public class DebugCameraController : MonoBehaviour {

		GameInputManager gameInputManager;

		private Vector2 lookRotation = Vector2.zero;
		private float walkingSpeed = 7;
		private float sprintingSpeed = 15;
		private float boostSpeed = 55;
		private float speed;
		private float rotationX;
		private float rotationY;
		private float rotationZ;
		private float clampMin = -85f;
		private float clampMax = 85f;

		private float mouseXSensitvity = 3f;
		private float mouseYSensitvity = 3f;
		private Vector3 position;
		private Quaternion rotation;

		Camera camera;

		void Start ()
		{
			gameInputManager = GameInputManager.Instance;
			camera = GetComponent<Camera>();
			camera.enabled = true;

			transform.parent.rotation = Quaternion.Euler(Vector3.zero);



		}


		void Update ()
		{
			camera.enabled = DebugController.DEBUG_MODE;
			if (!DebugController.DEBUG_MODE)
			{
				Destroy(transform.parent.gameObject);
				return;
			}
			MoveCamera();
		}


		private void MoveCamera()
		{
			Vector3 movement = transform.forward;
			speed = gameInputManager.GetKey(GameInputManager.SPRINT_KEYCODE) ? sprintingSpeed : gameInputManager.GetKey(GameInputManager.BOOST_KEYCODE) ? boostSpeed : walkingSpeed;

			if (gameInputManager.GetKey(KeyCode.W))
			{
				movement = transform.forward;	transform.parent.Translate(movement * Time.deltaTime * speed, Space.World);

			}  if (gameInputManager.GetKey(KeyCode.D))
			{
				movement = transform.right;	transform.parent.Translate(movement * Time.deltaTime * speed, Space.World);

			}
			if (gameInputManager.GetKey(KeyCode.A))
			{
				movement = -transform.right;	transform.parent.Translate(movement * Time.deltaTime * speed, Space.World);

			}
			if (gameInputManager.GetKey(KeyCode.S))
			{
				movement = -transform.forward;	transform.parent.Translate(movement * Time.deltaTime * speed, Space.World);
			}


			rotationZ = gameInputManager.GetKey(KeyCode.Q) ? rotationZ + Time.deltaTime * mouseXSensitvity : gameInputManager.GetKey(KeyCode.E) ? rotationZ - Time.deltaTime * mouseXSensitvity : 0  ;


			Vector2 r = GetLookRotation();
			transform.localRotation = Quaternion.Euler(r.y, 0, 0);
			transform.parent.Rotate(0, r.x, 0);

		}


		public Vector2 GetLookRotation()
		{

			rotationX = gameInputManager.input.look.x * mouseXSensitvity;
			rotationY -= gameInputManager.input.look.y * mouseYSensitvity;
			rotationY = Mathf.Clamp(rotationY, clampMin, clampMax);
			lookRotation.x = rotationX;
			lookRotation.y = rotationY;
			return lookRotation;
		}




	}

}
