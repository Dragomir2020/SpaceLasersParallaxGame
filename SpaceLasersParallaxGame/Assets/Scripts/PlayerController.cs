using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float playerSpeed = 15f;
	public float padding = 1f;
	public GameObject PlayerSpawner;

	private float xmin;
	private float xmax;
	private int playerGOIndex = 0;
	private int laserGOIndex = 1;
	//IMPORTANT: ships[0] is ships GO and then individual sprites are preceding
	private SpriteRenderer[] ships;
	//IMPORTANT: lasers[0] is lasers GO and then individual sprites are preceding
	private SpriteRenderer[] lasers;

	// Use this for initialization
	void Start () {
		ParsePrefab ();
		CreatePlayerShip (1);
		ScreenSize();
	}

	// Update is called once per frame
	void Update () {
		Vector3 previousPos = this.transform.GetChild (playerGOIndex).position;
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//Move Player ship right
			this.transform.GetChild (playerGOIndex).position = new Vector3 (Mathf.Clamp (previousPos.x - (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			//Move player ship left
			this.transform.GetChild (playerGOIndex).position = new Vector3 (Mathf.Clamp (previousPos.x + (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		}
	}

	private void ScreenSize(){
		//Ship GO
		GameObject GM = this.transform.GetChild(playerGOIndex).gameObject;
		float distance = GM.transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint (new Vector3(0f, 0f,distance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint (new Vector3(1f, 0f,distance));
		xmin = leftMostPos.x + padding;
		xmax = rightMostPos.x - padding;
	}

	private void ParsePrefab(){
		//GetPlayerElements
		GameObject shipsGO = PlayerSpawner.transform.GetChild(playerGOIndex).gameObject;
		GameObject lasersGO = PlayerSpawner.transform.GetChild(laserGOIndex).gameObject;
		ships = shipsGO.GetComponentsInChildren<SpriteRenderer> (true);
		lasers = lasersGO.GetComponentsInChildren<SpriteRenderer> (true);
	}

	private void CreatePlayerShip(int index){
		GameObject PlayerShip = Instantiate (ships[index].gameObject, new Vector3(0f, -4f, -5f), this.transform.rotation);
		PlayerShip.transform.parent = this.transform;
		PlayerShip.SetActive (true);
	}
}
