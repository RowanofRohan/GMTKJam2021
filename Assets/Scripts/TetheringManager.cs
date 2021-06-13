using System;
using System.Collections;
using UnityEngine;

public class TetheringManager : MonoSingleton<TetheringManager>
{
    [Header("Choices")]
    [SerializeField] private bool canTetherGround;

    [Header("Parameters")]
    [SerializeField] private float throwForce;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float slowMoDuration;
    [SerializeField] private float slowMoDelay;
    [SerializeField] private float slowMoRate;

    [Header("References")]
    [SerializeField] private LineRenderer line;
    [SerializeField] private ParticleSystem selectionFX;
    [SerializeField] private Rigidbody2D tetheredObj;
    [SerializeField] private GameObject focusPanel;

    private Camera cam;
    private Vector3? currentObjPos;
    private bool isUpdatingLine = false;

    public bool HasSelection { get => tetheredObj && !isUpdatingLine; }

    private void Awake()
    {
        cam = Camera.main;
    }

    private void OnEnable()
    {
        CollisionController.OnCollision += ResetLine;
        CollisionSnapper.OnCollision += ResetLine;
        Bullet.OnHitObject += OnHitObject;
        Bullet.OnHitWall += OnHitWall;
    }

    private void OnDisable()
    {
        CollisionController.OnCollision -= ResetLine;
        CollisionSnapper.OnCollision -= ResetLine;
        Bullet.OnHitObject -= OnHitObject;
        Bullet.OnHitWall -= OnHitWall;
    }

    private void Update()
    {
        MouseInteraction();

        UpdateTetherLine();
    }

    private void MouseInteraction()
    {
        if (Input.GetMouseButtonUp(1))
        {
            ResetLine();
        }
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
        if (HasSelection)
        {
            tetheredObj.velocity = Vector2.zero;
            currentObjPos = pos;

            StartTetherLine();

            Time.timeScale = slowMoRate;
            StartCoroutine(InitTethering(slowMoDuration, pos));
        }
        else
        {
            SelectEmpty();
        }
    }

    private IEnumerator InitTethering(float duration, Vector3 pos)
    {
        yield return new WaitForSeconds(duration);

        Time.timeScale = 1;
        focusPanel.SetActive(false);

        yield return new WaitForSeconds(slowMoDelay);

        if (tetheredObj != null)
        {
            Vector2 dir = pos - tetheredObj.transform.position;
            tetheredObj.AddForce(dir * throwForce, ForceMode2D.Force);
        }
        else
        {
            ResetLine();
        }
    }

    private void SelectObjects(Rigidbody2D hitRb)
    {
        // Register the first object
        if (!HasSelection)
        {
            ResetLine();
            tetheredObj = hitRb;
            selectionFX.transform.position = tetheredObj.position;
            selectionFX.Play();
            focusPanel.SetActive(true);
        }
        // Tether first object to the second object
        else
        {
            if (hitRb == tetheredObj)
            {
                ResetLine();
                return;
            }

            tetheredObj.velocity = Vector2.zero;
            currentObjPos = hitRb.transform.position;

            StartTetherLine();

            Time.timeScale = slowMoRate;
            StartCoroutine(InitTethering(slowMoDuration, hitRb));
        }
    }

    private IEnumerator InitTethering(float duration, Rigidbody2D hitRb)
    {
        yield return new WaitForSeconds(duration);

        Time.timeScale = 1;
        focusPanel.SetActive(false);

        yield return new WaitForSeconds(slowMoDelay);

        Vector2 dir = hitRb.transform.position - tetheredObj.transform.position;
        tetheredObj.AddForce(dir * throwForce, ForceMode2D.Force);
        hitRb.AddForce(-dir * throwForce, ForceMode2D.Force);
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
    }

    #region TetherLine

    private void StartTetherLine()
    {
        isUpdatingLine = true;
        line.gameObject.SetActive(true);
        UpdateTetherLine();
    }

    private void UpdateTetherLine()
    {
        if (!isUpdatingLine || currentObjPos == null) return;

        Vector3[] linePos = new Vector3[2] { currentObjPos.Value, tetheredObj.transform.position };
        line.SetPositions(linePos);

    }

    private void ResetLine()
    {
        selectionFX.Stop();
        focusPanel.SetActive(false);

        isUpdatingLine = false;
        line.gameObject.SetActive(false);
        tetheredObj = null;
        currentObjPos = null;
    }

    #endregion

}
