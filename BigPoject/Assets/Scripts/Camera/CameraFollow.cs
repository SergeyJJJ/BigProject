using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target = null;
    [Range(1f,40f)] [SerializeField] public float laziness = 10f;
    [SerializeField] public bool lookAtTarget = true;
    [SerializeField] public bool takeOffsetFromInitialPos = true;
    [SerializeField] public Vector3 generalOffset = Vector3.zero;
    private Vector3 whereCameraShouldBe = Vector3.zero;
    private bool warningAlreadyShown = false;

    private void Start()
    {
        if (takeOffsetFromInitialPos && target != null) generalOffset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            whereCameraShouldBe = target.position + generalOffset;
            transform.position = Vector3.Lerp(transform.position, whereCameraShouldBe, 1 / laziness);

            if (lookAtTarget)
            {
                transform.LookAt(target);
            }
        } 
        else
        {
            if (!warningAlreadyShown)
            {
                Debug.Log("Warning: You should specify a target in the simpleCamFollow script.", gameObject);
                warningAlreadyShown = true;
            }
        }
    }
}
