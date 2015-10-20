using UnityEngine;
using System.Collections;
using Leap;

public class Fist : MonoBehaviour {

	public bool isLeftHanded = false;

	Controller _controller;

	public float radius_threshold = 30f;

	private bool isFist = false;

	private Vector handSpeed = Vector.Zero;

	private bool isAttacking = false;
	public static bool IS_ATTACKING = false; 
	public float _zThreshold = 70f;
	void Start () {
		//leap_controller = Leap.Controller;
		_controller = new Controller ();

	}


	
	// Update is called once per frame
	void Update () {

		Frame frame = _controller.Frame();
		HandList _handlist = frame.Hands;
		if (_handlist.Count == 0)
			return;
		Hand leftHand = null; Hand rightHand = null;
		for (int i = 0; i < _handlist.Count; i++) {
			//*** may add a if statement to check if the hand is valid
			//check and assign left or right hand  
			if(_handlist[i].IsLeft)
				leftHand = _handlist[i];
			else if(_handlist[i].IsRight)
				rightHand = _handlist[i];
		}

		//get the radius of the hand sphere
		float radius = -1f;
		if (isLeftHanded && leftHand != null)
			radius = leftHand.SphereRadius;
		else if (rightHand != null)
			radius = rightHand.SphereRadius;

		if (radius <= radius_threshold && radius != -1f) {
			Debug.Log ("fist detected");
			//Debug.Log ("radius is " + radius.ToString ());
			isFist = true;
		} else 
			isFist = false;

		//check the attacking gesture 
		if (isFist) {
			//check the speed of the hand
			//if the velocity is greater than the pre set threshold, it is an attack
			handSpeed = rightHand.PalmVelocity;
			//Debug.Log("speed is " + handSpeed.ToString());

			if (handSpeed.z >= _zThreshold) {
				Debug.Log ("Attacking");
				isAttacking = true;
			} else
				isAttacking = false;
		} else
			isAttacking = false;

		IS_ATTACKING = isAttacking;

//		GestureList gesture_list = frame.Gestures();
//		for (int i = 0; i < gesture_list.Count; i++) {
//			Gesture gesture = gesture_list[i];
//			if(gesture.Type == Gesture.GestureType.TYPE_SWIPE)
//			{
//				//Debug.Log("Swipe detected");
//				if(isFist)
//					Debug.Log("hit");
//
//			}
//		}
	
	}
}
