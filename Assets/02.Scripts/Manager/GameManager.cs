using UnityEngine;

public enum GameState
{
    Stage1 = 0,
    Stage2 = 1,
    Stage3 = 2,
    Stage4 = 3,
    Stage5 = 4,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    
    public int Gold { get; private set; }
    public float CurrentTime  { get; private set; }
    public float[] TimeUntilNextLevel = {60,120,180,240,480};
    
    public GameObject GameOverCanvas;
    public GameObject GameClearCanvas;

    public float RequiredDurationToWin = 600f;

    private int currentStage;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentStage = 0;
        Gold = 10;
        State = GameState.Stage1;
    }

    public void GetGold(int gold)
    {
        Gold += gold;
    }

    public void ConsumeGold(int gold)
    {
        Gold -= gold;
    }

    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
    }

    public void GameClear()
    {
        GameClearCanvas.SetActive(true);
    }

    private void Update()
    {
        CurrentTime += Time.deltaTime;

        SetNextLevel();
        
        if (CurrentTime >= RequiredDurationToWin)
        {
            GameClear();
        }
    }

    private void SetNextLevel()
    {
        if (currentStage == (int)(GameState.Stage5))
        {
            return;
        }
        
        if (CurrentTime > TimeUntilNextLevel[currentStage])
        {
            currentStage++;
            State = (GameState)currentStage;
        }
    }
}
