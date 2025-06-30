using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject pauseGameMenu;
    //[SerializeField] private GameObject gameOverMenu;
    public GameObject gameOverMenu;
    [SerializeField] private GameObject nextLevelMenu;

    [Header("GameOver Sub Menu")] 
    [SerializeField] private GameObject contPanel;
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Image contImg;

    private float waitTime = 3.3f;
    private int score;
    private bool isShowingCont;
    private int levelsToWaitBeforeShowingCompulsoryAd = 3;
    private bool hasDiedThisLevel;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        //AdsManager.instance.HideBanner();
        ////ADManager.instance.ShowBannerAd(false);
        ScoreSystem.instance.ScoreCarryOver();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (pauseGameMenu.activeSelf || gameOverMenu.activeSelf || nextLevelMenu.activeSelf)
        {
            //AdsManager.instance.ShowBanner();
            ////ADManager.instance.ShowBannerAd(true);//true to hide banner

        }
        else
        {
            //AdsManager.instance.HideBanner();
            ////ADManager.instance.ShowBannerAd(false);//true to hide banner
        }

        if (isShowingCont)
        {
            contImg.fillAmount -= 1.0f / waitTime * Time.deltaTime;
        }
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "" + score;
    }

    public void RestartGame()
    {
        StartCoroutine("restartgame");
        hasDiedThisLevel = true;
    }
    
    IEnumerator restartgame()
    {
        yield return new WaitForSeconds(0.40f);
        //Invoke("ReloadGame",3f);
        gameOverMenu.SetActive(true);
        isShowingCont = true;
        StartCoroutine("ContinuePlay");
    }

    IEnumerator ContinuePlay()
    {
        yield return new WaitForSeconds(waitTime);
        isShowingCont = false;
        OnCancelCont();
    }

    public void HideGameOverMenu()
    {
        gameOverMenu.SetActive(false);
    }

    public void CancelContinueOption()
    {
        StopCoroutine("ContinuePlay");
        //AdsManager.instance.ShowInterstitialAd();
        int chance = Random.Range(0, 5);
        switch (chance)
        {
            case 0:
                break;
            case 1:
                //ADManager.instance.ShowInterstitialAd(OnAdClosed);
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                //ADManager.instance.ShowInterstitialAd(OnAdClosed);
                break;
        }
        OnCancelCont();
    }

    void OnCancelCont()
    {
        contPanel.SetActive(false);
        scorePanel.SetActive(true);
    }
    public void NextLevel()
    {
        //Invoke("ReloadGame",3f);
        if (SaveManager.instance.CompletedLevelsCount() % levelsToWaitBeforeShowingCompulsoryAd == 0)
        {
            //Display Interstial Ad after each 3 levels completed
            //ADManager.instance.ShowInterstitialAd(OnAdClosed);
            //AdsManager.instance.ShowInterstitialAd();
        }
        nextLevelMenu.SetActive(true);
    }

    public void ShowRewardedAdd()
    {
        //AdsManager.instance.ShowVideoRewardAd();
        //ADManager.instance.ShowRewardedVideoAd(OnRewardedAdClosed);
    }
    
    public void ReloadGame()
    {
        SaveManager.instance.SetHasDiedPreviousLevel(hasDiedThisLevel);
        //AdsManager.instance.DestroyBanner();
        //ADManager.instance.ShowBannerAd(false,true);
        SceneManager.LoadScene("Gameplay");
        //ScoreSystem.instance.ScoreCarryOver();
    }

    public void LoadMainMenu()
    {
        //AdsManager.instance.DestroyBanner();
        //ADManager.instance.ShowBannerAd(false,true);
        ScoreSystem.instance.ScoreCarryOver();
        SceneManager.LoadScene(0);
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        pauseGameMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        pauseGameMenu.SetActive(false);
    }

    public void TutorialClose()
    {
        tutorialPanel.SetActive(false);
    }

    private void OnAdClosed(ShowResult result)
    {
        //Regular Ad Closed
    }

    private void OnRewardedAdClosed(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Rewarded
                HideGameOverMenu();
                PlayerInteraction.instance.SetPlayerDeadState(false);
                PlayerInteraction.instance.SetPlayerPosition();
                hasDiedThisLevel = false;
                break;
            case ShowResult.Skipped:
                ////ADManager.instance.S
                break;
            case ShowResult.Failed:
                //RequestRewardedVideo();
                OnCancelCont();
                //CancelContinueOption();
                break;
        }
    }
}
