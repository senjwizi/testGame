using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    GameManager gameManager;
    UI UI;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        UI = FindObjectOfType<UI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Coin")
        { 
            gameManager.AddCoin(collision.gameObject);
        }

        if (collision.transform.tag == "Spike")
        {
            gameManager.Event(UI.UIType.GameOver);
        }
    }
}
