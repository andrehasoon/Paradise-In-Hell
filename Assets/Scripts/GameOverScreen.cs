using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{

    public Text timeSurvived;
    public void Setup(float score){
        gameObject.SetActive(true);
        timeSurvived.text = "Survived for " + string.Format("{0:.00}", score) + " seconds";
    }

    public void MainMenuButton(){
        SceneManager.LoadScene("Main Menu");
    }
}
