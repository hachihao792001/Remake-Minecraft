using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public bool beautiful;
	public Material beautifulSkyBox, defaultSkyBox;

	public GameObject PausePage;
	public bool gamePaused=false;

	public GameObject sun;

	public Text sunText, highLightText, skyText;

	public Material[] BlockTypes;

	public GameObject Block;
	public Transform BlockParent;
	public float blockEdge;
	public int rows=3, columns=3, stacks=3;

	public enum HighLightMethods
	{
		glow,
		hitbox
	}
	public HighLightMethods highLightMethod;

	// Use this for initialization
	void Start () {

		stacks = PlayerPrefs.GetInt ("stacks");
		columns = PlayerPrefs.GetInt ("columns");
		rows = PlayerPrefs.GetInt ("rows");


		Block.transform.localScale = new Vector3 (blockEdge, blockEdge, blockEdge);
		for (int h = 0; h < stacks; h++) {
			for (int w = 0; w < columns; w++) {
				for (int l = 0; l < rows; l++) {
					Instantiate (Block, new Vector3 (w * blockEdge, h * blockEdge, l * blockEdge), Quaternion.identity, BlockParent);
				}
			}
		}
			
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.Escape)) {
			PausePage.SetActive (!PausePage.activeSelf);
		}
		gamePaused = PausePage.activeSelf;
			

		if (Input.GetKeyDown (KeyCode.L)) {
			sun.SetActive (!sun.activeSelf);
			sunText.text = "Ánh nắng (L): " + ((sun.activeSelf)?"Bật":"Tắt");
			RenderSettings.skybox.SetFloat("_Exposure", (sun.activeSelf)?0.8f:0.2f);
		}if (Input.GetKeyDown (KeyCode.H)) {
			highLightMethod = (highLightMethod == HighLightMethods.glow) ? HighLightMethods.hitbox : HighLightMethods.glow;
			highLightText.text = "Block đang chọn(H): " + ((highLightMethod == HighLightMethods.glow) ? "Sáng" : "Hitbox");

			FindObjectOfType<PlayerRaycast>().previousHit.gameObject.SendMessage ("HighLight", false, SendMessageOptions.DontRequireReceiver);
			FindObjectOfType<PlayerRaycast>().previousHit.gameObject.SendMessage ("HighLight", true, SendMessageOptions.DontRequireReceiver);

		}if (Input.GetKeyDown (KeyCode.B)) {
			beautiful = !beautiful;
			RenderSettings.skybox = (beautiful) ? beautifulSkyBox : defaultSkyBox;
			RenderSettings.skybox.SetFloat("_Exposure", (sun.activeSelf)?0.8f:0.2f);
			skyText.text = "Bầu trời (B):" + ((beautiful)?"Bật":"Tắt");
		}
	}
}
