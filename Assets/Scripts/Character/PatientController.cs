using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace TarodevController
{
    /// <summary>
    /// Hey!
    /// Tarodev here. I built this controller as there was a severe lack of quality & free 2D controllers out there.
    /// I have a premium version on Patreon, which has every feature you'd expect from a polished controller. Link: https://www.patreon.com/tarodev
    /// You can play and compete for best times here: https://tarodev.itch.io/extended-ultimate-2d-controller
    /// If you hve any questions or would like to brag about your score, come to discord: https://discord.gg/tarodev
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PatientController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private ScriptableStats _stats;
        private Rigidbody2D _rb;
        private CapsuleCollider2D _col;
        private FrameInput _frameInput;
        private Vector2 _frameVelocity;
        private Vector2 _savedframeVelocity;
        private bool _cachedQueryStartInColliders;
        private RigidbodyConstraints2D constraints;
        private bool goingRight = true;
        [SerializeField] SpriteRenderer img;
        [SerializeField] public PatientSO patientData;
        #region Interface

        public Vector2 FrameInput => _frameInput.Move;
        public event Action<bool, float> GroundedChanged;
        public event Action Jumped;

        #endregion

        private float _time;

        private PatientSlot target;
        private bool reachedTarget = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _col = GetComponent<CapsuleCollider2D>();

            _cachedQueryStartInColliders = Physics2D.queriesStartInColliders;
        }

        private void OnEnable()
        {
            GameEventManager.PauseGame += OnGamePaused;
            GameEventManager.ResumeGame += OnGameResumed;
        }

        private void OnDisable()
        {
            GameEventManager.PauseGame -= OnGamePaused;
            GameEventManager.ResumeGame -= OnGameResumed;
        }

        private void Update()
        {
            _time += Time.deltaTime;
            GatherInput();
        }

        private void OnGamePaused()
        {
            Invoke("PausePlayer", 0.0f);
        }
        private void OnGameResumed()
        {
            Invoke("ResumePlayer", 0.2f);
        }

        private void PausePlayer()
        {
            constraints = _rb.constraints;
            _savedframeVelocity = _rb.velocity;
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        private void ResumePlayer()
        {
            _rb.constraints = constraints;
            _rb.velocity = _savedframeVelocity;
        }

        //public void SetTarget(PatientSlot _target)
        //{
        //    if (reachedTarget)
        //    {
        //        target.Left(patientData);
        //    }
        //
        //    reachedTarget = false;
        //    target = _target;
        //}

        public void SetPatientData(PatientSO _patientData)
        {
            patientData = new PatientSO();
            patientData.pathogen = _patientData.pathogen;
            patientData.symptoms = _patientData.symptoms;
            patientData.blood = _patientData.blood;
            patientData.isMale = _patientData.isMale;

            if (_patientData.stage > 0)
            {
                patientData.stage = _patientData.stage;
            }
            else
            {
                patientData.stage = Random.Range(0.0f, 1.0f);
            }

            if (_patientData.weight > 0)
            {
                patientData.weight = _patientData.weight;
            }
            else
            {
                patientData.weight = Random.Range(80, 120);
            }

            if (_patientData.age > 0)
            {
                patientData.age = _patientData.age;
            }
            else
            {
                if (patientData.isOld)
                {
                    patientData.age = Random.Range(60, 75);
                }
                else
                {
                    patientData.age = Random.Range(22, 32);
                }
            }

            if (!string.IsNullOrEmpty(_patientData.profession))
            {
                patientData.profession = _patientData.profession;
            }
            else
            {
                patientData.profession = Constants.Patients.Professions[Random.Range(0, Constants.Patients.Professions.Count - 1)];
            }

            if (!string.IsNullOrEmpty(_patientData.patientName))
            {
                patientData.patientName = _patientData.patientName;
            }
            else
            {
                if (patientData.isMale)
                {
                    patientData.patientName = Constants.Patients.maleNames[Random.Range(0, Constants.Patients.maleNames.Count - 1)];
                }
                else
                {
                    patientData.patientName = Constants.Patients.femaleNames[Random.Range(0, Constants.Patients.femaleNames.Count - 1)];
                }
            }

            PatientImages pi = PatientManager.Instance.GetPatientImages(patientData.isMale, patientData.isOld);
            patientData.skin = pi.skin;
            patientData.torso = pi.torso;
            patientData.visible = pi.visible;
            patientData.blury = pi.blury;
            patientData.infected = pi.infected;
            patientData.side = pi.side;
            img.sprite = patientData.side;
            patientData.controller = this;
        }

        public void SetRandomPatientData()
        {
            patientData = ScriptableObject.CreateInstance<PatientSO>();
            patientData.isMale = Random.Range(0, 2) < 1;
            patientData.isOld = Random.Range(0, 2) < 1;
            if (patientData.isMale)
            {
                patientData.patientName = Constants.Patients.maleNames[Random.Range(0, Constants.Patients.maleNames.Count - 1)];
            }
            else
            {
                patientData.patientName = Constants.Patients.femaleNames[Random.Range(0, Constants.Patients.femaleNames.Count - 1)];
            }
            if (patientData.isOld)
            {
                patientData.age = Random.Range(60, 75);
            }
            else
            {
                patientData.age = Random.Range(22, 32);
            }
            PatientImages pi = PatientManager.Instance.GetPatientImages(patientData.isMale, patientData.isOld);
            patientData.skin = pi.skin;
            patientData.torso = pi.torso;
            patientData.visible = pi.visible;
            patientData.blury = pi.blury;
            patientData.infected = pi.infected;
            patientData.side = pi.side;
            img.sprite = patientData.side;
            patientData.pathogen = (Pathogen)Random.Range(0, System.Enum.GetValues(typeof(Pathogen)).Length);
            patientData.weight = Random.Range(80, 120);
            patientData.profession = Constants.Patients.Professions[Random.Range(0, Constants.Patients.Professions.Count - 1)];
            patientData.symptoms = PatientManager.Instance.GetSymptoms(patientData.pathogen);
            patientData.blood = (Blood)Random.Range(0, System.Enum.GetValues(typeof(Blood)).Length);
            patientData.stage = Random.Range(0.0f, 1.0f);
            patientData.controller = this;
        }

        public bool TrytogoTo(PatientSlot newTarget)
        {
            if (newTarget != null)
            {
                if (target != null)
                {
                    target.taken = false;
                    target.patient = null;
                    target.Left(patientData);
                }

                target = newTarget;
                reachedTarget = false;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGoToWaitingQueue()
        {
            if (!reachedTarget)
            {
                return false;
            }
            return TrytogoTo(QueueSystem.Instance.GetEmptyWaitingRoomSlot(this));
        }

        public bool TryToGoExam1()
        {
            if (!reachedTarget)
            {
                return false;
            }
            return TrytogoTo(QueueSystem.Instance.GetEmptyExam1Slot(this));
        }

        public bool TryToGoExam2()
        {
            if (!reachedTarget)
            {
                return false;
            }

            return TrytogoTo(QueueSystem.Instance.GetEmptyExam2Slot(this));
        }

        public bool TryToGoExam3()
        {
            if (!reachedTarget)
            {
                return false;
            }

            return TrytogoTo(QueueSystem.Instance.GetEmptyExam3Slot(this));
        }

        public bool TryToGoExam4()
        {
            if (!reachedTarget)
            {
                return false;
            }

            return TrytogoTo(QueueSystem.Instance.GetEmptyExam4Slot(this));
        }

        public bool TryToGoIsolation()
        {
            if (!reachedTarget)
            {
                return false;
            }

            return TrytogoTo(QueueSystem.Instance.GetEmptyIsolationSlot(this));
        }

        public bool TryToGoExit()
        {
            if (!reachedTarget)
            {
                return false;
            }

            return TrytogoTo(QueueSystem.Instance.GetEmptyExitSlot(this));
        }

        private void GatherInput()
        {
            _frameInput = new FrameInput
            {
                //JumpDown = Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.C),
                //JumpHeld = Input.GetButton("Jump") || Input.GetKey(KeyCode.C),
            };

            if (target != null)
            {
                if (target.transform.position.x > transform.position.x + 0.2f)
                {
                    _frameInput.Move = Vector2.right;
                    if (!goingRight)
                    {
                        img.transform.localScale = img.transform.localScale.y * Vector2.up + img.transform.localScale.x * -Vector2.right;
                        goingRight = true;
                    }
                }
                else if (target.transform.position.x < transform.position.x - 0.2f)
                {
                    _frameInput.Move = -Vector2.right;
                    if (goingRight)
                    {
                        goingRight = false;
                        img.transform.localScale = img.transform.localScale.y * Vector2.up + img.transform.localScale.x * -Vector2.right;
                    }
                }
                else if (!reachedTarget)
                {
                    reachedTarget = true;
                    target.Reached(patientData);
                }
            }
            

            if (_stats.SnapInput)
            {
                _frameInput.Move.x = Mathf.Abs(_frameInput.Move.x) < _stats.HorizontalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.x);
                _frameInput.Move.y = Mathf.Abs(_frameInput.Move.y) < _stats.VerticalDeadZoneThreshold ? 0 : Mathf.Sign(_frameInput.Move.y);
            }

            //if (_frameInput.JumpDown)
            //{
            //    _jumpToConsume = true;
            //    _timeJumpWasPressed = _time;
            //}
        }

        private void FixedUpdate()
        {
            //HandleJump(); // Handle jump must be executed first, otherwise player starts jumping
            CheckCollisions(); // Update collisions and flags
            HandleDirection();
            HandleGravity();
            ApplyMovement();
        }

        #region Collisions
        
        private float _frameLeftGrounded = float.MinValue;
        private bool _grounded;

        private void CheckCollisions()
        {
            Physics2D.queriesStartInColliders = false;

            // Ground and Ceiling
            bool groundHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.down, _stats.GrounderDistance, ~_stats.PlayerLayer);
            bool ceilingHit = Physics2D.CapsuleCast(_col.bounds.center, _col.size, _col.direction, 0, Vector2.up, _stats.GrounderDistance, ~_stats.PlayerLayer);

            // Hit a Ceiling
            if (ceilingHit) _frameVelocity.y = Mathf.Min(0, _frameVelocity.y);

            // Landed on the Ground
            if (!_grounded && groundHit)
            {
                _grounded = true;
                _coyoteUsable = true;
                _bufferedJumpUsable = true;
                _endedJumpEarly = false;
                GroundedChanged?.Invoke(true, Mathf.Abs(_frameVelocity.y));
            }
            // Left the Ground
            else if (_grounded && !groundHit)
            {
                _grounded = false;
                _frameLeftGrounded = _time;
                GroundedChanged?.Invoke(false, 0);
            }

            Physics2D.queriesStartInColliders = _cachedQueryStartInColliders;
        }

        #endregion


        #region Jumping

        private bool _jumpToConsume;
        private bool _bufferedJumpUsable;
        private bool _endedJumpEarly;
        private bool _coyoteUsable;
        private float _timeJumpWasPressed;

        private bool HasBufferedJump => _bufferedJumpUsable && _time < _timeJumpWasPressed + _stats.JumpBuffer;
        private bool CanUseCoyote => _coyoteUsable && !_grounded && _time < _frameLeftGrounded + _stats.CoyoteTime;

        private void HandleJump()
        {
            if (!_endedJumpEarly && !_grounded && !_frameInput.JumpHeld && _rb.velocity.y > 0)
            {
                _endedJumpEarly = true;
            }

            if (!_jumpToConsume || !HasBufferedJump)
            {
                return;
            }
            else
            if (_grounded || CanUseCoyote)
            { 
                ExecuteJump();
            }

            _jumpToConsume = false;
        }

        private void ExecuteJump()
        {
            _endedJumpEarly = false;
            _timeJumpWasPressed = 0;
            _bufferedJumpUsable = false;
            _coyoteUsable = false;
            _frameVelocity.y = _stats.JumpPower;
            Jumped?.Invoke();
        }

        #endregion

        #region Horizontal

        private void HandleDirection()
        {
            if (_frameInput.Move.x == 0)
            {
                var deceleration = _grounded ? _stats.GroundDeceleration : _stats.AirDeceleration;
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, 0, deceleration * Time.fixedDeltaTime);
            }
            else
            {
                _frameVelocity.x = Mathf.MoveTowards(_frameVelocity.x, _frameInput.Move.x * _stats.MaxSpeed, _stats.Acceleration * Time.fixedDeltaTime);
            }
        }

        #endregion

        #region Gravity

        private void HandleGravity()
        {
            if (_grounded && _frameVelocity.y <= 0f)
            {
                _frameVelocity.y = _stats.GroundingForce;
            }
            else
            {
                var inAirGravity = _stats.FallAcceleration;
                if (_endedJumpEarly && _frameVelocity.y > 0) inAirGravity *= _stats.JumpEndEarlyGravityModifier;
                _frameVelocity.y = Mathf.MoveTowards(_frameVelocity.y, -_stats.MaxFallSpeed, inAirGravity * Time.fixedDeltaTime);
            }
        }

        #endregion

        private void ApplyMovement() => _rb.velocity = _frameVelocity;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_stats == null) Debug.LogWarning("Please assign a ScriptableStats asset to the Player Controller's Stats slot", this);
        }
#endif
    }
}