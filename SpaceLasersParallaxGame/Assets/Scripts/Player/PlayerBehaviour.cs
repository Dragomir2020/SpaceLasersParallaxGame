using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	public float playerHealth = 150f;

	/// <summary>
	///  Triggered when player collider is triggered with laser or ship
	/// </summary>
	void OnTriggerEnter2D(Collider2D collider){
		Projectile laser = collider.gameObject.GetComponent<Projectile> ();
		if (laser != null) {
			playerHealth -= laser.GetDamage ();
			laser.Hit ();
			if (playerHealth <= 0){
				Destroy (gameObject);
			}
		}
	}

}
