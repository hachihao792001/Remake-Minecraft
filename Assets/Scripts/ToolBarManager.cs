using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBarManager : MonoBehaviour {

	public int currentSlot;
	public Image[] slotImages;
	public Color normal, choosing;

	// Use this for initialization
	void Start () {
		ChangeSlotTo (1);	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Alpha1))
			ChangeSlotTo (1);
		if (Input.GetKeyDown (KeyCode.Alpha2))
			ChangeSlotTo (2);
		if (Input.GetKeyDown (KeyCode.Alpha3))
			ChangeSlotTo (3);
		if (Input.GetKeyDown (KeyCode.Alpha4))
			ChangeSlotTo (4);
		if (Input.GetKeyDown (KeyCode.Alpha5))
			ChangeSlotTo (5);
		if (Input.GetKeyDown (KeyCode.Alpha6))
			ChangeSlotTo (6);
		if (Input.GetKeyDown (KeyCode.Alpha7))
			ChangeSlotTo (7);
		if (Input.GetKeyDown (KeyCode.Alpha8))
			ChangeSlotTo (8);
		if (Input.GetKeyDown (KeyCode.Alpha9))
			ChangeSlotTo (9);
	}

	void ChangeSlotTo(int slot){
		currentSlot = slot;
		for (int i = 0; i < slotImages.Length; i++) {
			if (i == slot - 1)
				slotImages [i].color = choosing;
			else
				slotImages [i].color = normal;
		}
	}
}
