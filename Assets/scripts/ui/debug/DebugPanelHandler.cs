using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Game;

namespace UI
{
	public class DebugPanelHandler : Container {

		private Color errorTextDefaultColor;
		private Text errorText;
		private InputField inputField;
		private DebugController debugController;
		private string command;

		void OnEnable()
		{

		}

		void Start ()
		{
			inputField = transform.GetChild(0).GetComponent<InputField>();
			errorText = transform.GetChild(1).GetComponent<Text>();
			inputField.Select();
			inputField.onValueChange.AddListener(delegate {ValueChangeCheck(); });
			errorTextDefaultColor = errorText.color;
			errorText.color = new Color(0, 0, 0, 0);
		}

		void Update()
		{
			if (debugController == null) return;
			if (Input.GetKeyDown(KeyCode.Return))
			{
				debugController.FetchCommand(command);
				inputField.text = command = "";
				inputField.Select();
				inputField.ActivateInputField();
			}
		}


		public void ValueChangeCheck()
		{
			if (inputField == null) return;

			command = inputField.text;

			if (debugController == null) return;
		}


		public void SetDebugController(DebugController controller)
		{
			this.debugController = controller;
		}

		public IEnumerator TriggerErrorText(string text, Color c)
		{
			float alpha = 1;
			errorText.text = text;
			Color color = new Color( c.r,  c.g,  c.b, alpha);
			errorText.color = new Color( c.r,  c.g,  c.b, 1);
			while (alpha > 0)
			{

				alpha -= Time.deltaTime;
				color.a = alpha;
				errorText.color = color;
				yield return new WaitForSeconds(Time.deltaTime);
			}

			errorText.color = new Color( c.r,  c.g,  c.b, 0);
		}

		public void DisplayErrorText(string text, Color c)
		{
			StopCoroutine(TriggerErrorText(text, c));
			StartCoroutine(TriggerErrorText(text,  c));
			errorText.color = new Color( c.r,  c.g,  c.b, 1);
		}

		public override void OnValidate()
		{
			base.OnValidate();
		}

		public override void Enable()

		{
			if (inputField != null)
			{
				inputField.text = command = "";
				inputField.Select();
				inputField.ActivateInputField();
			}

			if (errorText != null)
			{
				errorText.color = new Color(0, 0, 0, 0);
			}
		}
	}

}
