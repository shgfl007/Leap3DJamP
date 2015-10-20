using UnityEngine;
using System.Collections;

public class MovingCharacter : MonoBehaviour {

	private GameObject player; 

	private Vector3 direction;
	private bool isMoving = false;
	public float speed;

	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	

	void Update () {
		isMoving = WalkingGesture.isMoving;
		if (isMoving) {
			direction = WalkingGesture.moving_Direction;
			_moveCharacter();
		}
	}

	void _moveCharacter(){
		//speed is in m/s
		//Debug.Log ("moving character function called");
		//float speed = 30.0f;
		CharacterController controller = player.GetComponent<CharacterController> ();
		//transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
		Vector3 forward = transform.TransformDirection(direction);
		//float curSpeed = speed * Input.GetAxis("Vertical");
		//controller.SimpleMove(forward * curSpeed);
		direction.x = -direction.x;
		direction.y = 0f;
		direction.z = -direction.z;
		controller.Move (direction * Time.deltaTime * speed);

	}
}
