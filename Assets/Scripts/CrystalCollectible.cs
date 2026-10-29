using UnityEngine;

public class CrystalCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CollectCrystal();
            Destroy(gameObject);
        }
    }
}
