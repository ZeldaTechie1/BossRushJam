using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]private Camera _mainCamera;
    [SerializeField]private float _movementSpeed;
    [SerializeField]private float _slideLength;
    [SerializeField]private float _slideSpeed;
    [SerializeField]private float _slideCoolDown;
    [SerializeField]private float _invincibilityLength;
    [SerializeField]private float _throwSpeed = 5f;
    [SerializeField]private GameEvent _playerCanTakeDamageEvent;
    [SerializeField]private List<SpriteRenderer> ItemsHeld;
    [SerializeField]private List<Animator> _playerAnimators;
    [SerializeField]private List<GameObject> _hitboxes;
    [SerializeField]private List<GameObject> _throwableObjects;
    [SerializeField]private Transform _throwSpawnPoint;
    [SerializeField]private float _currentDamage = 35;
    [SerializeField]private float _baseDamage = 35;
    [SerializeField]private float[] _additionalDamage;
    [SerializeField]private float _attackCoolDown;

    [SerializeField] protected GameAudioEventManager _gameAudioEventManager;

    //Effects
    [SerializeField]private ParticleSystem DashLeftParticle;
    [SerializeField]private ParticleSystem DashRightParticle;

    private Rigidbody _rb;
    private Vector2 _movementInput;
    private Vector2 _lookDirection;
    private Vector3 _movementDirection;
    private Vector3 _cardinalMoveDirection;
    private Vector3 currentOrientation;
    private int _lookDirectionIndex;
    private bool _isSliding;
    private bool _isAttacking;
    private bool _canSlide = true;
    private bool _canAttack = true;
    private float _attackTime = 0;
    private Health health;
    private PlayerInput _playerInput;


    private List<Boss> _debugBosses;

    private void Awake()
    {
        _debugBosses = FindObjectsOfType<Boss>().ToList();
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        foreach (AnimationClip clip in _playerAnimators[0].runtimeAnimatorController.animationClips)
        {
            if(clip.name.Contains("Attack"))
            {
                _attackTime = clip.length / 2;
                break;
            }
        }
        health = this.GetComponent<Health>();
        _playerInput = this.GetComponent<PlayerInput>();
    }

    //this gets called by the Player Input Component on this object
    public void OnLocomotion(InputValue value)
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        _movementInput = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        if (_isSliding || _isAttacking)
            return;
        _lookDirection = value.Get<Vector2>();
        if(_playerInput.currentControlScheme == "Mouse and Keyboard")
        {
            Vector3 mousePosition = UnityEngine.Input.mousePosition;
            Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
            _lookDirection = mousePosition - playerPosition;
        }
        if (_lookDirection.magnitude > 0)
        {
            _lookDirectionIndex = (int)HelperFunctions.CardinalizeVector(_lookDirection);
            foreach (Animator _playerAnimator in _playerAnimators)
                _playerAnimator.SetInteger("LookDirection", _lookDirectionIndex);
        }
        
    }

    public void OnSlide()
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        if (!_canSlide || health.IsStunned)
            return;

        //_gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayPlayerDash();
        string sfx_player_dash_path = "event:/SFX/sfx_player_dash";
        FMOD.Studio.EventInstance sfx_player_dash = FMODUnity.RuntimeManager.CreateInstance(sfx_player_dash_path);
        sfx_player_dash.start();

        _playerCanTakeDamageEvent.Invoke(data: false);
        _isSliding = true;
        _canSlide = false;
        DOTween.Sequence()
            .InsertCallback(_invincibilityLength, () => _playerCanTakeDamageEvent.Invoke(data: true))
            .InsertCallback(_slideLength, () => _isSliding = false)//stopping the slide
            .AppendInterval(_slideCoolDown)
            .AppendCallback(() => _canSlide = true);//allows to slide again
        //adds an impulse force either in the current movement direction or if standing still, in the direction they are facing
        _rb.AddForce((_rb.velocity.magnitude > 0? _rb.velocity.normalized : _cardinalMoveDirection.normalized) * _slideSpeed, ForceMode.Impulse);
        _lookDirectionIndex = (int)HelperFunctions.CardinalizeVector(_rb.velocity);
        if(_lookDirectionIndex == 0 || _lookDirectionIndex == 2)
        {
            DashRightParticle.Play();
        }
        else
        {
            DashLeftParticle.Play();
        }
        foreach(Animator _playerAnimator in _playerAnimators)
            _playerAnimator.SetInteger("LookDirection", _lookDirectionIndex);

    }

    public void OnAttack()
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        if (_isAttacking || _isSliding || health.IsStunned || !_canAttack)
            return;
        if(CraftingSystem.Instance.CraftingRecipes[CraftingSystem.Instance.ItemSelected].Throwable && CraftingSystem.Instance.CraftingRecipes[CraftingSystem.Instance.ItemSelected].Crafted)
        {
            GameObject newThrowable = Instantiate(_throwableObjects[CraftingSystem.Instance.ItemSelected - 2], _throwSpawnPoint.position, Quaternion.identity);
            newThrowable.transform.parent = null;
            Rigidbody rb = newThrowable.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.velocity = HelperFunctions.VectorDirections[_lookDirectionIndex] * _throwSpeed;  
            }
            CraftingSystem.Instance.RemoveDurability(1);

            //_gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayProjectileCollision();
            string sfx_projectile_collision_path = "event:/SFX/sfx_projectile_collision";
            FMOD.Studio.EventInstance sfx_projectile_collision = FMODUnity.RuntimeManager.CreateInstance(sfx_projectile_collision_path);
            sfx_projectile_collision.start();
        }
        else
        {
            //_gameAudioEventManager.GetComponent<GameAudioEventManager>().PlayPlayerWeaponSlash();
            string sfx_player_weapon_slash_path = "event:/SFX/sfx_player_weapon_slash";
            FMOD.Studio.EventInstance sfx_player_weapon_slash = FMODUnity.RuntimeManager.CreateInstance(sfx_player_weapon_slash_path);
            sfx_player_weapon_slash.start();

            _isAttacking = true;
            _canAttack = false;
            int hitboxIndex = _lookDirectionIndex;
            _hitboxes[hitboxIndex].SetActive(true);
            DOTween.Sequence()
                .InsertCallback(_attackTime, () =>
                {
                    _isAttacking = false;
                    _hitboxes[hitboxIndex].SetActive(false);
                })
                .InsertCallback(_attackCoolDown, () => _canAttack = true);
        }
    }

    private void FixedUpdate()
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        if (health.IsStunned)
            return;
        currentOrientation = _mainCamera.transform.forward;
        currentOrientation.y = 0;
        currentOrientation.Normalize();
        _movementDirection = (this.transform.forward * _movementInput.y) + (this.transform.right * _movementInput.x);
        if(_movementDirection.magnitude > 1)
        {
            _movementDirection.Normalize();
        }
        this.transform.forward = currentOrientation;
        if(!_isSliding)
        {
            _rb.velocity = _movementDirection * _movementSpeed;
        }
        if(_rb.velocity.magnitude != 0)
        {
            _cardinalMoveDirection = HelperFunctions.VectorDirections[(int)HelperFunctions.CardinalizeVector(_rb.velocity)];
        }
        //animator updates
        foreach(Animator _playerAnimator in _playerAnimators)
        {
            _playerAnimator.SetFloat("Speed", _rb.velocity.magnitude);
            _playerAnimator.SetBool("isSliding", _isSliding);
            _playerAnimator.SetBool("isAttacking", _isAttacking);
        }
        
    }

    public void OnScrollWheel(InputValue value)
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        Vector2 scroll = value.Get<Vector2>();
        if(scroll.y > 0)
        {
            CraftingSystem.Instance.SelectItemUp();
            OnCraftingSelectUp();
        }
        else if(scroll.y < 0)
        {
            CraftingSystem.Instance.SelectItemDown();
            OnCraftingSelectDown();
        }
    }

    public void OnCraftingButton()
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        UpdateItemHeld();
    }

    public void OnCraftingSelectUp()
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        UpdateItemHeld();
    }
    public void OnCraftingSelectDown()
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        UpdateItemHeld();
    }

    public void OnPause()
    {
        _mainCamera.GetComponent<CustomSceneManager>().TogglePause();
    }

    private void UpdateItemHeld()
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        foreach (SpriteRenderer item in ItemsHeld)
        {
            item.enabled = false;
        }
        if (CraftingSystem.Instance.CraftingRecipes[CraftingSystem.Instance.ItemSelected].Crafted && CraftingSystem.Instance.ItemSelected != 0)
        {
            if(!CraftingSystem.Instance.CraftingRecipes[CraftingSystem.Instance.ItemSelected].Throwable)
            {
                ItemsHeld[CraftingSystem.Instance.ItemSelected].enabled = true;
                _currentDamage = _baseDamage + _additionalDamage[CraftingSystem.Instance.ItemSelected];

                _gameAudioEventManager.PlayChangeEquippedItem();
            }
        }
        else
        {
            ItemsHeld[0].enabled = true;
            _currentDamage = _baseDamage;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (_mainCamera.GetComponent<CustomSceneManager>().isPaused)
            return;
        if (other.tag == "Enemy")
        {
            other.GetComponent<Health>().AffectHealth(null, -_currentDamage);
            if(CraftingSystem.Instance.ItemSelected != 0)
            {
                CraftingSystem.Instance.RemoveDurability(1);
            }
            UpdateItemHeld();
        }
    }

    // DEBUG COMMANDS TO SWAP BOSSES
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DebugBoss(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DebugBoss(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            DebugBoss(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            health.IsInvincible = !health.IsInvincible;
        }*/
    }

    private void DebugBoss(int bossIndex)
    {
        foreach (Boss boss in _debugBosses)
        {
            if (boss.BossIndex == bossIndex)
            {
                boss.gameObject.SetActive(true);
            }
            else
            {
                boss.gameObject.SetActive(false);
            }
        }
    }
}
