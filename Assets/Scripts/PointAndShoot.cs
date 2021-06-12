using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    public GameObject crosshairs;
    public GameObject player;
    public GameObject bulletStart;
    public string bulletTag;

    public float bulletSpeed = 60.0f;

    private Vector3 target;
    private Camera cam;

    void Start()
    {
        Cursor.visible = false;
        cam = Camera.main;
    }

    void Update()
    {
        target = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        crosshairs.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg + 90f;

        if (Input.GetMouseButtonDown(0))
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            FireBullet(direction, rotationZ);
        }
    }

    void FireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = ObjectPooler.Ins.GetPooledObject(bulletTag);
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.SetActive(true);

        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

}