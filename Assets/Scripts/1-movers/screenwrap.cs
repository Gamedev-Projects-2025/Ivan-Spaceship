using UnityEngine;

public class screenwrap : MonoBehaviour
{
    private Camera mainCamera;
    private float screenWidth;
    private float screenHeight;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        mainCamera = Camera.main;

        //calculating the dimensions of the screen
        screenHeight = 2f * mainCamera.orthographicSize;
        screenWidth = screenHeight * mainCamera.aspect;

        //Calculating the dimensions of the player object
        objectWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        objectHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    void Update()
    {
        //getting the player's position
        Vector3 newPosition = transform.position;

        //checking if he falls within the width of the camera. If not, warp him to other side.
        if (transform.position.x > mainCamera.transform.position.x + screenWidth / 2 + objectWidth)
        {
            newPosition.x = mainCamera.transform.position.x - screenWidth / 2 - objectWidth;
        }
        else if (transform.position.x < mainCamera.transform.position.x - screenWidth / 2 - objectWidth)
        {
            newPosition.x = mainCamera.transform.position.x + screenWidth / 2 + objectWidth;
        }

        //clamping the player's position within the height of the screen
        newPosition.y = Mathf.Clamp(transform.position.y, mainCamera.transform.position.y - screenHeight / 2, mainCamera.transform.position.y + screenHeight / 2);

        //applyng transform
        transform.position = newPosition;
    }
}
