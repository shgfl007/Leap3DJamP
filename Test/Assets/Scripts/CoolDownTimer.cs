using UnityEngine;
using System.Collections;

public class CoolDownTimer : MonoBehaviour {

	public static float timeLimit = 10.0f; // 10 seconds.
	void Update()
	{
		// translate object for 10 seconds.
		if(timeLimit > 1) {
			// Decrease timeLimit.
			timeLimit -= Time.deltaTime;
			// translate backward.
			transform.Translate(Vector3.back * Time.deltaTime, Space.World);
			Debug.Log(timeLimit);
		}
	}
}
