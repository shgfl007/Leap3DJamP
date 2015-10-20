using UnityEngine;
using System.Collections;

public class changeMaterialOnAttack : MonoBehaviour {

	public Material normalMaterial;
	public Material onAttackMaterial;

	private bool changeMaterial;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer>().material = normalMaterial;
	}
	
	// Update is called once per frame
	void Update () {

		changeMaterial = Fist.IS_ATTACKING;

		if (changeMaterial)
			gameObject.GetComponent<Renderer>().material = onAttackMaterial;
		else
			gameObject.GetComponent<Renderer>().material = normalMaterial;
	}
}
