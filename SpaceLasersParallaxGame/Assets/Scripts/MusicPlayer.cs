using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
		}
	}

	//USE THIS TO PERSIST DATA BETWEEN CLASSES
	private string myName;

	public string PlayerName{
		get { return myName; }
		set { this.myName = value; }
	}

	private int myScore;

	public int PlayerScore{
		get { return myScore; }
		set { this.myScore = value; }
	}

}
