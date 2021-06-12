using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed;
    private Vector3 mousePosition;
    private Vector3 direction;
    private float angle;
    private Quaternion rotation;

    private void Update()
    {
        RotateWeapon();
    }

    private void RotateWeapon()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        direction = Vector3.Normalize(mousePosition - transform.position);

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
