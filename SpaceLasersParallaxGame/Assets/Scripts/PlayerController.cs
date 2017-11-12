using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float playerSpeed;

	// Use this for initialization
	void Start () {
		playerSpeed = 0.25f; //World Units moved per frame
	}

	// Update is called once per frame
	void Update () {
		Vector3 previousPos = this.GetComponent<SpriteRenderer> ().transform.position;
		if(Input.GetKey(KeyCode.LeftArrow)){
			//Move Player ship right
			this.GetComponent<SpriteRenderer> ().transform.position = new Vector3(Mathf.Clamp (previousPos.x - playerSpeed, -6f, 6f), -4f, -5f);
		} else if(Input.GetKey(KeyCode.RightArrow)){
			this.GetComponent<SpriteRenderer> ().transform.position = new Vector3(Mathf.Clamp (previousPos.x + playerSpeed, -6f, 6f), -4f, -5f);
		}
	}
}
