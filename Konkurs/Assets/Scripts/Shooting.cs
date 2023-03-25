using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public int maxBullets = 10;
    public int currentBullets = 10;
    public float reloadTime = 1f;

    private bool isReloading = false;

    void Update()
    {
        // Check if the left mouse button is pressed and there are bullets available
        if (Input.GetMouseButtonDown(0) && currentBullets > 0 && !isReloading)
        {
            // Instantiate a bullet at the bullet spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Apply a force to the bullet to make it move forward
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bullet.GetComponent<Bullet>().speed, ForceMode.Impulse);

            // Decrease the number of bullets available
            currentBullets--;
        }

        // Check if the player presses the reload button and there are no bullets available
        if (Input.GetKeyDown(KeyCode.R) && currentBullets == 0 && !isReloading)
        {
            // Start the reloading coroutine
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        // Wait for the reload time
        yield return new WaitForSeconds(reloadTime);

        // Reset the number of bullets
        currentBullets = maxBullets;

        Debug.Log("Finished reloading.");
        isReloading = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player collides with an ammo pack
        if (other.CompareTag("AmmoPack"))
        {
            // Increase the number of bullets available
            currentBullets = Mathf.Min(currentBullets + 10, maxBullets);

            // Destroy the ammo pack
            Destroy(other.gameObject);
        }
    }

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    void FixedUpdate()
    {
        // Check if the left mouse button is pressed
        if (Input.GetMouseButtonDown(0))
        {
            // Instantiate a bullet at the bullet spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            // Apply a force to the bullet to make it move forward
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * bullet.GetComponent<Bullet>().speed, ForceMode.Impulse);
        }
    }
}
