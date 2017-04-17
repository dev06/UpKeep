using UnityEngine;
using System.Collections;

public class holdState : StateMachineBehaviour {


	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{

		Debug.Log("Entered");
	}

}
