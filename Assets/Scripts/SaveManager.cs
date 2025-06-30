using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { set; get; }
    public SaveState state;

    private void Awake()
    {
        //ResetSave();
        DontDestroyOnLoad(gameObject);
        instance = this;
        Load();

        //Debug.Log(SaveHelper.Serialize<SaveState>(state));
//        Debug.Log(IsPlayerOwned(0));
//        UnlockPlayer(2);
//        Debug.Log(IsPlayerOwned(2));
    }

    private void Start()
    {
        UnlockPlayer(0);
    }

    public void Save()
    {
        PlayerPrefs.SetString("save",SaveHelper.Serialize<SaveState>(state));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("save"))
        {
            state = SaveHelper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No Save File Found Creating a new one");
        }
    }

    public void SaveCoins(int val)
    {
        state.coins += val;
        Save();
    }

    public int CoinsCount()
    {
        return state.coins;
    }

    public void SetCurrentPlayer(int index)
    {
        state.currentPlayer = index;
        Save();
    }
    
    public int CurrentlySelectedPlayer()
    {
        return state.currentPlayer;
    }
    public int CompletedLevelsCount()
    {
        return state.completedLevels;
    }
    
    public void SaveLevels(int val)
    {
        state.completedLevels += val;
        Save();
    }
    public bool IsPlayerOwned(int index)
    {
        return (state.playerOwned & (1 << index)) != 0;
    }

    public bool BuyPlayer(int index, int cost)
    {
        if (state.coins >= cost)
        {
            state.coins -= cost;
            UnlockPlayer(index);
            
            Save();

            return true;
        }
        else
        {
            return false;
        }
    }
    public void UnlockPlayer(int index)
    {
        //Toggle on bit at index
        //https://www.youtube.com/watch?v=FRKYVhxzrRw
        state.playerOwned |= 1 << index;
    }
    
    public void SaveSFXLevel(float val)
    {
        state.sfxVolLvl = val;
        Save();
    }

    public float GetSFXLevel()
    {
        return state.sfxVolLvl;
    }
    
    public void SaveBGMLevel(float val)
    {
        state.bgmVolLvl = val;
        Save();
    }

    public float GetBGMLevel()
    {
        return state.bgmVolLvl;
    }

    public void SetHighScore(int score)
    {
        state.highScore = score;
        Save();
    }
    
    public int GetHighScore()
    {
        return state.highScore;
    }
    
    public void SetCurrentScoreFromPreviousLevel(int score)
    {
        state.currentScoreFromPreviousLevel = score;
        Save();
    }
    
    public int GetCurrentScoreFromPreviousLevel()
    {
        return state.currentScoreFromPreviousLevel;
    }

    public bool GetHasDiedPreviousLevel()
    {
        return state.hasDiedPreviousLevel;
    }

    public void SetHasDiedPreviousLevel(bool val)
    {
        state.hasDiedPreviousLevel = val;
        Save();
    }
    
    //Reset Save File
    public void ResetSave()
    {
        PlayerPrefs.DeleteKey("save");
        state.coins = 0;
        state.completedLevels = 0;
        state.currentPlayer = 0;
        state.playerOwned = 1;
        state.sfxVolLvl = 1.0f;
        state.bgmVolLvl = 0.2f;
        state.highScore = 0;
        state.currentScoreFromPreviousLevel = 0;
        state.hasDiedPreviousLevel = false;
    }
}
