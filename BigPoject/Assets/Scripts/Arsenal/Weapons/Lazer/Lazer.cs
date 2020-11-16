using System.Collections;
using Environment.ThingsDestruction;
using GameBehaviour;
using LivingBeings;
using LivingBeings.Player.CharacterMovement.MovementStateMachine;
using UnityEngine;

namespace Arsenal.Weapons.Lazer
{
    [RequireComponent((typeof(SpriteRenderer)))]
    public class Lazer : Weapon
    {
        [SerializeField] private GameObject _lazerStart = null;                          // Start lazer gameObject that contains start lazer sprite.
        [SerializeField] private GameObject _lazerMiddle = null;                         // Middle lazer gameObject that contains middle lazer sprit.
        [SerializeField] private GameObject _lazerEnd = null;                            // End lazer gameObject that contains end lazer sprite.
        [SerializeField] private float _maxLazerLength = 1f;                             // Length of the lazer.
        [SerializeField] private LayerMask _hittableByLazer = Physics2D.AllLayers;       // Determine what can be hitted by lazer.
        [SerializeField] private float _damageAmount = 0f;                               // How much damage will be applied to the entity with Health component every second.
        [SerializeField] private float _timeBeforeAapplyingDamage = 0f;                  // How much time should pass before next damage applying : Seconds.

        private bool _isButtonPressedNow = false;                                       // Determine is fire button pressed or not.
        
        private enum ButtonPressTimes                                                    // Enumeration that used to indicate how many times fire button was pressed.
        {
            NotOnce,
            Once,
            Twice
        }

        private ButtonPressTimes _buttonPressTimes = ButtonPressTimes.NotOnce;           // Determine how many time fire button was pressed. 
        
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
        
        private float _spendEnergyTimer = 1;                                             // Timer that control how fast lazer energy will be decremented.
        private float _decrementEnergeyTime = 1;                                         // Time after which energy will be decremented.
        private float _damageApllyingTimer = 0;                                          // Timer that control how often damage will be applyed.
        
        private CharacterMovement _characterMovement = null;                             // Characters movement control script.

        #region Properties

        public float CurrentLazerLength => _currentLazerLength;

        public float StartSpriteWidth => _startSpriteWidth;

        #endregion Properties
        
        protected override void CallShotEvent()
        {
            EventSystem.TriggerEvent("OnLazerShot");
        }


        protected override void CallStopShotEvent()
        {
            EventSystem.TriggerEvent("OnStopLazerShot");
        }


        private void CallActivationEvent()
        {
            EventSystem.TriggerEvent("OnLazerActivation");
        }


        private void CallDeactivationEvent()
        {
            EventSystem.TriggerEvent("OnLazerDeactivation");
        }


        private void Awake()
        {
            WeaponSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }


        private void Start()
        {
            _characterMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
            _startSpriteRenderer = _lazerStart.gameObject.GetComponent<SpriteRenderer>();
            WeaponSpriteRenderer.sprite = InGameSprite;
            _currentLazerLength = _maxLazerLength;
            CurrentBulletCount = BulletsAmount;
        }
        
        
         private void FixedUpdate()
         {
             if (IsShotTriggered)
            {
                if (!_isButtonPressedNow)
                {
                    _isButtonPressedNow = true;
                    
                    if (_buttonPressTimes == ButtonPressTimes.NotOnce)
                    {
                        _buttonPressTimes = ButtonPressTimes.Once;
                    }
                    else if (_buttonPressTimes == ButtonPressTimes.Once)
                    {
                        _buttonPressTimes = ButtonPressTimes.Twice;
                    }
                }
            }
            else
            {
                if (_isButtonPressedNow)
                {
                    _isButtonPressedNow = false;
                    
                    if (_buttonPressTimes == ButtonPressTimes.Once)
                    {
                        StopAllProcesses();
                        StartCoroutine(StartLazerShootingRoutine());
                    }
                    else if (_buttonPressTimes == ButtonPressTimes.Twice)
                    {
                        _buttonPressTimes = ButtonPressTimes.NotOnce;
                        
                        StopAllProcesses();
                        StartCoroutine(StopLazerShootingRoutine());
                    }
                }
            }
         }

         
         private IEnumerator StartLazerShootingRoutine()
         {
             yield return StartCoroutine(LazerActivationRoutine());
             
             StartCoroutine(LazerShootingRoutine());
         }
         
         
         private IEnumerator StopLazerShootingRoutine()
         {
             yield return StartCoroutine(LazerDeactivationRoutine());
         }


         private IEnumerator LazerActivationRoutine()
         {
             CallActivationEvent();
             
             yield return new WaitForSeconds(0.6f);
         }


         private IEnumerator LazerDeactivationRoutine()
         {
             yield return new WaitForFixedUpdate();
             
             CallStopShotEvent();
             
             DeactivateLazerPart(_lazerStart);
             DeactivateLazerPart(_lazerMiddle);
             DeactivateLazerPart(_lazerEnd);
             
             CallDeactivationEvent();
         }


         private IEnumerator LazerShootingRoutine()
         {
             while (true)
             {
                 yield return new WaitForFixedUpdate();
                 
                 if (IsEnoughBullets())
                 {
                     SpendEnergy();
                     CallShotEvent();
                     MoveCharacterWhenShooting();
 
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
 
                         _damageApllyingTimer -= Time.deltaTime;
                         
                         if (IsTimeBeforeDamageApplyingPass())
                         {
                             _damageApllyingTimer = _timeBeforeAapplyingDamage;
                             
                             Health health = GetHealthComponent(ray.collider.gameObject);
 
                             if (health != null)
                             {
                                 ApplyDamageTo(health);
                             }
                             else
                             {
                                 ThingsDestruction breakable = GetBreakableComponent(ray.collider.gameObject);

                                 if (breakable != null)
                                 {
                                     ApplyDamageTo(breakable);
                                 }
                             }
                         }
                     }
                     else
                     {
                         DeactivateLazerPart(_lazerEnd);
                     }
                 }
                 else
                 {
                     DeactivateLazerPart(_lazerStart);
                     DeactivateLazerPart(_lazerMiddle);
                     DeactivateLazerPart(_lazerEnd);
                     CallStopShotEvent();
                 }
             }
         }


         private void StopAllProcesses()
         {
             StopAllCoroutines();
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
            return Physics2D.Raycast(FirePoint.transform.position, transform.right, _maxLazerLength, _hittableByLazer);
        }


        private void CalculateLazerLength(RaycastHit2D ray)
        {
            if (IsRayCollideSomething(ray))
            {
                _currentLazerLength = Vector2.Distance(ray.point, FirePoint.transform.position);
            }
            else
            {
                _currentLazerLength = _maxLazerLength;
            }
        }


        private void SpendEnergy()
        {
            _spendEnergyTimer -= Time.deltaTime;
                
            if (_spendEnergyTimer <= 0)
            {
                DecrementBulletsCount();
                _spendEnergyTimer = _decrementEnergeyTime;
                DecrementBulletsCount();
            }
        }


        // Move character to the opposite side from lazer beam direction.
        private void MoveCharacterWhenShooting()
        {
            float movementForce = 19;
            _characterMovement.AddForceInDirection(-transform.right * movementForce);
        }


        private Health GetHealthComponent(GameObject collidedObject)
        {
            Health health = collidedObject.GetComponent<Health>();
            return health;
        }


        private ThingsDestruction GetBreakableComponent(GameObject collidedObject)
        {
            ThingsDestruction breakable = collidedObject.GetComponent<ThingsDestruction>();
            return breakable;
        }


        private void ApplyDamageTo(Health health)
        {
            health.TakeDamage(_damageAmount);
        }


        private void ApplyDamageTo(ThingsDestruction breakable)
        {
            breakable.Break(_damageAmount);
        }


        private bool IsRayCollideSomething(RaycastHit2D ray)
        {
            return ray.collider != null;
        }


        private bool IsFarEnoughToObject(float distance)
        {
            return _currentLazerLength >= distance;
        }


        private bool IsTimeBeforeDamageApplyingPass()
        {
            return _damageApllyingTimer < 0;
        }


        private void OnDisable()
        {
            _buttonPressTimes = ButtonPressTimes.NotOnce;
            
            DeactivateLazerPart(_lazerStart);
            DeactivateLazerPart(_lazerMiddle);
            DeactivateLazerPart(_lazerEnd);
        }
    }
}