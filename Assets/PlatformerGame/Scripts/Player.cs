using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Player
{
    public class Player : MonoBehaviour
    {
        public StateIdle StateIdle;
        public StateWalk StateWalk;
        public StateDying StateDying;
        public StateAttack StateAttack;
        public StateJumpRising StateJumpRising;
        public StateJumpFalling StateJumpFalling;
        public StateJumpRisingAttack StateJumpRisingAttack;
        public StateJumpFallingAttack StateJumpFallingAttack;
        public StateJumpDown StateJumpDown;
        public StateSit StateSit;
        public StateSitAttack StateSitAttack;
        public StateSitCrouch StateSitCrouch;
        public StateRollDown StateRollDown;
        public StateDamageTaken StateDamageTaken;

        [HideInInspector]
        public PlayerAnimations Animations;

        public Health Health { get; private set; }
        public Rigidbody2D Rigidbody { get; private set; }
        public SpriteRenderer Renderer { get; private set; }
        public Animator Animator { get; private set; }

        [HideInInspector]
        public float DirectionWeapon = 1f;

        [HideInInspector]
        public bool isDamageable; // have to dispose this bool and fix logic in states..

        public bool InAir;

        //Input
        [HideInInspector]
        public float DirectionX;
        [HideInInspector]
        public float DirectionY;
        [HideInInspector]
        public float HitJump;
        [HideInInspector]
        public float HitAttack;

        private PlayerConfig Config;

        private float HorizontalSpeed;
        private float CrouchSpeed;
        private float PushDownForce;
        private float JumpForce;
        private float RollDownForce;

        private float PrimaryAttackCooldown;
        private float JumpCooldown;
        private float RollDownTime;
        private float JumpDownCooldown;

        private float DamageShockTime;
        private float DeathShockTime;

        [SerializeField]
        private Collider2D StandingCollider;
        [SerializeField]
        private Collider2D SittingCollider;
        // current collider
        private Collider2D Collider;

        // link to a moving platform under player's feet
        private Rigidbody2D Platform = null;

        [HideInInspector]
        public float DeltaY;
        private Vector3 LastPosition;

        private BaseState CurrentState;

        //Weapons
        [SerializeField]
        private Transform StandingFirePoint;
        [SerializeField]
        private Transform SittingFirePoint;
        private Transform FirePoint;

        private Weapons PrimaryWeapon;
        private Weapons SecondaryWeapon;

        private float JumpTimer;
        private float RollDownTimer;
        private float AttackTimer;

        // UI
        [SerializeField]
        private HUD Hud;

        [SerializeField]
        private GameOverScreen GameOver;

        [SerializeField]
        private StartScreen StartScreen;

        private IResourceManager ResourceManager;

        private void Awake()
        {
            ResourceManager = CompositionRoot.GetResourceManager();

            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Renderer = GetComponent<SpriteRenderer>();
            Health = GetComponent<Health>();

            Animations = new PlayerAnimations(Animator);

            Health.HealthChanged += OnHealthChanged;
            Health.Killed += OnKilled;

            GameOver.Hide();
            StartScreen.Show();
            Hud.Show();

            PrimaryWeapon = Weapons.PlayerKnife;
            SecondaryWeapon = Weapons.PlayerAxe;

            StateIdle = new StateIdle(this);
            StateWalk = new StateWalk(this);
            StateDying = new StateDying(this);
            StateAttack = new StateAttack(this);
            StateJumpRising = new StateJumpRising(this);
            StateJumpFalling = new StateJumpFalling(this);
            StateJumpRisingAttack = new StateJumpRisingAttack(this);
            StateJumpFallingAttack = new StateJumpFallingAttack(this);
            StateJumpDown = new StateJumpDown(this);
            StateSit = new StateSit(this);
            StateSitAttack = new StateSitAttack(this);
            StateSitCrouch = new StateSitCrouch(this);
            StateRollDown = new StateRollDown(this);
            StateDamageTaken = new StateDamageTaken(this);

            Config = new PlayerConfig();
        }

        private void OnEnable()
        {
            LoadConfigData();

            Health.RefillHealth();
            int lives = Health.GetHealth;
            Hud.ChangeLivesAmount(lives);

            CurrentState = StateIdle;
            Animations.Idle();
            StandUp();
        }

        private void Update()
        {
            CurrentState.OnUpdate();
        }

        private void FixedUpdate()
        {
            UpdateLastYPosition();
            CurrentState.OnFixedUpdate();

            if (JumpTimer > 0)
            {
                JumpTimer -= Time.fixedDeltaTime;
            }

            if (AttackTimer > 0)
            {
                AttackTimer -= Time.fixedDeltaTime;
            }
        }

        public void SetState(BaseState newState)
        {
            CurrentState = newState;
        }

        public void DirectionCheck()
        {
            // Changes Renderer and weapon's directions
            if (DirectionX > 0)
            {
                Renderer.flipX = false;
                DirectionWeapon = 1f;
            }
            if (DirectionX < 0)
            {
                Renderer.flipX = true;
                DirectionWeapon = -1f;
            }
        }

        public void UpdateLastYPosition()
        {
            DeltaY = transform.position.y - LastPosition.y;
            LastPosition = transform.position;
        }

        // for Double Jump??
        public void UpdateInAir(bool state)
        {
            InAir = state;
        }

        public void Walk(bool platform = false)
        {
            if (platform)
            {
                Rigidbody.velocity = new Vector2(DirectionX * Time.fixedDeltaTime * HorizontalSpeed, 0f) + Platform.velocity;
            }
            else
            {
                Rigidbody.velocity = new Vector2(DirectionX * Time.fixedDeltaTime * HorizontalSpeed, Rigidbody.velocity.y);
            }
        }

        public void Crouch(bool platform = false)
        {
            if (platform)
            {
                Rigidbody.velocity = new Vector2(DirectionX * Time.fixedDeltaTime * CrouchSpeed, 0f) + Platform.velocity;
            }
            else
            {
                Rigidbody.velocity = new Vector2(DirectionX * Time.fixedDeltaTime * CrouchSpeed, Rigidbody.velocity.y);
            }
        }

        // For Idle and Sit States while riding a platform
        public void StickToPlatform()
        {
            Rigidbody.velocity = Platform.velocity;
        }

        // For Idle states on steep slopes, to prevent slip
        public void ResetVelocity()
        {
            Rigidbody.velocity = Vector2.zero; // slips anyway, but quite slowly
        }

        // vertical movement for correcting height in JumpRising state
        public void PushDown()
        {
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, Rigidbody.velocity.y + PushDownForce * Time.fixedDeltaTime * (-1));
        }

        public void Jump()
        {
            if (JumpTimer <= 0)
            {
                Rigidbody.AddForce(new Vector2(0f, JumpForce));
                JumpTimer = JumpCooldown;
            }
        }

        public void DamagePushBack()
        {
            // direction from Health ))
            float direction = Health.DamageDirection;
            DirectionX = direction;
            DirectionCheck();
            Rigidbody.AddForce(new Vector2(HorizontalSpeed / 2.3f * direction, JumpForce / 1.75f));
        }

        public void RollDown()
        {
            Rigidbody.AddForce(new Vector2(DirectionWeapon * RollDownForce, 0f));
        }

        public void StandUp()
        {
            Collider = StandingCollider;
            StandingCollider.enabled = true;
            SittingCollider.enabled = false;

            FirePoint = StandingFirePoint;
        }

        public void SitDown()
        {
            Collider = SittingCollider;
            StandingCollider.enabled = false;
            SittingCollider.enabled = true;

            FirePoint = SittingFirePoint;
        }

        public void AttackCheck()
        {
            //Both types of attack with single cooldown
            if (AttackTimer <= 0)
            {
                AttackTimer = PrimaryAttackCooldown;
                // Up + X - secondary
                if (DirectionY == 1)
                {
                    ShootWeapon(SecondaryWeapon);
                }
                else if (DirectionY <= 0) // attacks from idle and sitting
                {
                    ShootWeapon(PrimaryWeapon);
                }
            }
        }

        public bool Ceiled(LayerMask mask)
        {
            var origin = new Vector2(Collider.bounds.center.x, Collider.bounds.center.y + Collider.bounds.extents.y);
            var boxSize = new Vector2(Collider.bounds.size.x, 0.1f);
            float distance = 0.9f; // Magic number, height of standing collider(1.8f) - height of sitting(0.9f);

            RaycastHit2D CeilHit = Physics2D.BoxCast(origin, boxSize, 0f, Vector2.up, distance, mask);

            return CeilHit.collider != null;
        }

        public bool Grounded(LayerMask mask)
        {
            var origin = new Vector2(Collider.bounds.center.x, Collider.bounds.center.y - Collider.bounds.extents.y);
            var boxSize = new Vector2(Collider.bounds.size.x, 0.1f);

            float distance = 0.1f; // Magic number, empirical

            RaycastHit2D GroundHit = Physics2D.BoxCast(origin, boxSize, 0f, Vector2.down, distance, mask);

            // check moving platform
            if (GroundHit.collider != null && GroundHit.collider.gameObject.layer == (int)Layers.PlatformOneWay)
            {
                Platform = GroundHit.collider.gameObject.GetComponent<Rigidbody2D>();
            }

            return GroundHit.collider != null;
        }

        public void ResetDamageCooldown()
        {
            Health.ResetDamageCooldown();
        }

        public void EnableGameOver()
        {
            GameOver.Show();
        }

        public void JumpDown(bool state)
        {
            if (state)
            {
                this.gameObject.layer = (int)Layers.JumpDown;
            }
            if (!state)
            {
                this.gameObject.layer = (int)Layers.FeetCollider;
            }
        }

        public void UpdateStateName(string name)
        {
            Hud.ChangeStateName(name);
        }

        private void OnHealthChanged()
        {
            int lives = Health.GetHealth;
            Hud.ChangeLivesAmount(lives);

            Animations.DamageTaken();
            StateDamageTaken.Activate(DamageShockTime);
            SetState(StateDamageTaken);
        }

        private void OnKilled()
        {
            Hud.ChangeLivesAmount(0);

            Animations.Dying();
            StateDying.Activate(DeathShockTime);
            SetState(StateDying);
        }

        public float GetRollDownTime()
        {
            return RollDownTime;
        }

        public float GetJumpDownCooldown()
        {
            return JumpDownCooldown;
        }

        public void GetInput()
        {
            DirectionX = Input.GetAxisRaw("Horizontal");
            DirectionY = Input.GetAxisRaw("Vertical");
            HitJump = Input.GetAxisRaw("Jump");
            HitAttack = Input.GetAxisRaw("Fire1");
        }

        private void ShootWeapon(Weapons weapon)
        {
            float offset = 0.5f; // weapon starts it's movement not from the center of player's character
            Vector2 firePosition;

            firePosition = FirePoint.position;
            firePosition.x += offset * DirectionWeapon;

            var instance = ResourceManager.GetFromPool(weapon);
            instance.transform.position = firePosition;

            var weaponVelocity = instance.GetComponent<DamageDealer>().Velocity;
            weaponVelocity.x *= DirectionWeapon;
            instance.GetComponent<Rigidbody2D>().velocity = weaponVelocity;

            bool isAnimated = false;

            if (instance.GetComponent<Animator>() != null)
            {
                isAnimated = true;
            }

            if (DirectionWeapon > 0)
            {
                instance.GetComponent<SpriteRenderer>().flipX = false;
                if (isAnimated)
                {
                    instance.GetComponent<Animator>().SetBool("NegativeDirection", false);
                }
                
            }
            if (DirectionWeapon < 0)
            {
                instance.GetComponent<SpriteRenderer>().flipX = true;
                if (isAnimated)
                {
                    instance.GetComponent<Animator>().SetBool("NegativeDirection", true);
                }
            }
        }

        private void LoadConfigData()
        {
            HorizontalSpeed = Config.HorizontalSpeed;
            CrouchSpeed = Config.CrouchSpeed;
            PushDownForce = Config.PushDownForce;
            JumpForce = Config.JumpForce;
            RollDownForce = Config.RollDownForce;

            PrimaryAttackCooldown = Config.PrimaryAttackCooldown;
            JumpCooldown = Config.JumpCooldown;
            DamageShockTime = Config.DamageShockTime;
            DeathShockTime = Config.DeathShockTime;

            RollDownTime = Config.RollDownTime;
            JumpDownCooldown = Config.JumpDownCooldown;
        }
    }
}