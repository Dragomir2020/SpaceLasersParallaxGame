using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
/// <summary>
///  Class used to save and load previous scores
/// </summary>
public class Leaderboard : MonoBehaviour {

	public Text tex;
	private string filePath;
	//My score is passed in
	private int myScore;
	//My name is recieved from field
	private string myName;
	private LeaderboardData allLeaders;

	// Use this for initialization
	void Start () {
		filePath = "/playerData.dat";
		LoadPlayerScore ();
		//LoadData ();
		if (myScore != null) {
			tex.text = myScore.ToString ();
		} else {
			tex.text = "0";
		}
	}

	void Update(){
		//Execute on Enter
		if (Input.GetKey (KeyCode.KeypadEnter)) {
			myScore = 3100;
			myName = this.GetComponent<Text> ().text;
		}
	}

	/// <summary>
	///  Saves new player score to file
	/// </summary>
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + filePath, FileMode.Open);

		int count = 0;

		//Loop through and add score to leaderboard players if they beat anyone
		foreach(LeaderboardData.LeaderBoardPlayer n in allLeaders.LeaderboardArray){
			
			if (n.PlayerScore < myScore) {
				
				//Loop through moving down players
				for (int i = count; i < 9; i++) {
					allLeaders.LeaderboardArray [i + 1].PlayerScore = n.PlayerScore;
					allLeaders.LeaderboardArray [i + 1].PlayerName = n.PlayerName;
				}

				//Set new score after moving down losers
				allLeaders.LeaderboardArray[count].PlayerScore = n.PlayerScore;
				allLeaders.LeaderboardArray [count].PlayerName = n.PlayerName;

				break;
			}

			count++;
		}

		//Save data in binary to file
		bf.Serialize (file, allLeaders);
		file.Close ();
	}

	/// <summary>
	///  Loads player data
	/// </summary>
	public void LoadData(){
		BinaryFormatter bf = new BinaryFormatter ();
		//Check whether file exists
		if (File.Exists (Application.persistentDataPath + filePath)) {
			FileStream file = File.Open (Application.persistentDataPath + filePath, FileMode.Open);
			allLeaders = (LeaderboardData)bf.Deserialize (file);
			file.Close ();
		}
	}

	/// <summary>
	///  Loads player score from persisted data in music player
	/// </summary>
	private void LoadPlayerScore(){
		MusicPlayer mus = GameObject.Find ("MusicPlayer").GetComponent<MusicPlayer> ();
		myScore = mus.PlayerScore;
	}


	/// <summary>
	///  Class holds data for the leaderboards to be converted to binary and saved
	/// </summary>
	[Serializable]
	public class LeaderboardData
	{
		private LeaderBoardPlayer [] leaders = new LeaderBoardPlayer[10];

		public LeaderBoardPlayer[] LeaderboardArray{
			get { return leaders; }
			set { this.leaders = value; }
		}

		/// <summary>
		///  Base class contains single players data
		/// </summary>
		public class LeaderBoardPlayer{
			private int playerScore;

			public int PlayerScore{
				get { return playerScore; }
				set { this.playerScore = value; }
			}

			private string playerName;

			public string PlayerName{
				get { return playerName; }
				set { this.playerName = value; }
			}
		}
	}
}
	
