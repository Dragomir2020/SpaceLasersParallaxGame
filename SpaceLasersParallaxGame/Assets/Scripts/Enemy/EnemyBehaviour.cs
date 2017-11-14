using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Class keeps track of enemy health and controlls its individual behaviour
/// </summary>
public class EnemyBehaviour : MonoBehaviour {

	public float health = 150f;
	public float laserSpeed = 10f;

	private GameObject redLaser;

	void Start(){
		//TODO: This could be improved with GetComponentsInChildren<Projectile>();
		redLaser = this.transform.parent.parent.GetChild (0).GetChild (5).GetChild (0).gameObject;
	}

	void Update(){
		Vector3 startPosition = transform.position + new Vector3 (0f, -1f, 0f);
		GameObject redLaserInstance = Instantiate (redLaser, startPosition, Quaternion.identity) as GameObject;
		redLaserInstance.SetActive (true);
		redLaserInstance.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, -laserSpeed, 0f);
	}

	/// <summary>
	///  If hit enemies health is decreased by projectile strength
	/// </summary>
	void OnTriggerEnter2D(Collider2D collider){
		Projectile laser = collider.gameObject.GetComponent<Projectile> ();
		if (laser != null) {
			health -= laser.GetDamage ();
			laser.Hit ();
			if (health <= 0){
				Destroy (gameObject);
			}
		}
	}

}
