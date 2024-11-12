using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static MainMenuManager;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private AudioClip scoreSoundFX;
    [SerializeField] private Text scoreText;

    private int stringLength = 8;
    private int currentScore;
    private AudioSource audioSource;

    // Start is called before the first frame update

    void Update()
    {
    }
    void Start()
    {
        stringLength = scoreText.text.Length;
        var data = GetPlayerData();
        if (data != null)
        {
            currentScore = data.Score;
            scoreText.text = FormatScore(currentScore); //fill remain string with char
        }
        audioSource = GetComponent<AudioSource>();
    }
    private string FormatScore(int score)
    {
        return score.ToString().PadLeft(8, '0');
    }
    public void IncreaseScore(int _score)
    {
        if (scoreSoundFX != null)
        {
            audioSource.PlayOneShot(scoreSoundFX);
        }
        currentScore += _score;
        scoreText.text = FormatScore(currentScore); //fill remain string with char
    }
    public int GetScore()
    {
        return currentScore;
    }
    private PlayerData GetPlayerData()
    {
        try
        {
            string path = Application.persistentDataPath + "/playerdata.json";

            if (File.Exists(path))
            {
                //read json
                string json = File.ReadAllText(path);

                //deserialize json
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);

                return data;
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message + " -> " + ex.StackTrace);
        }
        return null;
    }
}
