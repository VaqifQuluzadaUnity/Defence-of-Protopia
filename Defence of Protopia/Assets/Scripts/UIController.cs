using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Image _Healthbar;
    public Text _coinText;
    public GameObject _gameOverPanel;
    private GameManager _gameManagerInstance;
    // Start is called before the first frame update
    void Start()
    {
        _gameManagerInstance = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Healthbar.fillAmount = _gameManagerInstance.MaxBaseHealth/100f;
        _coinText.text = "Coin: " + _gameManagerInstance._currentCoinAmount.ToString();

        if (_gameManagerInstance.MaxBaseHealth <= 0)
        {
            _gameOverPanel.SetActive(true);
        }
    }
}
