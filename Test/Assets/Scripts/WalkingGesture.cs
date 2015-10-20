using UnityEngine;
using System.Collections;
using Leap;

public class WalkingGesture : MonoBehaviour {

	Controller _controller;

	public bool isLeftHanded = false;

	public static Vector Direction = Vector.Zero;

	public float Z_THRESHOLD_MIN = -0.4f;
	public float Z_THRESHOLD_MAX = -0.2f;
	public static bool isMoving = false;
	public static Vector3 moving_Direction = Vector3.zero;
	public float Angle_Threshold = 22.5f;

	//-------------------------------
	private Vector3 Pre_Direction = Vector3.zero;
	private Vector3 Temp_Direction = Vector3.zero;

	void Start () {
		_controller = new Controller ();
	}
	
	void Update () {

		//store the frame object
		Frame frame = _controller.Frame ();

		//check the hand list
		HandList _handlist = frame.Hands;
		if (_handlist.Count == 0) {
			isMoving = false;
			return;
		}

		//Save left and right hand
		Hand leftHand = null; Hand rightHand = null;
		for (int i = 0; i < _handlist.Count; i++) {
			//*** may add a if statement to check if the hand is valid
			//check and assign left or right hand  
			if(_handlist[i].IsLeft)
				leftHand = _handlist[i];
			else if(_handlist[i].IsRight)
				rightHand = _handlist[i];
		}

		//using right hand as the controller for the player
		if (rightHand != null) {
			FingerList fingers = rightHand.Fingers;
			Finger Index_Finger = new Finger ();
			for (int i = 0; i < fingers.Count; i++) {
				//Debug.Log(fingers[i].Type);
				if (fingers [i].Type.Equals (Finger.FingerType.TYPE_INDEX)) {
					//Debug.Log("index finger found!");
					Index_Finger = fingers [i];
				}
			}
			//finger index: 
			//TYPE_THUMB = = 0 -
//			TYPE_INDEX = = 1 -
//			TYPE_MIDDLE = = 2 -
//			TYPE_RING = = 3 -
//			TYPE_PINKY = = 4 - 



			//store the middle finger
			Finger Middle_Finger = rightHand.Finger (2);

			//the walking gesture: index finger pointing forward
			if (Index_Finger.IsValid) {
				//Debug.Log ("index finger direction is " + Index_Finger.Direction.ToString ());

				Temp_Direction = Index_Finger.Direction.ToUnity();

				if(Pre_Direction != Vector3.zero)
				{
					//if this is not the first time, compare the angle between the two vectors
					float angle = Vector3.Angle(Pre_Direction,Temp_Direction);
					if(angle > Angle_Threshold)
					{
						Debug.Log("angle bigger than threshold! Angle is " + angle.ToString());
						moving_Direction = Temp_Direction;
						isMoving = true;
					}else if (angle < Angle_Threshold)
					{
						moving_Direction = Pre_Direction;
						isMoving = true;
					}else
						isMoving = false;

				}else
				{
					//the first time
					moving_Direction = Temp_Direction;
					isMoving = true;
				}

				Pre_Direction = Temp_Direction;

				//moving_Direction = Index_Finger.Direction.ToUnity ();

			} else
				isMoving = false;
		}


	}
}
