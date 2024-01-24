using TMPro;
using UnityEngine;

public class GameManagerUI : MonoBehaviour
{
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI CurrentTimeText;
    public TextMeshProUGUI TimeUntilNextStepText;
    
    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        GoldText.text = "골드 : " + GameManager.Instance.Gold;
        //CurrentTimeText.text = "흐른 시간 : " + GameManager.Instance.CurrentTime.ToString("F0"); 
        TimeUntilNextStepText.text =
            "다음 단계까지 : " + ((int)GameManager.Instance.TimeUntilNextLevel[(int)GameManager.Instance.State] - GameManager.Instance.CurrentTime).ToString("F0");// F0는 소수점 이하 0자리까지 표시
    }
}
