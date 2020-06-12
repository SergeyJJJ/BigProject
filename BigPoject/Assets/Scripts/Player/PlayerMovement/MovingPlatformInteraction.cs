using UnityEngine;

public class MovingPlatformInteraction : MonoBehaviour
{
    private SurfaceCheck _groundCheck = null;


    private void Awake()
    {
        InitializeReferencesOnScripts();
    }


    private void InitializeReferencesOnScripts()
    {
        _groundCheck = gameObject.GetComponent<SurfaceCheck>();
    }


    private void Update()
    {
        Collider2D surfaceOnWhichPlayerStanding = _groundCheck.GetSurfaceOnWhichPlayerStanding();

        if (surfaceOnWhichPlayerStanding != null)
        {
            // If player is on the platform.
            if (surfaceOnWhichPlayerStanding.gameObject.CompareTag("MovingPlatform"))
            {
                // Make player as a child of the platform.
                MakeChildOf(surfaceOnWhichPlayerStanding.gameObject.transform);
            }
        }
        // If player isn`t already staying on something.
        else
        {
            // Make player is now not as a child of the platform.
            MakeNotAChild();
        }
    }


    private void MakeChildOf(Transform parent)
    {
        gameObject.transform.SetParent(parent);
    }


    private void MakeNotAChild()
    {
        gameObject.transform.SetParent(null);
    }
}
