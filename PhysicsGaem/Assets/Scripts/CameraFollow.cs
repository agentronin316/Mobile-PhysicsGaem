using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{


    public float xMargin = 1.5f;
    public float yMargin = 1.5f;
    public float xSmooth = 1.5f;
    public float ySmooth = 1.5f;
    private Vector2 maxXAndY;
    private Vector2 minXAndY;
    private bool trackingShot = false;
    public bool shotFired = false;
    public Transform player;
    public Transform cannon;

    
    void Awake()
    {
        
        Bounds backgroundBounds = GameObject.Find("Background").GetComponent<SpriteRenderer>().bounds;

        Vector3 cameraTopLeft = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 cameraBottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f));

        minXAndY.x = backgroundBounds.min.x - cameraTopLeft.x;
        minXAndY.y = backgroundBounds.min.y - cameraBottomRight.y;
        maxXAndY.x = backgroundBounds.max.x - cameraBottomRight.x;
        maxXAndY.y = backgroundBounds.max.y - cameraTopLeft.y;
    }

    
    void LateUpdate()
    {
        if (shotFired)
        {
            player = GameObject.FindWithTag("Player").transform;

            if (player == null)
            {
                Debug.LogError("Player object not found.");
            }
            trackingShot = true;
            shotFired = false;
            StartCoroutine(CheckObjectsHaveStopped());
        }


        if (trackingShot)
        {

            float targetX = transform.position.x;
            
            if (CheckXMargin())
            {
                targetX = Mathf.Lerp(transform.position.x, player.position.x, xSmooth * Time.fixedDeltaTime);
            }
            
            targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
            
            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
        else
        {

            float targetX = transform.position.x;
            
            if (CheckXMargin())
            {
                targetX = Mathf.Lerp(transform.position.x, cannon.position.x, xSmooth * Time.fixedDeltaTime);
            }

            targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);

            transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
        }
    }

    //from http://answers.unity3d.com/questions/209472/detecting-when-all-rigidbodies-have-stopped-moving.html
    //modified by Marshall Mason
    IEnumerator CheckObjectsHaveStopped()
    {
        Rigidbody2D[] GOS = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
        bool allSleeping = false;

        while (!allSleeping)
        {
            allSleeping = true;
            GOS = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
            foreach (Rigidbody2D GO in GOS)
            {
                if (!GO.IsSleeping())
                {
                    allSleeping = false;
                    yield return null;
                    break;
                }
            }

        }

        yield return new WaitForSeconds(1);
        trackingShot = false;
        Destroy(player.gameObject);
    }

    
    /// <summary>
    /// Check if the player has moved near the edge of the camera bounds
    /// </summary>
    /// <returns>if the player has moved near the X edge of the camera</returns>
    bool CheckXMargin()
    {
        if (trackingShot)
        {
            return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
        }
        else
        {
            return Mathf.Abs(transform.position.x - cannon.position.x) > xMargin;
        }
    }
}