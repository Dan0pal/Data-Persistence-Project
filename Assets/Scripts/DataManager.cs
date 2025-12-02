using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

//Class to manage data across scenes and across game sessions
public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string PlayerName;

    public int HighScore=0;
    public string HighScorePlayerName="";

    public TMP_InputField nameInputField;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerName = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        PlayerName = nameInputField.text;
    }

    //Method to set high score if current score is greater
    public void SetHighScore(int score, string playerName)
    {
        if (score > HighScore)
        {
            HighScore = score;
            HighScorePlayerName = playerName;
        }
    }

    //Method to save data to persistent storage 
    [System.Serializable]
    class SaveDataClass
    {
        public int HighScore;
        public string HighScorePlayerName;
    }

    public void SaveData()
    {
        SaveDataClass data = new SaveDataClass();
        data.HighScore = HighScore;
        data.HighScorePlayerName = HighScorePlayerName;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    //Method to load data from persistent storage
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveDataClass data = JsonUtility.FromJson<SaveDataClass>(json);

            HighScore = data.HighScore;
            HighScorePlayerName = data.HighScorePlayerName;
        }
    }
}