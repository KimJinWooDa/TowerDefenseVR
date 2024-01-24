using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Gold { get; private set; }

    public GameObject GameOverCanvas;
    public GameObject GameClearCanvas;

    private void Awake()
    {
        Instance = this;
        Gold = 10;
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
}
