using UnityEngine;
using System.Collections;
namespace Game
{
	public class DayNightCycle : MonoBehaviour
	{

		public float speed = 2.0f;

		void Update ()
		{
			transform.Rotate(speed * Time.deltaTime, 0 , 0 );
		}


	}

}
