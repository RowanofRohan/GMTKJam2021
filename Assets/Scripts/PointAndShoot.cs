using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAndShoot : MonoBehaviour
{
    [SerializeField] private GameObject crosshairs;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletStart;
    [SerializeField] private string bulletTag;

    [SerializeField] private float bulletSpeed = 60.0f;
    [SerializeField] private float slowMoDuration;
    [SerializeField] private float slowMoRate;

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
        MouseInteractions(difference, rotationZ);
    }

    private void MouseInteractions(Vector3 difference, float rotationZ)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (TetheringManager.Ins.HasSelection)
            {
                Time.timeScale = slowMoRate;
                StartCoroutine(ResetSlowMo());
            }
        }

        else if (Input.GetMouseButtonUp(0))
        {
            Time.timeScale = 1;

            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            FireBullet(direction, rotationZ);
        }
    }

    private IEnumerator ResetSlowMo()
    {
        yield return new WaitForSeconds(slowMoDuration);

        Time.timeScale = 1;
    }

    void FireBullet(Vector2 direction, float rotationZ)
    {
        GameObject b = ObjectPooler.Ins.GetPooledObject(bulletTag);
        b.transform.position = bulletStart.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.SetActive(true);

        AudioManager.PlayMusic("Shoot");
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

}