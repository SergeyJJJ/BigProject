using UnityEngine;

public class MovingPlatformInteraction : MonoBehaviour
{
    [SerializeField] CameraBehaviour _cameraBehaviour = null;
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

                // Disable camera damping.
                if (_cameraBehaviour != null)
                {
                    _cameraBehaviour.DiasableDamping();
                }
            }
        }
        // If player isn`t already staying on something.
        else
        {
            // Make player is now not as a child of the platform.
            MakeNotAChild();

            // Enable camera damping.
            if (_cameraBehaviour != null)
            {
                _cameraBehaviour.RestoreDamping();
            }
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
