using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] public GameObject deathPanel;
    [SerializeField] public GameObject winPanel;
    
    private float timer = 2f;
    private bool isWin;
    public void GameOver()
    {
        if (PlayerController.Instance.isDeath )
        {
            StartCoroutine(WaitForDeath());
        }
    }

    public void WinGame()
    {
        isWin = true;
        StartCoroutine(WaitForDeath());
    }
    
    IEnumerator WaitForDeath()
    {
        yield return new WaitForSeconds(timer);

        if (isWin)
            winPanel.SetActive(!winPanel.activeSelf);
        else
        deathPanel.SetActive(!deathPanel.activeSelf);
        
    }
}

