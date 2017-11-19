using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  Class keeps track of enemy health and controlls its individual behaviour
/// </summary>
public class EnemyController : MonoBehaviour {

	public float health = 80f;
	public float laserSpeed = 10f;
	public float shotsPerSecond = 0.5f;
	public AudioClip fireSound;
	public AudioClip deathSound;

	private float TotalHealth;
	private ScoreKeeper ScoreKeeperGO;
	private GameObject redLaser;
	private GameObject explosion;

	/// <summary>
	///  Called when Enemy is spawned
	/// </summary>
	void Start(){
		ParseLaserGameObject ();
		TotalHealth = health;
		ScoreKeeperGO = GameObject.Find ("Score").GetComponent<ScoreKeeper> ();
	}

	/// <summary>
	///  Fires enemies laser based off of calculated probability
	/// </summary>
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
		GameObject redLaserInstance = Instantiate (redLaser, transform.position, Quaternion.identity) as GameObject;
		//This causes transform to chage with parent
		//redLaserInstance.transform.parent = this.transform;
		redLaserInstance.SetActive (true);
		redLaserInstance.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0f, -laserSpeed, 0f);
		AudioSource.PlayClipAtPoint (fireSound, transform.position);
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
				Die ();
			}
		}
	}
		
	/// <summary>
	///  Parses out laser game object to be shot at player
	/// </summary>
	private void ParseLaserGameObject(){
		//TODO: This could be improved with GetComponentsInChildren<Projectile>();
		redLaser = this.transform.parent.parent.GetChild (0).GetChild (5).GetChild (0).gameObject;
	}

	/// <summary>
	///  Kills enemy
	/// </summary>
	private void Die(){
		//Decrease enemy count by 1 on EnemySpawner
		gameObject.transform.parent.parent.GetChild(0).GetComponent<EnemySpawner>().EnemyKilled();
		//IncreaseScore
		ScoreKeeperGO.AddToScore((int)TotalHealth);
		AudioSource.PlayClipAtPoint (deathSound, transform.position);
		GetExplosionGO ();
		GameObject ex = Instantiate (explosion, this.transform.position, Quaternion.identity) as GameObject;
		ex.SetActive (true); //Invokes explosion on awake with self destruct
		//Destroy enemy game object
		Destroy(gameObject);
	}

	private void GetExplosionGO(){
		explosion = this.transform.parent.parent.GetChild (0).GetChild (6).GetChild (0).gameObject;
	}

}
