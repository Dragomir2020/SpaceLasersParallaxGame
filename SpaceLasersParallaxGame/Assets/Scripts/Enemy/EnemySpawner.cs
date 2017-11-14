using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	private List<SpriteRenderer[]> Enemies = new List<SpriteRenderer[]>();
	private GameObject enemyPosition;
	private float width;
	private float xmin;
	private float xmax;

	public float padding = 0f;

	// Use this for initialization
	void Start () {
		//Get screen parameteres
		ScreenSize ();

		//Get different enemy ships from prefab object
		GameObject[] Colors = GameObject.FindGameObjectsWithTag("Enemies");
		//enemyPosition = GameObject.FindGameObjectWithTag ("EnemyPosition");
		enemyPosition = this.GetComponentInChildren<Position>(true).gameObject;
		foreach (GameObject color in Colors) {
			//Must set true to get inactive components
			Enemies.Add(color.GetComponentsInChildren<SpriteRenderer>(true));
		}

		//Create Initial Enemy Positions
		for(float i = -0.5f * width; i < 0.5f * width; i += 2){
			for(float j = 2.5f; j > -2; j -= 2){
				GameObject enemyPos = Instantiate (enemyPosition, new Vector3(i, j, -5f), this.transform.rotation);
				enemyPos.transform.parent = this.transform.parent;
				enemyPos.SetActive (true);
			}
		}

		//Find enemy positions
		Position[] enemiesPositions = this.transform.parent.GetComponentsInChildren<Position>();
	
		//Create enemy at enemiesPositions
		//foreach(Transform child in transform){
		foreach(Position pos in enemiesPositions){
			//This is basic black enemy and could be changed
			GameObject enemy = Instantiate (Enemies [1] [0].gameObject, pos.gameObject.transform);
			enemy.SetActive (true);
		}

		//I added this comment via vi		
		//CreateBlackShip1Example ();
	}

	/// <summary>
	///  Example of accessing parsed ship sprite and creating gameobject with it
	/// </summary>
	private void CreateBlackShip1Example(){
		//Create example enemy
		GameObject blackShip = Instantiate (Enemies [0] [0].gameObject, this.transform.position, this.transform.rotation);
		//Set enemie's parent to EnemyFormation
		blackShip.transform.parent = this.transform.parent;
		//Set GameObject active in the scene to make it visible
		blackShip.SetActive (true);
	}

	/// <summary>
	///  Set parameters relative to screen size
	/// </summary>
	private void ScreenSize(){
		//Locate bounds
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint (new Vector3(0f, 0f, distance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint (new Vector3(1f, 0f, distance));
		width = (rightMostPos.x - leftMostPos.x) * 0.5f;
		xmin = leftMostPos.x + padding;
		xmax = rightMostPos.x - padding;
	}
}
