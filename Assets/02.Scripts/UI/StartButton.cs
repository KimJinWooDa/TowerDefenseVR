using AutoSet.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField, AutoSet] private Button button;
    public string NextScene;
    private void Start()
    {
        button.onClick.AddListener(()=>SceneManager.LoadScene(NextScene));
    }
    
}
