using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceScript : MonoBehaviour {

	[SerializeField]
	Vector3 newBlockRelativePos;

	public float whiteTensity;
	ToolBarManager toolBar;
	GameManager gameManager;
	Color c;
	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		toolBar = FindObjectOfType<ToolBarManager> ();
		c = GetComponent<MeshRenderer> ().material.color;

		switch (gameObject.name) {
		case "u":
			newBlockRelativePos = Vector3.up * gameManager.blockEdge;
			break;
		case "d":
			newBlockRelativePos = Vector3.down * gameManager.blockEdge;
			break;
		case "l":
			newBlockRelativePos = Vector3.left * gameManager.blockEdge;
			break;
		case "r":
			newBlockRelativePos = Vector3.right * gameManager.blockEdge;
			break;
		case "f":
			newBlockRelativePos = Vector3.forward * gameManager.blockEdge;
			break;
		case "b":
			newBlockRelativePos = Vector3.back * gameManager.blockEdge;
			break;
		}
	}

	void BeingLeftClicked(){Destroy ();}
	void Destroy(){
		GameObject.Find ("Hitbox").transform.position = Vector3.one * 100;
		transform.parent.gameObject.SendMessage ("DestroyThisBlock");
	}

	void BeingRightClicked(){
		PlaceBlock (transform.parent.position + newBlockRelativePos);
	}

	void PlaceBlock(Vector3 blockPos){
		if (!(Physics.OverlapBox (blockPos, Vector3.one * (gameManager.blockEdge / 2 - 0.1f)).Length > 0)) {
			GameObject block = Instantiate (gameManager.Block, blockPos, Quaternion.identity, gameManager.BlockParent);
			HighLight (false);

			for (int i = 0; i < 6; i++) {
				block.transform.GetChild(i).GetComponent<MeshRenderer> ().material = gameManager.BlockTypes [toolBar.currentSlot - 1];
			}
			transform.parent.gameObject.SendMessage ("TestCollideWithOtherBlock");
		}
	}

	/*
	bool canHighLight;
	void HighLight(){
		canHighLight = true;
	}

	void OnMouseOver(){
		if (canHighLight) {
			if(gameManager.highLightMethod == GameManager.HighLightMethods.hitbox)
				GameObject.Find("Hitbox").transform.position = transform.parent.position;
			else 
				GetComponent<MeshRenderer> ().material.color = new Color (c.r + whiteTensity, c.g + whiteTensity, c.b + whiteTensity);
		}
	}
	void OnMouseExit(){
		GameObject.Find ("Hitbox").transform.position = Vector3.one * 100;
		GetComponent<MeshRenderer> ().material.color = Color.white;
		canHighLight = false;
	}
	*/

	void HighLight(bool on){
		if (on) {
			if (gameManager.highLightMethod == GameManager.HighLightMethods.hitbox)
				GameObject.Find ("Hitbox").transform.position = transform.parent.position;
			else {
				for(int i=0; i<6; i++)
					transform.parent.GetChild(i).gameObject.GetComponent<MeshRenderer> ().material.color = new Color (c.r + whiteTensity, c.g + whiteTensity, c.b + whiteTensity);
			}
		} else {
			GameObject.Find ("Hitbox").transform.position = Vector3.one * 100;
			for(int i=0; i<6; i++)
				transform.parent.GetChild(i).gameObject.GetComponent<MeshRenderer> ().material.color = Color.white;
			
		}
	}
}
