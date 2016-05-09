using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptControls : MonoBehaviour {

    public ScriptCanon cannon;
    public Button nextLevelButton;
    public GameObject gameplayUI;
    public GameObject resultsScreen;
    public Text resultsText;

    void Start()
    {
        gameplayUI.SetActive(true);
        resultsScreen.SetActive(false);
        if (ScriptLevelRatings.currentLevel == 5 || ScriptLevelRatings.GetCurrentLevelRating() == 0)
        {
            nextLevelButton.interactable = false;
        }
	    if (cannon == null)
        {
            cannon = GameObject.FindGameObjectWithTag("Cannon").GetComponent<ScriptCanon>();
        }
	}
	
	public void _PlusPower()
    {
        cannon.IncreasePower();
    }

    public void _MinusPower()
    {
        cannon.DecreasePower();
    }

    public void _PlusAngle()
    {
        cannon.IncreaseAngle();
    }

    public void _MinusAngle()
    {
        cannon.DecreaseAngle();
    }

    public void _FireCannon()
    {
        cannon.FireCannon();
    }

    public void _RestartLevel()
    {
        SceneManager.LoadScene(ScriptLevelRatings.currentLevel + 1);
    }

    public void _NextLevel()
    {
        ScriptLevelRatings.currentLevel++;
        _RestartLevel();
    }

    public void DisplayResults(int starRating)
    {
        gameplayUI.SetActive(false);
        resultsScreen.SetActive(true);
        Time.timeScale = 0;
        switch(starRating)
        {
            case 0:
                resultsText.text = "Try Again?";
                break;
            case 1:
                resultsText.text = "You Made it!";
                nextLevelButton.interactable = true;
                break;
            case 2:
                resultsText.text = "Congratulations!";
                nextLevelButton.interactable = true;
                break;
            case 3:
                resultsText.text = "Awesome Job!";
                nextLevelButton.interactable = true;
                break;
        }
        ScriptLevelRatings.Ratings[ScriptLevelRatings.currentLevel] = starRating;
    }
}
