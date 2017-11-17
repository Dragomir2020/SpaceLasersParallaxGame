using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
///  Class handles level related tasks as well as switching scenes
/// </summary>
public class LevelManager : MonoBehaviour {

	/// <summary>
	///  Open new scenes
	/// </summary>
	public void LoadLevel(string name){
		Debug.Log ("New Level load: " + name);
		SceneManager.LoadScene (name);
	}

	/// <summary>
	///  Quit game
	/// </summary>
	public void QuitRequest(){
		Debug.Log ("Quit requested");
		Application.Quit ();
	}

}
