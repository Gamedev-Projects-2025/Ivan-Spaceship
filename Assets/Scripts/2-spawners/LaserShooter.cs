using UnityEngine;

/**
 * This component spawns the given laser-prefab whenever the player clicks a given key.
 * It also updates the "scoreText" field of the new laser.
 */
public class LaserShooter: ClickSpawner {
    [SerializeField]
    [Tooltip("How many points to add to the shooter, if the laser hits its target")]

    // A reference to the field that holds the score that has to be updated when the laser hits its target.
    NumberField scoreField;  

    private void Start() {
        if (tag == "Player")
        {
            scoreField = GetComponentInChildren<NumberField>();
        }
    }

    protected override GameObject spawnObject() {
        GameObject newObject = base.spawnObject();  // base = super

        // Modify the text field of the new object.
        ScoreAdder newObjectScoreAdder = newObject.GetComponent<ScoreAdder>();
        if (newObjectScoreAdder)
            newObjectScoreAdder.SetScoreField(scoreField);

        return newObject;
    }
}
