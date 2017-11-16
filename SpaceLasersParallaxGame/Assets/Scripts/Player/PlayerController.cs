using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///  Class controlls player related initialization, movement and shooting	
/// </summary>
public class PlayerController : MonoBehaviour {

	public float playerSpeed = 15f;
	public float padding = 1f;
	public float projectileSpeed = 20f;
	public float fireingRate = 0.2f;
	public float playerHealth = 150f;


	private float xmin;
	private float xmax;
	private int playerGOIndex = 0;
	private int laserGOIndex = 1;
	//IMPORTANT: lasers[0] is lasers GO and then individual sprites are preceding
	private SpriteRenderer laser;

	/// <summary>
	///  Initializes screen variables and gets laser game object
	/// </summary>
	void Start () {
		ScreenSize();
		GetLaserGO ();
	}

	/// <summary>
	///  Calls given functions every frame
	/// </summary>
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			// Invokes FireLaser function at given interval
			InvokeRepeating ("FireLaser", 0.000001f, fireingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("FireLaser");
		}
		Vector3 previousPos = this.transform.position;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//Move Player ship right
			this.transform.position = new Vector3 (Mathf.Clamp (previousPos.x - (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			//Move player ship left
			this.transform.position = new Vector3 (Mathf.Clamp (previousPos.x + (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		}
	}

	/// <summary>
	///  Fire laser from player at enemy
	/// </summary>
	private void FireLaser(){
		if (laser != null) {
			Vector3 shipPosition = this.transform.position + new Vector3 (0f, 1f, 0f);
			GameObject Laser = Instantiate (laser.gameObject, shipPosition, Quaternion.identity) as GameObject;
			//Laser.transform.parent = this.transform;
			Laser.SetActive (true);
			Laser.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, projectileSpeed, 0f);
		} else {
			Debug.LogWarning ("laser gameobject is null");
		}
	}
		
	/// <summary>
	///  Set screen size related variables
	/// </summary>
	private void ScreenSize(){
		//Ship GO
		GameObject GM = this.transform.gameObject;
		float distance = GM.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint (new Vector3(0f, 0f,distance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint (new Vector3(1f, 0f,distance));
		xmin = leftMostPos.x + padding;
		xmax = rightMostPos.x - padding;
	}

	/// <summary>
	///  Get laser gameobject for shooting
	/// </summary>
	private void GetLaserGO(){
		laser = this.transform.parent.GetChild(0).GetChild(1).GetChild(0).GetComponent<SpriteRenderer> ();
	}

	/// <summary>
	///  Triggered when player collider is triggered with laser or ship
	/// </summary>
	void OnTriggerEnter2D(Collider2D collider){
		Projectile laser = collider.gameObject.GetComponent<Projectile> ();
		if (laser != null) {
			playerHealth -= laser.GetDamage ();
			laser.Hit ();
			if (playerHealth <= 0){
				Destroy (gameObject);
			}
		}
	}
		

}
