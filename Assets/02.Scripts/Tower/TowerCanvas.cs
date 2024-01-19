using System;
using AutoSet.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerCanvas : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private TextMeshProUGUI towerLevel;
    [SerializeField] private TextMeshProUGUI damage;
    [SerializeField] private TextMeshProUGUI attackSpeed;
    [SerializeField] private TextMeshProUGUI cost;

    [SerializeField] private Button upgradeButton;
    [SerializeField, AutoSetFromParent] private TowerController towerController;
    
    private void Start()
    {
        if (towerController != null)
        {
            towerController.OnUpdateCanvas += ChangeCanvas;
            towerController.OnPopOffCanvas += PopOffCanvas;
        }
    }

    private void ChangeCanvas(TowerState tower)
    {
        canvasGroup.alpha = 1f;

        towerLevel.text = $"현재 타워 레벨 : {tower.TowerLevel}";
        damage.text = $"공격력 : {tower.Damage}";
        attackSpeed.text = $"공격 속도 : {tower.AttackSpeed}";
        
        cost.text = $"업그레이드 비용 : {tower.NextUpgradeCost}$";
    }

    private void PopOffCanvas()
    {
        canvasGroup.alpha = 0f;
    }

    private void OnDestroy()
    {
        if (towerController != null)
        {
            towerController.OnUpdateCanvas -= ChangeCanvas;
            towerController.OnPopOffCanvas -= PopOffCanvas;
        }
    }
}
