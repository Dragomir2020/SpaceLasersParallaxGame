using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

	void OnDrawGizmos(){
		Gizmos.DrawWireSphere (this.transform.position, 1f);
	}
}
