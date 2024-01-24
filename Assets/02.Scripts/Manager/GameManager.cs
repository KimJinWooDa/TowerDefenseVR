using UnityEngine;
using UnityEngine.SceneManagement;

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
    [Header("[State]")]
    public static GameManager Instance;
    public GameState State;
    public string CurrentScene;
    
    public int CurrentGold { get; private set; }
    public float CurrentTime  { get; private set; }
    public float[] TimeUntilNextLevel = {60,120,180,240,480};
    public float RequiredDurationToWin = 600f;

    [Space(10)]
    [Header("[UI]")]
    public GameObject GameOverCanvas;
    public GameObject GameClearCanvas;
    
    
    private int currentStage;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentStage = 0;
        CurrentGold = 10;
        State = GameState.Stage1;
    }

    public void GetGold(int gold)
    {
        CurrentGold += gold;
    }

    public void ConsumeGold(int gold)
    {
        CurrentGold -= gold;
    }

    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
    }

    public void GameClear()
    {
        GameClearCanvas.SetActive(true);
    }

    public void GoLobby()
    {
        SceneManager.LoadScene(0);
    }
    
    public void ReStart()
    {
        SceneManager.LoadScene(CurrentScene);
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
