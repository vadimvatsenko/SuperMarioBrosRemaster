using UnityEngine;

public class MarioFireBall : MonoBehaviour
{
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject fireBallPrefab;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Instantiate(fireBallPrefab, firePoint.position, firePoint.rotation);
    }
}
