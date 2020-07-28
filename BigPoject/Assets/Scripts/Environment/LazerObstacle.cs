using UnityEngine;

namespace Environment
{
    public class LazerObstacle : MonoBehaviour
    {
        [SerializeField] private GameObject _firePoint = null;                           // Point from which lazer is shooting.
        [SerializeField] private GameObject _lazerStart = null;                          // Start lazer gameObject that contains start lazer sprite.
        [SerializeField] private GameObject _lazerMiddle = null;                         // Middle lazer gameObject that contains middle lazer sprit.
        [SerializeField] private GameObject _lazerEnd = null;                            // End lazer gameObject that contains end lazer sprite.
        [SerializeField] private float _maxLazerLength = 1f;                             // Length of the lazer.
        [SerializeField] private LayerMask _damageableByLazer = Physics2D.AllLayers;     // Determine what can be damaged by lazer.
        
        private float _currentLazerLength = 1f;                                          // Current length of the lazer.
        private float _startSpriteWidth = 0f;                                            // Start lazer sprite length.
        private SpriteRenderer _startSpriteRenderer = null;                              // SpriteRenderer component of the start lazer sparite.

        /*
           This is the minimum distance that must be
           between character and another object for that
           the lazer doesn`t go inside itself. 
           When that distances is already reached we
           turn off particular lazer object.
        */
        private float _middleMinDistance = 0.85f;                                        // When that distance is reached we turn off middle lazer.
        private float _startMinDistance = 0.55f;                                         // When that distance is reached we turn off start lazer.
        

        private void Start()
        {
            _startSpriteRenderer = _lazerStart.gameObject.GetComponent<SpriteRenderer>();
            _currentLazerLength = _maxLazerLength;
        }


        private void Update()
        {
            RaycastHit2D ray = ThrowRaycast();
            CalculateLazerLength(ray);

            if (IsFarEnoughToObject(_startMinDistance))
            {
                InitializeStartLazerPart();
                ActivateLazerPart(_lazerStart);
            }
            else
            {
                DeactivateLazerPart(_lazerStart);
            }

            if (IsFarEnoughToObject(_middleMinDistance))
            {
                InitializeMiddlePart();
                ActivateLazerPart(_lazerMiddle);
            }
            else
            {
                DeactivateLazerPart(_lazerMiddle);
            }

            if (IsRayCollideSomething(ray))
            {
                InitializeEndPart();
                ActivateLazerPart(_lazerEnd);

            }
            else
            {
                DeactivateLazerPart(_lazerEnd);
            }
        }


        private void InitializeStartLazerPart()
        {
            _startSpriteWidth = _startSpriteRenderer.bounds.size.x;
            _lazerStart.transform.localPosition = Vector2.zero;
        }


        private void InitializeMiddlePart()
        {
            Vector3 midleLocalScale = _lazerMiddle.transform.localScale;
            _lazerMiddle.transform.localScale = new Vector3((_currentLazerLength - _startSpriteWidth),
                midleLocalScale.y,
                midleLocalScale.z);
            _lazerMiddle.transform.localPosition = new Vector2((_currentLazerLength/2), 0f);
        }


        private void InitializeEndPart()
        {
            float offset = 0.5f;           // This offset is needed for the end sprite is placed in properly position.
            _lazerEnd.transform.localPosition = new Vector2(_currentLazerLength - offset, 0f);
        }


        private void ActivateLazerPart(GameObject lazerPart)
        {
            lazerPart.SetActive(true);   
        }


        private void DeactivateLazerPart(GameObject lazerPart)
        {
            lazerPart.SetActive(false);  
        }


        private RaycastHit2D ThrowRaycast()
        {
            return Physics2D.Raycast(_firePoint.transform.position, transform.right, _maxLazerLength, _damageableByLazer);
        }


        private void CalculateLazerLength(RaycastHit2D ray)
        {
            if (IsRayCollideSomething(ray))
            {
                _currentLazerLength = Vector2.Distance(ray.point, _firePoint.transform.position);
            }
            else
            {
                _currentLazerLength = _maxLazerLength;
            }
        }
        

        private bool IsRayCollideSomething(RaycastHit2D ray)
        {
            return ray.collider != null;
        }


        private bool IsFarEnoughToObject(float distance)
        {
            return _currentLazerLength >= distance;
        }
    }
}
