using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameMenuScirpt : MonoBehaviour
{
    public static bool GameIsPaused = false;

    PlayerInfo Info;

    public GameObject MenuUI;
    // Start is called before the first frame update
    void Start()
    {
        GameObject PlayerInfo = GameObject.Find("PlayerInfo");
        Info = PlayerInfo.GetComponent<PlayerInfo>();
        GameIsPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1;
        GameIsPaused = false;
    }

    void Pause()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
}
