using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Class keeps track of enemy health and controlls its individual behaviour
/// </summary>
public class EnemyController : MonoBehaviour {

	public float health = 150f;
	public float laserSpeed = 10f;
	public float fireingRate = 3f;
	public float shotsPerSecond = 0.5f;

	private GameObject redLaser;

	void Start(){
		//TODO: This could be improved with GetComponentsInChildren<Projectile>();
		redLaser = this.transform.parent.parent.GetChild (0).GetChild (5).GetChild (0).gameObject;
	}
		
	void Update(){
		float probability = shotsPerSecond * Time.deltaTime;
		//Random.value returns value between 0 and 1
		if(Random.value < probability){
			FireLaser ();
		}
	}

	/// <summary>
	///  Fires enemies laser
	/// </summary>
	private void FireLaser(){
		Vector3 startPosition = transform.position + new Vector3 (0f, -1f, 0f);
		GameObject redLaserInstance = Instantiate (redLaser, startPosition, Quaternion.identity) as GameObject;
		//This causes transform to chage with parent
		//redLaserInstance.transform.parent = this.transform;
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
