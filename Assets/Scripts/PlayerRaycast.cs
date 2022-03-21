using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRaycast : MonoBehaviour {

	Rigidbody rb;
	public float rayDistance;
	public Collider previousHit;

	void Start(){
		rb = transform.parent.gameObject.GetComponent<Rigidbody>();
	}

	void Update () {

		if (FindObjectOfType<GameManager>().gamePaused) {
			rb.isKinematic = true;
			return;
		}
		rb.isKinematic = false;

		//Left click and right lick on Blocks
		RaycastHit rc;
		if (Physics.Raycast (transform.position, transform.forward, out rc, rayDistance)) {
			Collider hitted = rc.collider;

			if (previousHit == null) {
				previousHit = hitted;
				hitted.gameObject.SendMessage ("HighLight", true, SendMessageOptions.DontRequireReceiver);
			}

			if (previousHit != hitted) {
				previousHit.gameObject.SendMessage ("HighLight", false, SendMessageOptions.DontRequireReceiver);
				previousHit = hitted;
				hitted.gameObject.SendMessage ("HighLight", true, SendMessageOptions.DontRequireReceiver);
			}



			if (Input.GetMouseButtonDown (0)) {
				hitted.gameObject.SendMessage ("BeingLeftClicked", SendMessageOptions.DontRequireReceiver);
			} else if (Input.GetMouseButtonDown (1)) {
				hitted.gameObject.SendMessage ("BeingRightClicked", SendMessageOptions.DontRequireReceiver);
			}
		} else {
			if(previousHit !=null)
				previousHit.gameObject.SendMessage ("HighLight", false, SendMessageOptions.DontRequireReceiver);
		}
	}
}
