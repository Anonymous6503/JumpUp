[System.Serializable]
public class SaveState
{
    public int coins = 0;
    public int completedLevels = 0;
    public int playerOwned = 0;

    public int currentPlayer = 0;

    public float sfxVolLvl = 1.0f;

    public float bgmVolLvl = 0.2f;
    
    public int highScore = 0;

    public int currentScoreFromPreviousLevel = 0;

    public bool hasDiedPreviousLevel = false;
    //Theme owned
}
