using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TMP_Text coinsText;
    public TMP_Text labelText;
    public GameObject eventWindow;

    public enum UIType
    {
        GameOver,
        Win
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UIEvent(UIType type)
    {
        eventWindow.SetActive(true);
        switch (type)
        {
            case UIType.GameOver:
                labelText.text = "Game Over";
                break;
            case UIType.Win:
                labelText.text = "Win";
                break;
            default:
                break;
        }
    }
}
