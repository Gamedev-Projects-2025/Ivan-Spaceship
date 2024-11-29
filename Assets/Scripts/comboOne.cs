using UnityEngine;

public class comboOne : MonoBehaviour
{
    [SerializeField] protected GameObject prefabToSpawn;
    [SerializeField] protected Vector3 velocityOfSpawnedObject;
    [SerializeField] private GameObject source;
    public void PerformAction()
    {
        spawnObject();
    }

    //spans a prefab for the combo
    public GameObject spawnObject()
    {
        Vector3 positionOfSpawnedObject = source.transform.position;
        Quaternion rotationOfSpawnedObject = Quaternion.identity;
        GameObject newObject = Instantiate(prefabToSpawn, positionOfSpawnedObject, rotationOfSpawnedObject);

        Mover newObjectMover = newObject.GetComponent<Mover>();
        if (newObjectMover)
        {
            newObjectMover.SetVelocity(velocityOfSpawnedObject);
        }

        return newObject;
    }
}
