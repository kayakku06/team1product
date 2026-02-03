using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public GameObject bulletPrefab;
    public InputActionProperty triggerAction;

    void Update()
    {
        if (triggerAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, muzzle.position, muzzle.rotation);
    }
}

