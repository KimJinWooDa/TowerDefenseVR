using System;
using UnityEngine;

public enum TowerLevel
{
    Level0 = 0,
    Level1 = 1,
    Level2 = 2,
    Level3 = 3,
}

public class TowerController : MonoBehaviour
{
    public event Action<TowerState> OnUpdateCanvas;
    public event Action OnPopOffCanvas;
    
    [Header("[Tower State]")]
    public TowerState[] TowerStates;
    public GameObject[] Towers;
    
    [Space(10)]
    [Header("[Tower ]")]
    public TowerLevel CurrentLevel;
    public TowerLevel NextLevel;

    [Space(10)]
    [Header("[Particle System]")]
    public ParticleSystem ParticleSystem;
    
    private const string PlayerTag = "Player";
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < Towers.Length; i++)
        {
            Towers[i].SetActive(false);
        }
        CurrentLevel = TowerLevel.Level0;
        NextLevel = TowerLevel.Level1;
    }

    public void Upgrade()
    {
        if((int)CurrentLevel >= Towers.Length - 1 || GameManager.Instance.Gold < TowerStates[(int)CurrentLevel].NextUpgradeCost)
        {
            return;
        }
        
        GameManager.Instance.ConsumeGold(TowerStates[(int)CurrentLevel].NextUpgradeCost);
        ParticleSystem.Play();
        
        Towers[(int)CurrentLevel].SetActive(false);
        Towers[(int)NextLevel].SetActive(true);

        CurrentLevel = NextLevel;
        NextLevel = CurrentLevel + 1;
        
        OnUpdateCanvas?.Invoke(TowerStates[(int)CurrentLevel]);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            if (CurrentLevel == TowerLevel.Level0)
            {
                Towers[(int)CurrentLevel].SetActive(true);
            }
            OnUpdateCanvas?.Invoke(TowerStates[(int)CurrentLevel]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(PlayerTag))
        {
            if (CurrentLevel == TowerLevel.Level0)
            {
                Towers[(int)CurrentLevel].SetActive(false);
            }
           
            OnPopOffCanvas?.Invoke();
        }
    }
    
    #if UNITY_EDITOR
    void OnGUI()
    {
        float xPosition = Screen.width - 100 - 10;
        float yPosition = 10;

        Rect buttonRect = new Rect(xPosition, yPosition, 100, 50);

        if (GUI.Button(buttonRect, "Upgrade"))
        {
            Upgrade();
        }
    }
    #endif
}
