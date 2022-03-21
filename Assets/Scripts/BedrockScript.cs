using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedrockScript : MonoBehaviour {
	public float bounceForce;

	void OnCollisionEnter(Collision collision){
		collision.collider.gameObject.GetComponent<Rigidbody> ().AddForce (Vector3.up * bounceForce);
		StartCoroutine (FlashAvatar ());
	}

	IEnumerator FlashAvatar(){
		GetComponent<MeshRenderer> ().enabled = true;
		yield return new WaitForSeconds (3f);
		GetComponent<MeshRenderer> ().enabled = false;
	}
}
