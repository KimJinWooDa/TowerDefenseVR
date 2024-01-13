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
            OnUpdateCanvas?.Invoke(TowerStates[(int)CurrentLevel]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPopOffCanvas?.Invoke();
        }
    }
    
    public int buttonWidth = 100;
    public int buttonHeight = 50;
    public int margin = 10;

    void OnGUI()
    {
        float xPosition = Screen.width - buttonWidth - margin;
        float yPosition = margin;

        Rect buttonRect = new Rect(xPosition, yPosition, buttonWidth, buttonHeight);

        if (GUI.Button(buttonRect, "Upgrade"))
        {
            Upgrade();
        }
    }
}
