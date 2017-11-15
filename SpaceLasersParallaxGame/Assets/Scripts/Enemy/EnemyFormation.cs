using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormation : MonoBehaviour {

	private float xmin;
	private float xmax;
	private bool isMovingRight = true;

	public GameObject Enemies;
	public float padding = 0.1f;
	public float speed = 0.01f;
	private float width;
	public float height = 5f;

	/// <summary>
	///  Create game object with enemies from prefab on awake
	/// </summary>
	void Awake(){
		if (Enemies != null) {
			GameObject go = Instantiate (Enemies, this.transform.position, this.transform.rotation);
			//Set this game object as parent
			go.transform.parent = this.transform;
		} else {
			Debug.LogError ("Add EnemySpawner prefab to EnemyFormation GameObject in inspector");
		}
	}

	void Start(){
		ScreenSize ();
	}

	/// <summary>
	///  Moves formation left and right within screen bounds
	/// </summary>
	void Update(){
		if (isMovingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		} else {
			transform.position -= Vector3.right * speed * Time.deltaTime;
		}
		if(transform.position.x - 0.5f * width < xmin){
			isMovingRight = true;
		} else if(transform.position.x + 0.5f * width > xmax){
			isMovingRight = false;
		}
	}
		
	/// <summary>
	///  Allows EnemyFormation space to be visible in editor
	/// </summary>
	public void OnDrawGizmos(){
		Gizmos.DrawWireCube (transform.position, new Vector3(width, height, -5f));
	}
	/// <summary>
	///  Set parameters for screen size 
	/// </summary>
	private void ScreenSize(){
		//Locate bounds
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint (new Vector3(0f, 0f, distance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint (new Vector3(1f, 0f, distance));
		width = (rightMostPos.x - leftMostPos.x) * 0.5f;
		xmin = leftMostPos.x + padding;
		xmax = rightMostPos.x - padding;
	}
}
