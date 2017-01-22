using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControlEndMenu : MonoBehaviour
{
    [SerializeField]
    public Transform endMenu;

    public Text winText;

    public void OpenMenu(int playerNumber)
    {
        endMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        winText.text = "Player " + playerNumber + " wins!";
    }

    public void LoadMainMenu()
    {
        endMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel(int levelId)
    {
        endMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(levelId);
    }
}
