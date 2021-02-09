using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public AudioClip fire;
    public AudioClip reloadBullet;
    AudioSource gunAudio;

    Animator anim;

    public float reloadTime;
    // public float range = 100.0f;
    public ParticleSystem muzzleFlash;
    public GameObject blood;
    public GameObject impactEffect;
    // public float fireRate = 15.0f;
    private float nextTimeToFire = 0;
    public Camera fpsCam;
    // Update is called once per frame

    // gun information
    public float range;
    public float fireRate;
    public int bullet;
    public int gunDamage;
    public int gunBulletMax;

    public bool holdDownFire;
    public bool canShoot = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        bullet = gunBulletMax;
    }
    private void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool fire = holdDownFire ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");

        if (fire && Time.time >= nextTimeToFire && canShoot)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        else if (Time.time < nextTimeToFire)
        {
            // Debug.Log("Cooldown Gun");
        }
        else if (bullet == 0)
        {
            // Debug.Log("Out of Bullet");
        }
    }

    void Shoot()
    {
        gunAudio.PlayOneShot(fire);
        anim.SetTrigger("shoot");
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {

            Debug.Log("hit " + hit.transform.name);

            ZombieHurtBox hurtBox = hit.transform.GetComponent<ZombieHurtBox>();
            if (hurtBox != null)
            {
                hurtBox.TakeDamage(gunDamage);

                GameObject bloodParticle = Instantiate(blood.gameObject, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(bloodParticle, 2f);
            }

            GameObject go = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(go, 2f);
        }

        if (--bullet == 0)
        {
            canShoot = false;
        }

    }

    public void Reload(int amount)
    {
        if (amount > 0)
        {
            gunAudio.PlayOneShot(reloadBullet);
            canShoot = false;
            anim.SetTrigger("reload");
            bullet += amount;
            Invoke("resetCanshoot", reloadTime);
        }
    }

    void resetCanshoot()
    {
        canShoot = true;
    }
}
