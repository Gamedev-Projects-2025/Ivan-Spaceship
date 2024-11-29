using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

/**
 * This component destroys its object whenever it triggers a 2D collider with the given tag.
 */
public class PlayerHealth : MonoBehaviour {
    [Tooltip("Every object tagged with this tag will trigger the destruction of this object")]
    [SerializeField] string triggeringTag;
    [SerializeField] string triggeringHealthTag;
    [SerializeField] private int health = 1;
    [SerializeField][Tooltip("Name of scene to move to when triggering the given tag")] string sceneName = null;
    [SerializeField] private NumberField healthField;
    [SerializeField] private float invincibilityTime = 1f;
    private float lastSpawnTime = -Mathf.Infinity;


    private void OnTriggerEnter2D(Collider2D other) {
        //couting hits only if the happened after the invincibilityTime
        if (other.tag == triggeringTag && enabled && Time.time >= lastSpawnTime + invincibilityTime)
        {
            //time since last hit
            lastSpawnTime = Time.time;
            health--;
            if (health <= 0)
            {
                //game over
                Destroy(this.gameObject);
                Destroy(other.gameObject);
                if (!string.IsNullOrEmpty(sceneName))
                {
                    SceneManager.LoadScene(sceneName);
                }
            }
            else
            {
                //changing the alpha of the player to incidate that he has invincibility
                flash();
                Destroy(other.gameObject);
            }
            if (healthField != null)
            {
                healthField.SetNumber(health);
            }
        }
        else if (other.tag == triggeringHealthTag && enabled)
        {
            
            healthField.SetNumber(++health);
            Destroy(other.gameObject);
        }
        Debug.Log("Health");


    }

    private void Start()
    {
        if (healthField != null)
        {
            healthField.SetNumber(health);
        }
    }

    async void flash()
    {
        //changing the alpha of the player to incidate that he has invincibility
        Color tmp = gameObject.GetComponent<SpriteRenderer>().color;
        tmp.a = 0.5f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;

        await Awaitable.WaitForSecondsAsync(invincibilityTime);

        tmp.a = 1f;
        gameObject.GetComponent<SpriteRenderer>().color = tmp;
    }
}
