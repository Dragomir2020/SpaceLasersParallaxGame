using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour {

	public GameObject Enemies;

	//Create game object with enemies from prefab on awake
	void Awake(){
		if (Enemies != null) {
			GameObject go = Instantiate (Enemies, this.transform.position, this.transform.rotation);
			//Set this game object as parent
			go.transform.parent = this.transform;
		} else {
			Debug.LogError ("Add EnemySpawner prefab to EnemyFormation GameObject in inspector");
		}
	}
}
