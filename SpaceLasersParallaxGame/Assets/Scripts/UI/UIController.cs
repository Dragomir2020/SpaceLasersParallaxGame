using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

	private GameObject life;
	private GameObject[] livesUI = new GameObject[3];

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
		Destroy (livesUI [lives]);
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
		for (int i = 0; i < lives; i++) {
			livesUI[i] = Instantiate (life, life.transform.position + new Vector3(0.5f * (float)i, 0f, 0f), Quaternion.identity) as GameObject;
			livesUI[i].SetActive (true);
			livesUI[i].transform.SetParent (this.transform);
		}
	}
}
