using UnityEngine;

public class lifespan : MonoBehaviour
{
    [SerializeField] private float lifetime = 5f; 
    [SerializeField] private float elapsedTime = 0f; 
    private SpriteRenderer spriteRenderer;
    private MeshRenderer meshRenderer;
    private Material meshMaterial;

    void Start()
    {
        //gathering the rendrers for the object
        spriteRenderer = GetComponent<SpriteRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshMaterial = meshRenderer.material;
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        //couting how much time has passed
        elapsedTime += Time.deltaTime;

        //calculating the alpha based on the time elapsed. The more time passes the more transparent it will be
        float alpha = Mathf.Clamp01(1 - (elapsedTime / lifetime));

        //applying new alpha
        if (spriteRenderer != null)
        {
            Color tmp = spriteRenderer.color;
            tmp.a = alpha;
            spriteRenderer.color = tmp;
        }

        if (meshMaterial != null)
        {
            Color tmpAlpha = meshMaterial.color;
            tmpAlpha.a = alpha;
            meshMaterial.color = tmpAlpha;
        }
    }
}
