using UnityEngine;
using TMPro; 

public class CoinCounter : MonoBehaviour
{
    public int totalCoins = 0;
    public TMP_Text coinText; 

    public GameObject explosion;
    void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoins(int amount)
    {
        totalCoins += amount;
        UpdateCoinUI();

        if (totalCoins >= 5)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Debug.Log("Explosion instantiated at " + transform.position);
        }
    }

    void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + totalCoins;
    }
}
