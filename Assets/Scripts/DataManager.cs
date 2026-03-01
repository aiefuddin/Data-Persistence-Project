using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string playerName;
    public int currentPlayerScore;
    public int highScore;
    public List<PlayerRecord> players = new List<PlayerRecord>();

    private string savePath;


    [System.Serializable]
    public class PlayerRecord
    {
        public string name;
        public int score;
    }

    //THIS CLASS ONLY FOR SAVING THE DATA
    [System.Serializable]
    class SaveFile
    {
        public List<PlayerRecord> players;
    }

    private void Awake()
    {
        //Singleton pattern to ensure only one instance of DataManager exists
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        savePath = Application.persistentDataPath + "/savefile.json";
        LoadData();
    }

    public void SubmitScore(int score)
    {
        currentPlayerScore = score;

        //Check is current player exists in the list
        PlayerRecord existingPlayer = players.Find(p => p.name == playerName);

        if (existingPlayer != null)
        {
            //Update score if current score is higher
            if (score > existingPlayer.score)
            {
                existingPlayer.score = score;
            }
        }
        else
        {
            //Add new player record
            PlayerRecord newPlayer = new PlayerRecord
            {
                name = playerName,
                score = score
            };

            players.Add(newPlayer);

            UpdateHighScore();
            SaveData();
        }
    }

    private void UpdateHighScore()
    {
        highScore = 0;
        foreach (var player in players)
        {
            if (player.score > highScore)
            {
                highScore = player.score;
            }
        }
    }

    public void SaveData()
    {
        SaveFile data = new SaveFile();
        data.players = players;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadData()
    {
        if (!File.Exists(savePath))
            return;

        string json = File.ReadAllText(savePath);
        SaveFile data = JsonUtility.FromJson<SaveFile>(json);

        players = data.players ?? new List<PlayerRecord>();
    }

    public PlayerRecord GetHighestScorePlayer()
    {
        if (players == null || players.Count == 0)
            return null;

        PlayerRecord highest = players[0];

        foreach (var player in players)
        {
            if (player.score > highest.score)
            {
                highest = player;
            }
        }

        return highest;
    }
    public string HighestPlayerName
    {
        get
        {
            var top = GetHighestScorePlayer();
            return top != null ? top.name : "";
        }
    }

    public int HighestScore
    {
        get
        {
            var top = GetHighestScorePlayer();
            return top != null ? top.score : 0;
        }
    }

    public void ResetAllData()
    {
        players.Clear();
        highScore = 0;
        playerName = "";
        currentPlayerScore = 0;

        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
    }


}
