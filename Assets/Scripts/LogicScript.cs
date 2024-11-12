using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private PlayerMovement player;


    // Update is called once per frame
    void Update()
    {


    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
