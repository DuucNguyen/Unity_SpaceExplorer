using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public InputField txtName;

    public GameObject newGameConfirmationPanel;
    public GameObject mainMenuUI;
    public GameObject playerDataUI;

    public Text errorMessage;
    public Text playerName;
    public Text playerScore;
    public Button displayLevel1;
    public Button displayLevel2;
    public Button displayLevel3;

    public AudioSource audioSource;
    public AudioClip mainMenuTheme;

    private bool valid;
    public void NewGame() //agree button clicked
    {
        PlayerData data = new PlayerData();
        if (!string.IsNullOrEmpty(txtName.text))
        {
            if (txtName.text.Length > 25)
            {
                errorMessage.text = "Name must be less than 25 characters !";
            }
            else
            {
                playerName.text = txtName.text;
                valid = true;
            }
        }
        else
        {
            errorMessage.text = "Enter your name first !";
        }

        if (valid)
        {
            data.Name = txtName.text;
            data.Score = 0;
            data.IsNew = true;

            string path = Application.persistentDataPath + "/playerdata.json";

            // convert object to json
            string json = JsonUtility.ToJson(data);

            // write to a file
            File.WriteAllText(path, json);

            Debug.Log("Save new data at: " + path);

            //LoadScene Level1
            SceneManager.LoadScene("Level1");
        }
    }

    public void LoadPlayerData()// continues button from main menu clicked
    {
        string path = Application.persistentDataPath + "/playerdata.json";
        if (File.Exists(path))
        {
            // read json
            string json = File.ReadAllText(path);

            // deserialize json
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            if (!data.IsNew)
            {
                playerDataUI.SetActive(true);
                mainMenuUI.SetActive(false);

                playerName.text = data.Name;
                playerScore.text = data.Score.ToString();

                UpdateStatusButton(displayLevel1, data.UnlockLevel1);
                UpdateStatusButton(displayLevel2, data.UnlockLevel2);
                UpdateStatusButton(displayLevel3, data.UnlockLevel3);
            }
            else
            {
                //display panel to inform no data
            }
        }
        else
        {
            Debug.Log("Save file not found!");
        }
    }

    private void UpdateStatusButton(Button button, bool status)
    {
        button.interactable = false;

        Image buttonImage = button.gameObject.GetComponent<Image>();

        buttonImage.color = status ? Color.white : Color.black;
    }

    public void LoadConfirmationPanel() //new game button clicked
    {
        errorMessage.text = "";
        newGameConfirmationPanel.SetActive(true);
        mainMenuUI.SetActive(false);
        playerDataUI.SetActive(false);
    }
    public void LoadMainMenuPanel() //back button click
    {
        mainMenuUI.SetActive(true);
        newGameConfirmationPanel.SetActive(false);
        playerDataUI.SetActive(false);
    }

    public void Continues() //continues button from playerData clicked
    {
        //LoadScene Level1
        SceneManager.LoadScene("Level1");
    }

    public void Quit() // quit button click
    {
        UnityEditor.EditorApplication.isPlaying = false;
        //Application.Quit(); // use when export 
    }


    [System.Serializable] // use to save as object
    public class PlayerData
    {
        public PlayerData() //only use when create new data
        {
            UnlockLevel1 = true;
            UnlockLevel2 = false;
            UnlockLevel3 = false;

            //IsNew = true;
        }

        public string Name;
        public int Score;

        public bool UnlockLevel1;
        public bool UnlockLevel2;
        public bool UnlockLevel3;

        public bool IsNew;
    }

}
