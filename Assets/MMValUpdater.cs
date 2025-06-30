using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MMValUpdater : MonoBehaviour
{

    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI levelCompletedText;
    public TextMeshProUGUI highScoreText;

    public Slider SFXSlider;
    public Slider BGMSlider;

    public AudioSource bgm;
    // Start is called before the first frame update
    void Start()
    {
        //ADManager.instance.ShowBannerAd(false);
    }

    
    // Update is called once per frame
    void Update()
    {
        coinsText.text = SaveManager.instance.CoinsCount().ToString();
        levelCompletedText.text = $"Lvl:{SaveManager.instance.CompletedLevelsCount()}";
        highScoreText.text = $"HS:{SaveManager.instance.GetHighScore()}";
        SFXSlider.value = SaveManager.instance.GetSFXLevel();
        BGMSlider.value = SaveManager.instance.GetBGMLevel();
    }

    public void OnSfxSliderValueChanged(float value)
    {
        SaveManager.instance.SaveSFXLevel(value);
    }
    public void OnBgmSliderValueChanged(float value)
    {
        SaveManager.instance.SaveBGMLevel(value);
        bgm.volume = value;
    }
}
