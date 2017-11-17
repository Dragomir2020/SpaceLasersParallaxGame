using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

	public float playerSpeed = 15f;
	public float padding = 1f;
	public float projectileSpeed = 20f;
	public GameObject playerSpawner;
	public float fireingRate = 0.2f;
	public float playerHealth = 150f;

	private float xmin;
	private float xmax;
	private int playerGOIndex = 0;
	//IMPORTANT: ships[0] is ships GO and then individual sprites are preceding
	private SpriteRenderer[] ships;

	void Start () {
		ParsePrefab ();
		CreatePlayerSpawnerGO ();
		CreatePlayerShip (1);
		ScreenSize();
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
		if (playerSpawner != null) {
			//GetPlayerElements
			GameObject shipsGO = playerSpawner.transform.GetChild (playerGOIndex).gameObject;
			ships = shipsGO.GetComponentsInChildren<SpriteRenderer> (true);
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

	/// <summary>
	///  Create player prefab game object so other classes can parse out assets
	/// </summary>
	private void CreatePlayerSpawnerGO(){
		GameObject playerSpawn = Instantiate (playerSpawner, this.transform.position, Quaternion.identity) as GameObject;
		playerSpawn.transform.parent = this.transform;
	}


}
