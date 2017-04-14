using UnityEngine;
using System.Collections;
using UI;
using GameUtility;
namespace Game
{
	public class DebugController : MonoBehaviour {

		public static bool DEBUG_MODE = false;
		public static string[] CommandList =
		{
			"set speed ",
			"set health ",
			"set jumpingHeight ",
			"set gravity ",
			"set debug ",
			"set time ",
			"set daySpeed ",
			"spawn item ",
		};

		Player player;

		DayNightCycle dayNightCycle;

		DebugPanelHandler debugPanelHandler;

		StateManager stateManager;

		string command;

		void Start()
		{
			StartCoroutine("FindHandler");

			stateManager = StateManager.Instance;

			player = FindObjectOfType<Player>();

			dayNightCycle = FindObjectOfType<DayNightCycle>();

			player.movementController.FreezeMovement(DEBUG_MODE);
			player.lookController.FreezeRotation(DEBUG_MODE);
		}

		void Update ()
		{
			if (debugPanelHandler == null || !stateManager.IsState(StateManager.State.DEBUG)) return;
		}

		private IEnumerator FindHandler()
		{
			int delay = 1;
			while (debugPanelHandler == null)
			{
				debugPanelHandler = FindObjectOfType<DebugPanelHandler>();

				yield return new WaitForSeconds(delay);
			}

			debugPanelHandler.SetDebugController(this);

		}


		public void FetchCommand(string command)
		{
			this.command = command;

			ParseCommand();
			if (player == null) player = FindObjectOfType<Player>();
		}

		private void ParseCommand()
		{


			bool foundCommand = false;

			try
			{
				for (int i = 0; i < CommandList.Length; i++)
				{
					if (command.Contains(CommandList[i]))
					{

						foundCommand = true;
						string[] data = command.Split(' ');


						switch (CommandList[i])
						{
							case "set speed ":
							{
								float speed = float.Parse(data[2]);
								player.movementController.SetWalkingSpeed(speed);
								debugPanelHandler.DisplayErrorText("Set walking speed to " + speed, MasterVar.COMMAND_EXE_COLOR);
								break;
							}

							case "set jumpingHeight ":
							{
								float height = float.Parse(data[2]);
								player.movementController.SetJumpingHeight(height);
								debugPanelHandler.DisplayErrorText("Set jumping Height to " + height, MasterVar.COMMAND_EXE_COLOR);
								break;
							}

							case "set gravity ":
							{
								float gravity = float.Parse(data[2]);
								player.movementController.SetGravity(gravity);
								debugPanelHandler.DisplayErrorText("Set gravity to " + gravity, MasterVar.COMMAND_EXE_COLOR);
								break;
							}

							case "set debug ":
							{
								float option = float.Parse(data[2]);
								DEBUG_MODE = option == 1 ? true : false;
								player.movementController.FreezeMovement(DEBUG_MODE);
								player.lookController.FreezeRotation(DEBUG_MODE);
								if (DEBUG_MODE)
								{
									Instantiate(GameResource.DebugCamera, player.cameraController.transform.position, player.cameraController.transform.rotation);
								}

								debugPanelHandler.DisplayErrorText("Debug Mode " + (DEBUG_MODE ? "enabled " : "disabled"), MasterVar.COMMAND_EXE_COLOR);
								break;
							}

							case "set time ":
							{
								float time = float.Parse(data[2]);
								dayNightCycle.SetTime(time);
								debugPanelHandler.DisplayErrorText("Set Time to " + time, MasterVar.COMMAND_EXE_COLOR);
								break;
							}

							case "set daySpeed ":
							{
								float time = float.Parse(data[2]);
								dayNightCycle.SetSpeed(time);
								debugPanelHandler.DisplayErrorText("Set Day speed to " + time, MasterVar.COMMAND_EXE_COLOR);
								break;
							}

							case "set health ":
							{
								float health = float.Parse(data[2]);
								player.vitalController.SetHealth(health);
								debugPanelHandler.DisplayErrorText("Set Health to " + health, MasterVar.COMMAND_EXE_COLOR);
								break;

							}

							case "spawn item ":
							{
								int objectId = int.Parse(data[2]);
								int quantity = 1;
								if (data.Length > 2)
								{
									quantity = int.Parse(data[3]);
								}
								for (int q = 0; q < quantity; q++)
								{
									ObjectSpawnerController.SpawnObjectRelativeTo(objectId, player.transform);
								}
								debugPanelHandler.DisplayErrorText("Item spawned ", MasterVar.COMMAND_EXE_COLOR);
								break;

							}
						}
					}
				}
			} catch (System.Exception e)
			{

			}


			if (foundCommand == false)
			{
				debugPanelHandler.DisplayErrorText("Could not find the command. Try Again." , MasterVar.COMMAND_ERROR_COLOR);
			}
		}
	}
}
