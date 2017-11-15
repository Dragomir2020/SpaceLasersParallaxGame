using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Class destroys gameobjects that leave bounds of play space
/// </summary>
public class Shredder : MonoBehaviour {

	/// <summary>
	///  Destroy any game object that triggers shredder
	/// </summary>
	void OnTriggerEnter2D(Collider2D col){
		Destroy (col.gameObject);
	}

}
