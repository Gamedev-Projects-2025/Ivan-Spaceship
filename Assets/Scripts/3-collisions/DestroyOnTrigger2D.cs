using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

/**
 * This component destroys its object whenever it triggers a 2D collider with the given tag.
 */
public class DestroyOnTrigger2D : MonoBehaviour {
    [Tooltip("Every object tagged with this tag will trigger the destruction of this object")]
    [SerializeField] string triggeringTag;
    [SerializeField] private int health = 1;
    private int hits = 0;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == triggeringTag && enabled) {
            hits++;
            if (health <= hits)
            {
                Destroy(this.gameObject);
                Destroy(other.gameObject);

            }
            else
            {
                Destroy(other.gameObject);
            }


        }

    }

    private void Start()
    {

    }

    private void Update() {
        /* Just to show the enabled checkbox in Editor */
    }
}
