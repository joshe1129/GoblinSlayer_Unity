using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    #region Variables
         
    public static float general_volume = 1;
    

    #endregion

    #region Referencias

    public SaveData saveData;

    #endregion

    // Start is called before the first frame update
    /*private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }*/
    private void Awake()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            return;
        }
        else
        {
            switch (saveData.identifierSaveLoad)
            {
                case 1:
                    saveData.SpawnCharacterNewGame();
                    saveData.SpawnEnemiesNG();
                    break;
                case 2:
                    saveData.SpawnCharacterLoading();
                    saveData.SpawnEnemiesloading();
                    break;
                default:
                    break;
            }
        }


    }

    private void Start()
    {
        GameObject[] enemiesNumber = GameObject.FindGameObjectsWithTag("Enemy");
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ChangeScene(string scene)
    {
        if (scene == "Level_01")
        {
            Time.timeScale = 1;
        }
        SceneManager.LoadScene(scene);
    }
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void ChangeLoudness(float value)
    {
        foreach (AudioSource audioSource in GameObject.FindObjectsOfType<AudioSource>())
        {
            general_volume = value;
            audioSource.volume = value;
        }
    }
    public void ChangeLoudness(Slider slider)
    {
        ChangeLoudness(slider.value);
    }
    public void PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

}
