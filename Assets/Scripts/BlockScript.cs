using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockScript : MonoBehaviour {

	public GameObject hitBox;
	GameManager gameManager;
	public bool[] blockFacesCollided;

	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		TestCollideWithOtherBlock ();
	}

	void TestCollideWithOtherBlock(){
		Collider[] cols = Physics.OverlapBox (transform.position, Vector3.one * gameManager.blockEdge);
		//u,d,r,l,f,b
		blockFacesCollided = new bool[6]{false,false,false,false,false,false};

		if (cols.Length > 0) {
			for (int i = 0; i < cols.Length; i++) {
				if (cols [i].gameObject.tag == "Block") {
					if (cols [i].transform.GetChild(0).gameObject.GetComponent<MeshRenderer> ().material.name == "8Glass")
						Debug.Log ("is glass");
					Vector3 currentTestingBlockPos = cols [i].transform.position;
					Vector3 thisBlockPos = transform.position;
					float edge = gameManager.blockEdge;

					if (currentTestingBlockPos == thisBlockPos + Vector3.up * edge) 
						blockFacesCollided [0] = true;
					else if (currentTestingBlockPos == thisBlockPos + Vector3.down * edge) 
						blockFacesCollided [1] = true;
					else if (currentTestingBlockPos == thisBlockPos + Vector3.right * edge) 
						blockFacesCollided [2] = true;
					else if (currentTestingBlockPos == thisBlockPos + Vector3.left * edge) 
						blockFacesCollided [3] = true;
					else if (currentTestingBlockPos == thisBlockPos + Vector3.forward * edge)
						blockFacesCollided [4] = true;
					else if (currentTestingBlockPos == thisBlockPos + Vector3.back * edge) 
						blockFacesCollided [5] = true;
					
				}
			}

			transform.Find ("u").gameObject.SetActive (!blockFacesCollided[0]);
			transform.Find ("d").gameObject.SetActive (!blockFacesCollided[1]);
			transform.Find ("r").gameObject.SetActive (!blockFacesCollided[2]);
			transform.Find ("l").gameObject.SetActive (!blockFacesCollided[3]);
			transform.Find ("f").gameObject.SetActive (!blockFacesCollided[4]);
			transform.Find ("b").gameObject.SetActive (!blockFacesCollided[5]);


		}else {
			for (int i = 0; i < 6; i++) 
				transform.GetChild (i).gameObject.SetActive (true);
		}
	}

	void DestroyThisBlock(){
		GetComponent<Collider> ().enabled = false;
		Collider[] cols = Physics.OverlapBox (transform.position, Vector3.one * gameManager.blockEdge);
		for (int i = 0; i < cols.Length; i++) {
			cols [i].gameObject.SendMessage ("TestCollideWithOtherBlock", SendMessageOptions.DontRequireReceiver);
		}

		Destroy (gameObject);
	}
}
