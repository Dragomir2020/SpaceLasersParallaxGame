using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
///  Class controlls player related initialization, movement and shooting	
/// </summary>
public class PlayerController : MonoBehaviour {

	public float playerSpeed = 15f;
	public float padding = 1f;
	public float projectileSpeed = 20f;
	public float fireingRate = 0.2f;
	public float playerHealth = 150f;
	public AudioClip fireSound;

	private float xmin;
	private float xmax;
	private int playerGOIndex = 0;
	private int laserGOIndex = 1;
	//IMPORTANT: lasers[0] is lasers GO and then individual sprites are preceding
	private SpriteRenderer laser;
	private float maxPlayerHealth;

	/// <summary>
	///  Initializes screen variables and gets laser game object
	/// </summary>
	void Start () {
		ScreenSize();
		GetLaserGO ();
		maxPlayerHealth = playerHealth;
	}

	/// <summary>
	///  Calls given functions every frame
	/// </summary>
	void Update () {
		InvokeFireSpaceKeyPressed ();
		MovePlayerLeftOrRight ();
	}

	/// <summary>
	///  Invokes FireLaser function when the space key is pressed
	/// </summary>
	private void InvokeFireSpaceKeyPressed(){
		if(Input.GetKeyDown(KeyCode.Space)){
			// Invokes FireLaser function at given interval
			InvokeRepeating ("FireLaser", 0.000001f, fireingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("FireLaser");
		}
	}

	/// <summary>
	///  Moves the player left and right between bounds when left and right keys are pressed
	/// </summary>
	private void MovePlayerLeftOrRight(){
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
			GameObject Laser = Instantiate (laser.gameObject, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity) as GameObject;
			//Laser.transform.parent = this.transform;
			Laser.SetActive (true);
			Laser.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, projectileSpeed, 0f);
			AudioSource.PlayClipAtPoint (fireSound, transform.position);
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
				UIController cont = this.transform.parent.parent.GetChild (0).GetComponent<UIController> ();
				cont.DeleteLife ();
				if (cont.NumberOfLives == 0) {
					Die ();
				} else {
					//Set game object invisible for some time or delete and recreate
					playerHealth = maxPlayerHealth;
				}
			}
		}
	}

	private void Die(){
		//Get Score game object
		GameObject score = GameObject.Find ("Score");
		int scoreNum = score.GetComponent<ScoreKeeper> ().GetScore ();
		//Save score to music player
		GameObject musicP = GameObject.Find ("MusicPlayer");
		if(musicP != null){
			MusicPlayer mus = musicP.GetComponent<MusicPlayer> ();
			mus.PlayerScore = scoreNum;
		}
		SceneManager.LoadScene ("Lose Screen");
		Destroy (gameObject);
	}
		
}