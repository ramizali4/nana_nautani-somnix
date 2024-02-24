using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject shoot_VFX;

    public float shootForce = 10f; // Adjust the force applied to the player
    public GameObject projectile;
    public Transform shotPoint;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public float rotationSpeed = 5f;
    public int ammoCount = 3;
    [Header("Events")]
    public GameEvent onWeaponShot;
    [Header("Audio")]
    public AudioSource shot_SFX;
    public AudioSource reload_SFX;
    public AudioSource empty_SFX;
    private bool hasPlayedRecoilSound = false;

    private void Start()
    {
        onWeaponShot.Raise(this, ammoCount);
    }
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (!GameManager.gameFailed)
        {

            RotateWeapon(mousePosition);
          /*  if (ammoCount > 0)
            {*/
                if (timeBtwShots <= 0)
                {
                    if (!hasPlayedRecoilSound)
                    {
                        reload_SFX.Play();
                        hasPlayedRecoilSound = true;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        shoot_VFX.SetActive(true);
                        Instantiate(projectile, shotPoint.position, transform.rotation);
                        shot_SFX.Play();
                        ammoCount--;

                        onWeaponShot.Raise(this, mousePosition);
                        onWeaponShot.Raise(this, ammoCount);
                        onWeaponShot.Raise(this, "switch");
                        //   playerRB.AddForce(-(mousePosition - playerRB.gameObject.transform.position) * shootForce, ForceMode2D.Impulse);
                        timeBtwShots = startTimeBtwShots;
                        hasPlayedRecoilSound = false;
                    }
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
            else
            {
                // ....
                // empty_SFX.Play();
            }
       // }
    }
    /*  private void Update()
      {
          // Handles the weapon rotation
          Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
          float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
          transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

          if (timeBtwShots <= 0)
          {
              if (Input.GetMouseButton(0))
              {
                 // Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
              //    camAnim.SetTrigger("shake");
                  Instantiate(projectile, shotPoint.position, transform.rotation);
                  timeBtwShots = startTimeBtwShots;
              }
          }
          else
          {
              timeBtwShots -= Time.deltaTime;
          }
      }*/

    void RotateWeapon(Vector3 mousePosition)
    {
        // Get the mouse position in the world coordinates
        mousePosition.z = 0; // Ensure the z-coordinate is 0 for a 2D game

        // Calculate the direction from the weapon to the mouse position
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the weapon smoothly towards the mouse position
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
