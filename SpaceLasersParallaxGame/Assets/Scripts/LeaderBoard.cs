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

	private string filePath;
	//My score is passed in
	private int myScore;
	//My name is recieved from field
	private string myName;
	private LeaderboardData allLeaders;

	// Use this for initialization
	void Start () {
		filePath = "/playerData.dat";
		LoadData ();
	}

	/// <summary>
	///  Saves new player score to file
	/// </summary>
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + filePath, FileMode.Open);

		//Loop through and add score to leaderboard players if they beat anyone
		LeaderboardData leader = new LeaderboardData ();
		//leader.PlayerScore = myScore;
		//leader.PlayerName = myName;

		bf.Serialize (file, leader);
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
		} else {
			//Setup empty leaderboards

			//bf.Serialize (file, leader);

		}

	}

	/// <summary>
	///  Class holds data for the leaderboards to be converted to binary and saved
	/// </summary>
	[Serializable]
	private class LeaderboardData
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
	
