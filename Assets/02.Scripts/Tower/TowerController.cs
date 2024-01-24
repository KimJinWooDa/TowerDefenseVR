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
    public event Action OnHideCanvas;
    
    [Header("[Tower State]")]
    public TowerState[] TowerStates;
    public TowerState CurrentTowerState => TowerStates[(int)CurrentLevel];
    public GameObject[] Towers;
    
    [Space(10)]
    [Header("[Tower]")]
    public TowerLevel CurrentLevel;
    public TowerLevel NextLevel;
    public TowerLevel MaxTowerLevel = TowerLevel.Level3;
    
    [Space(10)]
    [Header("[FX]")]
    public ParticleSystem ParticleSystem;
    public AudioSource UpgradeSound;
    
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
        if(CurrentLevel >= MaxTowerLevel)
        {
            return;
        }

        if (GameManager.Instance.CurrentGold < TowerStates[(int)CurrentLevel].NextUpgradeCost)
        {
            return;
        }
        
        GameManager.Instance.ConsumeGold(TowerStates[(int)CurrentLevel].NextUpgradeCost);
        UpgradeSound.Play();
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
           
            OnHideCanvas?.Invoke();
        }
    }
}
