using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
	GameManager gameManager;

	GameObject PlayerBody;
	Rigidbody rb;
	public Text posText;

	public float mouseSentivity;
	float yaw, pitch;

	public Vector3 moveInput;
	public float walkSpeed, runSpeed, currentSpeed;

	public float jumpForce;
	public bool grounded;
	public float groundDistance;
	public bool flying=false;
	public float flyUpSpeed, flyDownSpeed;

	public float OAcceleration;
	float v;

	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		PlayerBody = transform.parent.gameObject;
		rb = PlayerBody.GetComponent<Rigidbody> ();

		Cursor.lockState = CursorLockMode.Locked;
		currentSpeed = walkSpeed;
	}
		
	bool one_click = false;
	bool timer_running;
	float timer_for_double_click;
	float delay = 0.5f; 

	void Update () {

		if (gameManager.gamePaused) {
			rb.isKinematic = true;
			return;
		}
		rb.isKinematic = false;

		posText.text = transform.position.ToString ();
		if (Input.GetKey (KeyCode.O)) {
			PlayerBody.transform.Translate ((Vector3.zero - PlayerBody.transform.position).normalized * v * Time.deltaTime, Space.World);
			v += OAcceleration;
		} else if (Input.GetKeyUp (KeyCode.O))
			v = 0;

		//Mouse lock
		if (Input.GetMouseButtonDown (0))
			Cursor.lockState = CursorLockMode.Locked;
		else if (Input.GetKeyDown (KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
		//Mouse Movement
		yaw -= Input.GetAxis ("Mouse Y") * mouseSentivity;
		pitch += Input.GetAxis ("Mouse X") * mouseSentivity;
		PlayerBody.transform.eulerAngles = new Vector3 (0, pitch, 0);
		transform.eulerAngles = new Vector3 (yaw, pitch, 0);


		//Walking and Running
		moveInput = new Vector3 (Input.GetAxis ("Horizontal"),0, Input.GetAxis ("Vertical"));
		currentSpeed = (Input.GetKey (KeyCode.LeftControl))?runSpeed:walkSpeed;
		if (!flying || moveInput != Vector3.zero)
			rb.AddForce (transform.parent.TransformDirection (
				new Vector3 (moveInput.x * currentSpeed, rb.velocity.y, moveInput.z * currentSpeed)) - rb.velocity, 
				ForceMode.VelocityChange);

		//Jumping
		grounded = Physics.Raycast (transform.position, Vector3.down, groundDistance);
		if (grounded){
			if(!flying && Input.GetButtonDown ("Jump")) 
				rb.AddForce (Vector3.up * jumpForce);
			flying = false;
		}

		//Flying

		if (CheckForDoubleSpace ()) 
			flying = !flying;
		
		rb.useGravity = !flying;

		if (flying && Input.GetKey (KeyCode.Space)) {rb.velocity = (rb.velocity + Vector3.up).normalized * flyUpSpeed;}
		if (flying && Input.GetKey (KeyCode.LeftShift)) {rb.velocity = (rb.velocity + Vector3.down).normalized * flyDownSpeed;}
		if (flying && (Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp (KeyCode.LeftShift))) {rb.velocity = Vector3.zero;}

		if (flying && moveInput == Vector3.zero && !Input.GetKey (KeyCode.Space) && !Input.GetKey (KeyCode.LeftShift)) {
			rb.velocity = Vector3.zero;
		}
	}

	bool CheckForDoubleSpace(){
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (!one_click) { //nếu trước đó chưa click lần nào
				one_click = true; //đánh dấu click 1 lần
				//lưu lại thời gian hiện tại để bắt đầu tính giờ
				timer_for_double_click = Time.time; 

			} else { //nếu trước đó đã click 1 lần rồi
				one_click = false; //reset lại, coi như chưa click lần nào
				return true;
			}
		}

		if (one_click) {
			if (Time.time - timer_for_double_click > delay) {
				// nếu thời gian đã qua lớn hơn delay thì sẽ reset lại, coi như chưa click lần nào
				one_click = false;

			}
		}

		return false;
	}
}
