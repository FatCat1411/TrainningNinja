using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class NJ_GameController: MonoBehaviour
{
    private NJ_MainHUD mainHUD;
    private bool canUpdatePlayer =false;

    private static NJ_GameController current;
    public static NJ_GameController Current
    {
        private set { }
        get
        {
            if(null == current)
            {
                GameObject gameControllerObj = new GameObject("GameControllerObj");
                current = gameControllerObj.AddComponent<NJ_GameController>();
            }
            return current;
        }
    }

    private int playerScore;
    public int PlayerScore
    {
        private set
        {
            playerScore = value;
        }
        get
        {
            return playerScore;
        }
    }

    private void Awake()
    {
        Debug.Log("Something");
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if(FB.IsInitialized)
        {
            FB.ActivateApp();
        }
    }

    private void OnHideUnity(bool isGameShow)
    {

    }

    private NJ_GameController()
    {
    }

    private IEnumerator UpdateScore()
    {
        while(true == canUpdatePlayer)
        {
            yield return new WaitForSeconds(1);

            playerScore++;
            mainHUD.UpdateScore();
        }
    }

    public void OnGameOver()
    {
        canUpdatePlayer = false;

        MainCharacterController player = GameObject.FindObjectOfType<MainCharacterController>();
        if(null == player)
        {
            return;
        }
        player.OnDeath();
        mainHUD.OnDeath();

        WeaponDroppingGenerator generator = GameObject.FindObjectOfType<WeaponDroppingGenerator>();
        if(null == generator)
        {
            return;
        }
        generator.SetSpawnSetting(false);

        StopCoroutine(current.UpdateScore());

    }

    public void BeginGame()
    {
        canUpdatePlayer = true;
        mainHUD = GameObject.FindObjectOfType<NJ_MainHUD>();
        StartCoroutine(current.UpdateScore());

        MainCharacterController player = GameObject.FindObjectOfType<MainCharacterController>();
        if (null == player)
        {
            return;
        }
        player.OnBeginGame();

        WeaponDroppingGenerator generator = GameObject.FindObjectOfType<WeaponDroppingGenerator>();
        if (null == generator)
        {
            return;
        }
        generator.SetSpawnSetting(true);
        
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }

}
