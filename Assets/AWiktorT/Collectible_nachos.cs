using UnityEngine;

public class Collectible_nachos : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collectible collected by player!"); 
            Game_manager_nachos.Instance.AddPoint(); 
            Destroy(gameObject); 
        }
        else if (other.CompareTag("Ground"))
        {
            Debug.Log("Collectible destroyed by ground!"); 
            Destroy(gameObject);
        }
    }
}
