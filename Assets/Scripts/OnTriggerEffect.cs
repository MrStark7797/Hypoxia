using UnityEngine;

public class OnTriggerEffect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject prefabLooted; // Assign "Corpse_Looted" prefab here
    public float oxygenRestoreAmount = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTrigger");
        if (other.CompareTag("Player"))
        {
            PlayerScript player = other.GetComponent<PlayerScript>();
            if (player != null)
            {
                player.AddOxygen(oxygenRestoreAmount);

                if (prefabLooted != null)
                {
                    Instantiate(prefabLooted, transform.position, Quaternion.identity, transform.parent);
                }
                Destroy(gameObject);
            }
        }
    }

    private void Loot()
    {
        // 3. Spawn the looted version at the same position
        if (prefabLooted != null)
        {
            Instantiate(prefabLooted, transform.position, transform.rotation, transform.parent);
        }

        // 4. Remove the unlooted oxygen object
        Destroy(gameObject);
    }
}
