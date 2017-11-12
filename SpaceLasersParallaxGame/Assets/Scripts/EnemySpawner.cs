using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	private List<SpriteRenderer[]> Enemies = new List<SpriteRenderer[]>();

	// Use this for initialization
	void Start () {
		//Get different enemy ships from prefab object
		GameObject[] Colors = GameObject.FindGameObjectsWithTag("Enemies");
		print ("Colors " + Colors.Length.ToString());
		foreach (GameObject color in Colors) {
			//Must set true to get inactive components
			Enemies.Add(color.GetComponentsInChildren<SpriteRenderer>(true));
		}
		CreateBlackShip1Example ();
	}

	private void CreateBlackShip1Example(){
		//Create example enemy
		GameObject blackShip = Instantiate (Enemies [0] [0].gameObject, this.transform.position, this.transform.rotation);
		//Set enemie's parent to EnemyFormation
		blackShip.transform.parent = this.transform.parent;
		//Set GameObject active in the scene to make it visible
		blackShip.SetActive (true);
	}
}
