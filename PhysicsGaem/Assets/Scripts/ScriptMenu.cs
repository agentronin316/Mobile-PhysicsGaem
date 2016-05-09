using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ScriptMenu : MonoBehaviour {

    public GameObject mainMenu;
    public GameObject creditsMenu;
    public GameObject levelSelection;
    public GameObject[] levelButtons;
    

    void Awake()
    {
        if (PlayerPrefs.GetString("HasPlayed") == "Yes")
        {
            ScriptLevelRatings.LoadData();
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].GetComponentInChildren<Image>().fillAmount = ScriptLevelRatings.Ratings[i] / 3f;
                if (i != levelButtons.Length - 1 && ScriptLevelRatings.Ratings[i] > 0)
                {
                    levelButtons[i + 1].GetComponent<Button>().interactable = true;
                }
            }
        }
        else
        {
            ScriptLevelRatings.Initialize();
            ScriptLevelRatings.SaveData();
            PlayerPrefs.SetString("HasPlayed", "Yes");
            for (int i = 0; i < levelButtons.Length; i++)
            {
                levelButtons[i].GetComponentInChildren<Image>().fillAmount = 0f;
            }
        }
        _MainMenu();
    }

    void OnApplicationPause()
    {
        ScriptLevelRatings.SaveData();
    }

    public void _Credits()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }

    public void _MainMenu()
    {
        creditsMenu.SetActive(false);
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void _LevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void _Quit()
    {
        ScriptLevelRatings.SaveData();
        Application.Quit();
    }

    public void _StartLevel(int levelNumber)
    {
        ScriptLevelRatings.currentLevel = levelNumber - 1;
        SceneManager.LoadScene(levelNumber);
    }

    public void _ResetProgress()
    {
        ScriptLevelRatings.Initialize();
        ScriptLevelRatings.SaveData();
    }
}