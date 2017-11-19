using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  Class sets projectile damage and ships check whether they collide with projectile class
/// </summary>
public class Projectile : MonoBehaviour {

	public float damage = 100f;

	/// <summary>
	///  Signals projectile hit object and should destroy itself
	/// </summary>
	public void Hit(){
		Destroy (gameObject);
	}

	/// <summary>
	///  Returns damage projectile causes
	/// </summary>
	public float GetDamage(){
		return damage;
	}

}
