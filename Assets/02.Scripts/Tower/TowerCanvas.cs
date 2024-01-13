using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI towerLevel;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI attackSpeed;
    [SerializeField] private TextMeshProUGUI numberOfTargets;
    [SerializeField] private TextMeshProUGUI cost;

    [SerializeField] private Button upgradeButton;

    private void Awake()
    {
        TowerController.OnUpdateCanvas += ChangeCanvas;
        TowerController.OnPopOffCanvas += PopOffCanvas;
    }

    private void ChangeCanvas(TowerState tower)
    {
        canvasGroup.alpha = 1f;

        towerLevel.text = $"현재 타워 레벨 : {tower.TowerLevel}";
        damage.text = $"공격력 : {tower.Damage}";
        attackSpeed.text = $"공격 속도 : {tower.AttackSpeed}";
        numberOfTargets.text = $"타켓팅 수 : {tower.NumberOfTargets}";
        
        cost.text = $"업그레이드 비용 : {tower.NextUpgradeCost}$";
    }

    private void PopOffCanvas()
    {
        canvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        TowerController.OnUpdateCanvas -= ChangeCanvas;
        TowerController.OnPopOffCanvas -= PopOffCanvas;
    }
}
