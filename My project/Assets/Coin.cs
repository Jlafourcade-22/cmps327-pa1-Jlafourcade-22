using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1; // How much this coin is worth

    private void OnTriggerEnter(Collider other)
    {
        CoinCounter playerCoins = other.GetComponent<CoinCounter>();
        if (playerCoins != null)
        {
            playerCoins.AddCoins(coinValue);
        }

        Destroy(gameObject);
    
    }
}
