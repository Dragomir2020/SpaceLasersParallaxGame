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
	public GameObject PlayerSpawner;
	public float fireingRate = 0.2f;


	private float xmin;
	private float xmax;
	private int playerGOIndex = 0;
	private int laserGOIndex = 1;
	//IMPORTANT: ships[0] is ships GO and then individual sprites are preceding
	private SpriteRenderer[] ships;
	//IMPORTANT: lasers[0] is lasers GO and then individual sprites are preceding
	private SpriteRenderer[] lasers;

	void Start () {
		ParsePrefab ();
		CreatePlayerShip (1);
		ScreenSize();
	}
		
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)){
			InvokeRepeating ("FireLaser", 0.000001f, fireingRate);
		}
		if (Input.GetKeyUp (KeyCode.Space)) {
			CancelInvoke ("FireLaser");
		}
		Vector3 previousPos = this.transform.GetChild (playerGOIndex).position;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//Move Player ship right
			this.transform.GetChild (playerGOIndex).position = new Vector3 (Mathf.Clamp (previousPos.x - (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			//Move player ship left
			this.transform.GetChild (playerGOIndex).position = new Vector3 (Mathf.Clamp (previousPos.x + (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		}
	}

	private void FireLaser(){
		GameObject Laser = Instantiate (lasers [1].gameObject, this.transform.GetChild (playerGOIndex).position, Quaternion.identity) as GameObject;
		Laser.transform.parent = this.transform;
		Laser.SetActive (true);
		Laser.GetComponent<Rigidbody2D>().velocity = new Vector3 (0f, projectileSpeed, 0f);
	}
		
	/// <summary>
	///  Set screen size related variables
	/// </summary>
	private void ScreenSize(){
		//Ship GO
		GameObject GM = this.transform.GetChild(playerGOIndex).gameObject;
		float distance = GM.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint (new Vector3(0f, 0f,distance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint (new Vector3(1f, 0f,distance));
		xmin = leftMostPos.x + padding;
		xmax = rightMostPos.x - padding;
	}
		
	/// <summary>
	///  Parse player objects out of prefab
	/// </summary>
	private void ParsePrefab(){
		if (PlayerSpawner != null) {
			//GetPlayerElements
			GameObject shipsGO = PlayerSpawner.transform.GetChild (playerGOIndex).gameObject;
			GameObject lasersGO = PlayerSpawner.transform.GetChild (laserGOIndex).gameObject;
			ships = shipsGO.GetComponentsInChildren<SpriteRenderer> (true);
			lasers = lasersGO.GetComponentsInChildren<SpriteRenderer> (true);
		} else {
			Debug.LogWarning ("Attach Player Spawner prefab to PlayerController");
		}
	}
		
	/// <summary>
	///  Create initial player ship
	/// </summary>
	private void CreatePlayerShip(int index){
		GameObject PlayerShip = Instantiate (ships[index].gameObject, new Vector3(0f, -4f, -5f), this.transform.rotation);
		PlayerShip.transform.parent = this.transform;
		PlayerShip.SetActive (true);
	}



}
