using UnityEngine;

public class spawnArmor : MonoBehaviour
{
    [SerializeField] private GameObject armorPrefab;

    private GameObject currentFollower;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //spawning the armor for the player
        if (other.tag == "Player")
        {
            GameObject spawnedPrefab = Instantiate(armorPrefab, other.transform.position, Quaternion.identity);

            spawnedPrefab.transform.SetParent(other.transform);

            spawnedPrefab.transform.localPosition = Vector3.zero;

            
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
