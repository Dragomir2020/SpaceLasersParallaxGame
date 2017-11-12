using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float playerSpeed = 15f;
	public float padding = 1f;
	float xmin;
	float xmax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftMostPos = Camera.main.ViewportToWorldPoint (new Vector3(0f, 0f,distance));
		Vector3 rightMostPos = Camera.main.ViewportToWorldPoint (new Vector3(1f, 0f,distance));
		xmin = leftMostPos.x + padding;
		xmax = rightMostPos.x - padding;
	}

	// Update is called once per frame
	void Update () {
		Vector3 previousPos = this.GetComponent<SpriteRenderer> ().transform.position;
		if(Input.GetKey(KeyCode.LeftArrow)){
			//Move Player ship right
			this.GetComponent<SpriteRenderer> ().transform.position = new Vector3(Mathf.Clamp (previousPos.x - (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		} else if(Input.GetKey(KeyCode.RightArrow)){
			//Move player ship left
			this.GetComponent<SpriteRenderer> ().transform.position = new Vector3(Mathf.Clamp (previousPos.x + (playerSpeed * Time.deltaTime), xmin, xmax), -4f, -5f);
		}
	}
}
