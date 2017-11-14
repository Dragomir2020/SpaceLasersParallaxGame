using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	/// <summary>
	///  Open new scenes
	/// </summary>
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		SceneManager.GetSceneByName (name);
	}

	/// <summary>
	///  Quit game
	/// </summary>
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
