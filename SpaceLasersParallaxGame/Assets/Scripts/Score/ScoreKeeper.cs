using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///  Class keeps track of player score
/// </summary>
public class ScoreKeeper : MonoBehaviour {

	private int Score;
	private Text ScoreText;

	void Start(){
		Score = 0;
		ScoreText = this.GetComponent<Text> ();
		ScoreText.text = Score.ToString ();
	}

	/// <summary>
	///  Returns score
	/// </summary>
	public int GetScore(){
		return Score;
	}

	/// <summary>
	///  Adds points to score
	/// </summary>
	public void AddToScore(int points){
		Score += points;
		ScoreText.text = Score.ToString ();
	}

	/// <summary>
	///  Resets score to zero
	/// </summary>
	public void ClearScore(){
		Score = 0;
	}
}
