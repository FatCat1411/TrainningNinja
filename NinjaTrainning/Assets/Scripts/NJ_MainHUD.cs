using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NJ_MainHUD : MonoBehaviour
{
    public Text txtScore;
    public GameObject panelGameOver;
    public Text txtGameOverScore;

	// Use this for initialization
	void Start ()
    {
        NJ_GameController.Current.BeginGame();

        panelGameOver.SetActive(false);
    }
	
    public void UpdateScore()
    {
        txtScore.text = "Score: " + NJ_GameController.Current.PlayerScore;
    }

    public void OnDeath()
    {
        panelGameOver.SetActive(true);
        txtGameOverScore.text = "You have score " + NJ_GameController.Current.PlayerScore + " points!!!";
    }

    public void OnBtnTryAgain_Clicked()
    {
        NJ_GameController.Current.TryAgain();
    }
}
