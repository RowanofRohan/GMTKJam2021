using System;
using UnityEngine;

public class TetheringManager : MonoSingleton<TetheringManager>
{
    [SerializeField] private bool canTetherGround;

    [SerializeField] private float throwForce;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private LineRenderer line;
    [SerializeField] private ParticleSystem selectionFX;

    private Camera cam;
    [SerializeField] private Rigidbody2D tetheredObj;
    private Vector3? currentObjPos;
    private bool isUpdatingLine = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        CollisionController.OnCollision += ResetLine;
        Bullet.OnHitObject += OnHitObject;
        Bullet.OnHitWall += OnHitWall;
    }

    private void OnDisable()
    {
        CollisionController.OnCollision -= ResetLine;
        Bullet.OnHitObject -= OnHitObject;
        Bullet.OnHitWall -= OnHitWall;
    }

    //private void Start()
    //{
    //    line.positionCount = 2;
    //}

    private void Update()
    {
        UpdateTetherLine();
    }

    private void ResetLine()
    {
        isUpdatingLine = false;
        line.gameObject.SetActive(false);
        tetheredObj = null;
        currentObjPos = null;
    }

    private void OnHitObject(Rigidbody2D rb)
    {
        if (layerMask == (layerMask | (1 << rb.gameObject.layer)))
        {
            SelectObjects(rb);
        }
        else
        {
            SelectEmpty();
        }
    }

    private void OnHitWall(Vector3 pos)
    {
        // Already selected an object to tether
        if (tetheredObj && !isUpdatingLine)
        {
            tetheredObj.velocity = Vector2.zero;
            Vector2 dir = pos - tetheredObj.transform.position;
            tetheredObj.AddForce(dir * throwForce, ForceMode2D.Force);

            currentObjPos = pos;
            PostLaunch();
        }
        else
        {
            SelectEmpty();
        }
    }

    private void SelectObjects(Rigidbody2D hitRb)
    {
        // Register the first object
        if (tetheredObj == null || (tetheredObj != null && isUpdatingLine))
        {
            ResetLine();
            tetheredObj = hitRb;
            selectionFX.transform.position = tetheredObj.position;
            selectionFX.Play();
        }
        // Tether first object to the second object
        else
        {
            tetheredObj.velocity = Vector2.zero;
            Vector2 dir = hitRb.transform.position - tetheredObj.transform.position;
            tetheredObj.AddForce(dir * throwForce, ForceMode2D.Force);
            hitRb.AddForce(-dir * throwForce, ForceMode2D.Force);

            currentObjPos = hitRb.transform.position;
            PostLaunch();
        }
    }

    private void SelectEmpty()
    {
        if (canTetherGround)
        {
            //tetheredObj.velocity = Vector2.zero;
            Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - tetheredObj.transform.position;
            tetheredObj.AddForce(dir * throwForce, ForceMode2D.Force);

            currentObjPos = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        PostLaunch();
    }

    private void PostLaunch()
    {
        isUpdatingLine = true;
        selectionFX.Stop();
        line.gameObject.SetActive(true);
        UpdateTetherLine();

        //tetheredObj = null;
        //currentObjPos = null;
    }

    private void UpdateTetherLine()
    {
        if (!isUpdatingLine || currentObjPos == null) return;

        Vector3[] linePos = new Vector3[2] { currentObjPos.Value, tetheredObj.transform.position };
        line.SetPositions(linePos);

    }

}
