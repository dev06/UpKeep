using UnityEngine;
using System.Collections;
using Game;

namespace GameUtility
{
	public class MasterVar
	{
		private const float constantColor = 255f;
		public static Color SkyColor = new Color(92f / constantColor, 175 / constantColor, 255f / constantColor, 1.0f);

		public static readonly Color COMMAND_EXE_COLOR = new Color(91f / constantColor, 225f / constantColor, 26f / constantColor, 1f);
		public static readonly Color COMMAND_ERROR_COLOR = new Color(255f / constantColor, 26f / constantColor, 26f / constantColor, 1f);

		public static readonly int LAYER_BUILDING = 12;
		public static readonly int LAYER_MOB = 11;
	}

}
