using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	public Text rowText, columnText, stackText;
	public int rowInput, columnInput, stackInput;
	public float rotateSpeed;
	public Transform theCamera;
	public GameObject errorText;
	public GameObject menu,loadingImage;

	void Start(){
		RenderSettings.skybox.SetFloat("_Exposure", 0.8f);
		menu.SetActive (true);
		loadingImage.SetActive (false);
	}

	void Update(){
		theCamera.RotateAround(Vector3.zero, Vector3.up, rotateSpeed*Time.deltaTime);
	}

	public void CreateButtonClicked(){
		if (int.TryParse (rowText.text, out rowInput) && int.TryParse (columnText.text, out columnInput) && int.TryParse (stackText.text, out stackInput)) {
			if (rowInput > 0 && columnInput > 0 && stackInput > 0) {
				PlayerPrefs.SetInt ("rows", rowInput);
				PlayerPrefs.SetInt ("columns", columnInput);
				PlayerPrefs.SetInt ("stacks", stackInput);

				menu.SetActive (false);
				loadingImage.SetActive (true);
				SceneManager.LoadScene (1);
			} else {
				StartCoroutine (error ());
			}
		} else {
			StartCoroutine (error ());
		}
	}
	IEnumerator error(){
		errorText.SetActive (true);
		yield return new WaitForSeconds (2f);
		errorText.SetActive (false);
	}
}
