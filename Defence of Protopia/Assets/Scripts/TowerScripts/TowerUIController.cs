using UnityEngine;
using UnityEngine.UI;

//Vaqif Quluzada
public class TowerUIController : MonoBehaviour
{
    #region Public Variables

    [SerializeField]private Text LevelText;

    [SerializeField]private Text DamageText;

    [SerializeField]private Text AttackSpeedText;

    [SerializeField] private Button _upgradeButton;

    #endregion


    #region Public Methods

    public void UpgradeUIStats(int currentLevel,float currentDamage,float currentAttackSpeed,bool isMax)
    {
        
            LevelText.text = "Level:" + currentLevel.ToString();

            DamageText.text = "Damage:" + currentDamage.ToString();

            AttackSpeedText.text = "Attack speed:" + currentAttackSpeed.ToString();
        if (isMax)
        {
            _upgradeButton.interactable = false;
            _upgradeButton.GetComponentInChildren<Text>().text = "Max level";
        }
    }

    #endregion
}
