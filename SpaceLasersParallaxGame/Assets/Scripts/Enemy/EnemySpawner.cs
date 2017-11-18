using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Class creates enemy formations and parses gameobjects out of prefab
/// </summary>
public class EnemySpawner : MonoBehaviour {

	private List<SpriteRenderer[]> Enemies = new List<SpriteRenderer[]>();
	private GameObject enemyPosition;
	private float width;
	private float xmin;
	private float xmax;
	private int numberOfEnemies = 0;

	public float padding = 0f;
	public float spawnDelay = 0.5f;

	void Awake(){
		//Get screen parameteres
		ScreenSize ();
		ParseEnemyPrefabs ();
	}

	// Use this for initialization
	void Start () {
		InitializeEnemyPositions ();
		CreateEnemiesAtPositions ();
	}

	/// <summary>
	///  Creates enemy positions inside of gizmo
	/// </summary>
	private void InitializeEnemyPositions(){
		//Create Initial Enemy Positions
		for(float i = -0.5f * width; i < 0.5f * width; i += 1){
			for(float j = 4f; j > 0; j -= 1){
				GameObject enemyPos = Instantiate (enemyPosition, new Vector3(i, j, -5f), this.transform.rotation);
				enemyPos.transform.parent = this.transform.parent;
				enemyPos.SetActive (true);
			}
		}
	}

	/// <summary>
	///  Parses enemy ships out of game object prefab
	/// </summary>
	private void ParseEnemyPrefabs(){
		//Get different enemy ships from prefab object
		GameObject[] Colors = GameObject.FindGameObjectsWithTag("Enemies");
		//enemyPosition = GameObject.FindGameObjectWithTag ("EnemyPosition");
		enemyPosition = this.GetComponentInChildren<Position>(true).gameObject;
		foreach (GameObject color in Colors) {
			//Must set true to get inactive components
			Enemies.Add(color.GetComponentsInChildren<SpriteRenderer>(true));
		}
	}

	/// <summary>
	///  Creates enemy game object at each position created
	/// </summary>
	private void CreateEnemiesAtPositions(){
		//Find enemy positions
		Position[] enemiesPositions = this.transform.parent.GetComponentsInChildren<Position>();
		//Create enemy at enemiesPositions
		foreach(Position pos in enemiesPositions){
			//This is basic black enemy and could be changed
			GameObject enemy = Instantiate (Enemies [1] [0].gameObject, pos.gameObject.transform);
			enemy.SetActive (true);
			numberOfEnemies++;
		}
	}

	/// <summary>
	///  Spawn enemies with delay until all positions are occupied
	/// </summary>
	void SpawnUntilFull(){
		Transform freePosition = NextFreePosition();
		if(freePosition != null){
			GameObject enemy = Instantiate (Enemies [1] [0].gameObject, freePosition);
			enemy.SetActive (true);
			numberOfEnemies++;
			Invoke ("SpawnUntilFull", spawnDelay);
		}
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

	/// <summary>
	///  Returns true if all enemies are gone
	/// </summary>
	public bool AllEnemiesAreDead(){
		if (numberOfEnemies <= 10) {
			return true;
		}
		return false;
	}

	/// <summary>
	///  Requires alternate script to delete enemy each time gameobject is destroyed
	/// </summary>
	public void EnemyKilled(){
		numberOfEnemies--;
		if (AllEnemiesAreDead()) {
			SpawnUntilFull ();
		}
	}

	/// <summary>
	///  Returns transform of next free position in enemy formation
	/// </summary>
	private Transform NextFreePosition(){
		Position[] pos = transform.parent.GetComponentsInChildren<Position> ();
		foreach(Position childPosition in pos){
			if (childPosition.gameObject.transform.childCount == 0) {
				return childPosition.gameObject.transform;
			}
		}
		return null;
	}
}
