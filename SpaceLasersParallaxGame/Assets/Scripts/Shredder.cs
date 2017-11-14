using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour {

	/// <summary>
	///  Destroy any game object that triggers shredder
	/// </summary>
	void OnTriggerEnter2D(Collider2D col){
		Destroy (col.gameObject);
	}

}
