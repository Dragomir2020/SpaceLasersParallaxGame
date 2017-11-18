using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	private GameObject life;
	// Use this for initialization
	void Start () {
		GetLifeGO ();
		CreateLifeUI ();
	}
		
	public int lives = 3;

	/// <summary>
	///  Delete life from outside class
	/// </summary>
	public void DeleteLife(){
		lives--;
	}

	/// <summary>
	///  Return or set number of lives
	/// </summary>
	public int NumberOfLives{
		get { return lives; }
	}

	/// <summary>
	///  Get the life game object from the player spawner
	/// </summary>
	private void GetLifeGO(){
		life = GameObject.Find ("PlayerSpawner(Clone)").transform.GetChild (2).GetChild (0).gameObject;
		//life = this.transform.parent.GetChild(3).GetChild(0).GetChild(2).GetChild(0).gameObject
	}

	/// <summary>
	///  Create UI for number of lives
	/// </summary>
	private void CreateLifeUI(){
		GameObject lifeGO = Instantiate (life, life.transform.position, Quaternion.identity) as GameObject;
		lifeGO.SetActive (true);
		lifeGO.transform.SetParent (this.transform);
	}
}
