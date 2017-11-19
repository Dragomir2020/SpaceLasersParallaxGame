using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.SceneManagement;


/// <summary>
///  Class used to save and load previous scores
/// </summary>
public class LeaderBoard : MonoBehaviour {

	public Text tex;
	private string filePath;
	//My score is passed in
	private int myScore;
	//My name is recieved from field
	private string myName;
	private LeaderboardData allLeaders = new LeaderboardData();

	// Use this for initialization
	void Start () {
		filePath = "/playerData.dat"; //Filepath to store binary file
		LoadPlayerScore (); //Loads player score from music player
		LoadData (); //Loads data from binary and parses into gameobject
		if (myScore != null) {
			tex.text = myScore.ToString ();
		} else {
			tex.text = "0";
		}
		UpdateUILeaderboard ();
	}

	void Update(){
		//Execute on Enter
		if (Input.GetKey (KeyCode.KeypadEnter) || Input.GetKey("enter") || Input.GetKey(KeyCode.Return)) {
			ButtonGetName ();
		}
	}

	private void UpdateUILeaderboard(){
		//Find is slow and should not be used in update methods but is ok here
		String Leaders = "";
		int count = 1;
		foreach (LeaderboardData.LeaderBoardPlayer n in allLeaders.LeaderboardArray){
			string updateName = n.PlayerName.PadRight (15 - n.PlayerName.Length);
			Leaders += " " + count.ToString() + ": " + updateName + "  " + n.PlayerScore + System.Environment.NewLine; 
			count++;
		}
		GameObject.Find ("Viewport").GetComponentInChildren<Text> ().text = Leaders;
	}

	/// <summary>
	///  Activated when Start Game button is pressed and saves data to leaderboardData
	/// </summary>
	public void ButtonGetName(){
		GetName ();
		Save ();
		SceneManager.LoadScene ("Start Menu");
	}

	/// <summary>
	/// Gets name from input field
	/// </summary>
	private void GetName(){
		string name = GameObject.Find ("Name").transform.GetChild (2).GetComponent<Text> ().text;
		if (name != null) {
			myName = name;
		} else {
			myName = "";
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
				for (int i = 8; i >= count; i--) {
					//THIS WILL ACTIVATE GARBAGE COLLECTION BC ALWAYS CREATING NEW PLAYER
					LeaderboardData.LeaderBoardPlayer player = new LeaderboardData.LeaderBoardPlayer();
					player.PlayerScore = allLeaders.LeaderboardArray[i].PlayerScore;
					player.PlayerName = allLeaders.LeaderboardArray[i].PlayerName;
					allLeaders.SetLeaderPosition (i + 1, player);
				}
					
				//Set new score after moving down losers
				LeaderboardData.LeaderBoardPlayer me = new LeaderboardData.LeaderBoardPlayer();
				me.PlayerScore = myScore;
				me.PlayerName = myName;
				allLeaders.SetLeaderPosition (count, me);

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
		//DeleteFile ();
		BinaryFormatter bf = new BinaryFormatter ();
		//Check whether file exists
		if (File.Exists (Application.persistentDataPath + filePath)) {
			Debug.LogWarning ("Loading file");
			FileStream file = File.Open (Application.persistentDataPath + filePath, FileMode.Open);
			allLeaders = (LeaderboardData)bf.Deserialize (file);
			file.Close ();
		} else {
			Debug.LogWarning ("Created file");
			//Create file and parse in faked data
			FileStream file = File.Open (Application.persistentDataPath + filePath, FileMode.CreateNew);
			LeaderboardData FakeData = new LeaderboardData();
			//Loop through moving down players
			for (int i = 0; i < 10; i++) {
				LeaderboardData.LeaderBoardPlayer player = new LeaderboardData.LeaderBoardPlayer();
				player.PlayerScore = 0;
				player.PlayerName = "None";
				FakeData.SetLeaderPosition (i, player);
			}
			allLeaders = FakeData;
			bf.Serialize (file, FakeData);
			file.Close ();
		}
	}

	/// <summary>
	///  IMPORTANT: This must be called to delete file if LeaderboardData class is changed
	/// </summary>
	private void DeleteFile(){
		if (File.Exists (Application.persistentDataPath + filePath)) {
			File.Delete (Application.persistentDataPath + filePath);
		}
	}
	 
	/// <summary>
	///  Loads player score from persisted data in music player
	/// </summary>
	private void LoadPlayerScore(){
		GameObject GO = GameObject.Find ("MusicPlayer");
		if (GO != null) {
			MusicPlayer mus = GO.GetComponent<MusicPlayer> ();
			myScore = mus.PlayerScore;
		} else {
			myScore = 0;
		}
	}


	/// <summary>
	///  Class holds data for the leaderboards to be converted to binary and saved
	/// </summary>
	[Serializable]
	public class LeaderboardData
	{
		private LeaderBoardPlayer [] leaders = new LeaderBoardPlayer[10];

		public void SetLeaderPosition(int pos, LeaderBoardPlayer player){
			leaders [pos] = player;
		}

		public LeaderBoardPlayer[] LeaderboardArray{
			get { return leaders; }
			set { this.leaders = value; }
		}

		/// <summary>
		///  Base class contains single players data
		/// </summary>
		[Serializable]
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
	
