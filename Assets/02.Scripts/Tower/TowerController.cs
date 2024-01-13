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
    public static Action<TowerState> OnUpdateCanvas;
    public static Action OnPopOffCanvas;
    
    public TowerState[] TowerStates;
    public GameObject[] Towers;
    public TowerLevel CurrentLevel;
    public TowerLevel NextLevel;

    public ParticleSystem ParticleSystem;

    private void Awake()
    {
        CurrentLevel = TowerLevel.Level0;
        NextLevel = TowerLevel.Level1;
    }

    public void Upgrade()
    {
        if((int)CurrentLevel >= Towers.Length - 1)
        {
            return;
        }
        
        ParticleSystem.Play();
        
        Towers[(int)CurrentLevel].SetActive(false);
        Towers[(int)NextLevel].SetActive(true);

        CurrentLevel = NextLevel;
        NextLevel = CurrentLevel + 1;
        
        OnUpdateCanvas?.Invoke(TowerStates[(int)CurrentLevel]);
    }
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Towers[(int)CurrentLevel].SetActive(true);
            OnUpdateCanvas?.Invoke(TowerStates[(int)CurrentLevel]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CurrentLevel == TowerLevel.Level0)
            {
                Towers[(int)CurrentLevel].SetActive(false);
            }
           
            OnPopOffCanvas?.Invoke();
        }
    }
    
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
}
