using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static MainMenuManager;

public class GameManager : MonoBehaviour
{
    public GameObject gameOpeningPanel;
    public GameObject player;
    public GameObject asteroidSpawner;
    public GameObject bonusSpawner;
    public GameObject enemySpawner;

    public Button Level1Button; //list buttons 
    public Button Level2Button; //list buttons 
    public Button Level3Button; //list buttons 

    public Text playerName;
    public Text playerScore;
    public InputField txtName;

    public GameObject gameOverPanel;
    public Text gameOverPlayerName;
    public Text gameOverPlayerScore;
    public Text gameOverResult;
    public Toggle gameOverVerify;
    public Button gameOverNextLevelButton;
    public GameManagerState State;

    public AudioSource audioSource;
    public AudioClip gameOverClip;
    public AudioClip congratulationClip;

    public enum GameManagerState
    {
        Opening,
        GamePLay,
        GameOver,
    }

    private PlayerData data;


    // Start is called before the first frame update
    void Start()
    {
        //initiate default state
        State = GameManagerState.Opening;

        //initiate data
        data = GetPlayerData();

        //initiate level buttons
        UpdateStatusButton(Level1Button, data.UnlockLevel1);
        UpdateStatusButton(Level2Button, data.UnlockLevel2);
        UpdateStatusButton(Level3Button, data.UnlockLevel3);

        //add event to buttons
        InitiateLevelButtons();


        if (data != null)
        {
            txtName.text = data.Name;
            playerName.text = data.Name;
            playerScore.text = FormatScore(data.Score);
        }
    }

    void Update()
    {
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

    void UpdateGameManagerState()
    {
        switch (State)
        {
            case GameManagerState.Opening:
                {
                    Debug.Log("Opening");
                    //Hide game over
                    gameOverPanel.SetActive(false);

                    //Prevent spawn before start
                    asteroidSpawner.GetComponent<AsteroidSpawner>().StopSpawnAsteroid();
                    bonusSpawner.GetComponent<BonusSpawner>().StopSpawnBonus();
                    if (enemySpawner != null)
                    {
                        enemySpawner.GetComponent<EnemySpawner>().StopSpawnEnemy();
                    }

                    //active play button
                    gameOpeningPanel.SetActive(true);
                    break;
                }
            case GameManagerState.GamePLay:
                {
                    Debug.Log("Start");

                    gameOpeningPanel.SetActive(false);

                    player.GetComponent<PlayerMovement>().Init();

                    asteroidSpawner.GetComponent<AsteroidSpawner>().StartSpawnAsteroid();
                    bonusSpawner.GetComponent<BonusSpawner>().StartSpawnBonus();

                    if (enemySpawner != null)
                    {
                        enemySpawner.GetComponent<EnemySpawner>().StartSpawnEnemy();
                    }

                    break;
                }
            case GameManagerState.GameOver:
                {
                    Debug.Log("Game Over");
                    var score = player.GetComponent<PlayerScore>().GetScore();

                    gameOverPlayerName.text = txtName.text;
                    gameOverPlayerScore.text = FormatScore(score);

                    //update score to save
                    data.Score = score;

                    var currentScene = SceneManager.GetActiveScene().name;
                    var currentLevel = int.Parse(currentScene[currentScene.Length - 1].ToString());

                    if (currentLevel == 1 && score >= 1000)
                    {
                        audioSource.PlayOneShot(congratulationClip);

                        gameOverVerify.isOn = true;
                        data.UnlockLevel2 = true;
                        gameOverResult.text = "LEVEL COMPLETED !";
                        gameOverResult.color = Color.green;
                    }
                    else if (currentLevel == 2 && score >= 3000)
                    {
                        audioSource.PlayOneShot(congratulationClip);

                        gameOverVerify.isOn = true;
                        data.UnlockLevel3 = true;
                        gameOverResult.text = "LEVEL COMPLETED !";
                        gameOverResult.color = Color.green;
                    }
                    else if(currentLevel == 3)
                    {
                        audioSource.PlayOneShot(congratulationClip);

                        gameOverResult.text = "CONGRATULATION !";
                        gameOverResult.color = Color.green;
                    }
                    else
                    {
                        //game over case
                        audioSource.PlayOneShot(gameOverClip);
                        gameOverResult.text = "GAME OVER";
                        gameOverResult.color = Color.red;
                    }

                    //save new data
                    SaveData();

                    gameOverNextLevelButton.interactable = gameOverVerify.isOn;

                    //display game over
                    gameOverPanel.SetActive(true);

                    //stop object spawning
                    asteroidSpawner.GetComponent<AsteroidSpawner>().StopSpawnAsteroid();
                    bonusSpawner.GetComponent<BonusSpawner>().StopSpawnBonus();

                    if (enemySpawner != null)
                    {
                        enemySpawner.GetComponent<EnemySpawner>().StopSpawnEnemy();
                    }

                    ////change state to Opening 
                    //Invoke("ChangeToOpeningState", 20f);
                    break;
                }
        }
    }

    private void SaveData()
    {
        try
        {
            if (data != null)
            {
                string path = Application.persistentDataPath + "/playerdata.json";

                // convert object to json
                string json = JsonUtility.ToJson(data);

                // write to a file
                File.WriteAllText(path, json);

                Debug.Log("Save data at: " + path);
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log("Save data ERROR: " + ex.Message + " -> " + ex.StackTrace);
        }
    }

    private string FormatScore(int score)
    {
        return score.ToString().PadLeft(8, '0');
    }

    private void UpdateStatusButton(Button button, bool status)
    {
        button.interactable = status;

        Image buttonImage = button.gameObject.GetComponent<Image>();

        buttonImage.color = status ? Color.white : Color.black;
    }

    private void InitiateLevelButtons()
    {
        var buttons = new List<Button>();
        buttons.Add(Level1Button);
        buttons.Add(Level2Button);
        buttons.Add(Level3Button);


        foreach (var button in buttons)
        {
            button.onClick.AddListener(() => LoadLevel(button));
        }
    }

    private void LoadLevel(Button button)
    {
        switch (button.name)
        {
            case "Level1":
                {
                    SceneManager.LoadScene("Level1");
                    break;
                }
            case "Level2":
                {
                    SceneManager.LoadScene("Level2");
                    break;
                }
            case "Level3":
                {
                    SceneManager.LoadScene("Level3");
                    break;
                }
        }
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    public void SetGameManagerState(GameManagerState _state)
    {
        State = _state;
        UpdateGameManagerState();
    }

    //Play button call
    public void StartGamePlay()
    {
        State = GameManagerState.GamePLay;
        UpdateGameManagerState();

        if (data != null)
        {
            data.IsNew = false;
        }

        SaveData();
    }

    //Next Level button call
    public void LoadNextLevel()
    {
        var currentScene = SceneManager.GetActiveScene().name;
        var currentLevel = int.Parse(currentScene[currentScene.Length - 1].ToString());

        SceneManager.LoadScene("Level" + (currentLevel + 1));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
