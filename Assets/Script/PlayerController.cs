using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseStat
{   
    public AudioClip pickupEffect;
    AudioSource sound;
    public shoot gun;
    public int ammoLeft = 250;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    CharacterController controller;

    Vector3 velocity;
    bool isGrounded;
    bool isDead;
    public bool canMove = true;

    private void Start()
    {   
        sound = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        gun = GetComponentInChildren<shoot>();

        if (GameManager.Instance.playerInstance == null)
        {
            // set self as main player
            GameManager.Instance.playerInstance = gameObject;
            GameManager.Instance.SetHealthBar(currentHealth, maxHealth);
            DontDestroyOnLoad(GameManager.Instance.playerInstance);
        }
    }

    void Update()
    {
        if(!canMove) return;

        GroundCheck();

        if (!isDead)
        {
            Move();
            Jump();
        }
        else
        {
            // Debug.Log("You're Dead");
        }

        if (Input.GetKeyDown(KeyCode.R) && ammoLeft > 0 && gun.bullet < gun.gunBulletMax && !isDead)
        {
            // reload
            int reloadAmount = Mathf.Min(ammoLeft, gun.gunBulletMax - gun.bullet);
            ammoLeft -= reloadAmount;
            gun.Reload(reloadAmount);
        }

        Fall();

        GameManager.Instance.AmmoUi.updateAmmo(gun.bullet, ammoLeft);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if pickableobject..
        PickableObject pickable = other.gameObject.GetComponent<PickableObject>();
        if (pickable != null)
        {
            Debug.Log("Pickup");
            sound.PlayOneShot(pickupEffect);
            pickable.Picked(this);
        }

        if (other.gameObject.CompareTag("EnemyHitbox"))
        {
            Debug.Log("Hitted");
            Hitbox hitbox = other.gameObject.GetComponent<Hitbox>();
            if (hitbox)
                TakeDamage(hitbox.damage);
        }

        var preventFall = other.GetComponent<PreventPlayerFall>();
        if (preventFall != null) {
            preventFall.movePlayer(transform);
        }
    }

    public void TakeDamage(int damage)
    {
        // some animation ?
        // red screen flash...
        if (!isDead)
        {
            ChangeHealth(-damage);
            print("player health: " + currentHealth);
            GameManager.Instance.flashRed.StartFlash();
        }

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        isDead = true;
        gun.canShoot = false;
        gun.gameObject.SetActive(false);


        GameManager.Instance.GameEnd();


        // play animation
        // Destroy(gameObject);
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    void Move()
    {

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        // Invoke("walk",walkCount);
        // move on ground
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void Jump()
    {
        // jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // v = root h * -2 *g
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Fall()
    {
        // falling
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void AddAmmo(int ammount)
    {
        ammoLeft += ammount;
    }
}
