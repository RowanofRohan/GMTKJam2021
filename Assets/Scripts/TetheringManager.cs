using UnityEngine;

public class TetheringManager : MonoSingleton<TetheringManager>
{
    [SerializeField] private bool canTetherGround;

    [SerializeField] private float throwForce;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private LineRenderer line;

    private Camera cam;
    private Rigidbody2D tetheredObj;
    private Vector3? currentObjPos;
    private bool isUpdatingLine = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        MouseInteraction();

        //UpdateTetherLine();
    }

    private void MouseInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray, 20, layerMask);
            MouseClick(hit2D);
        }
    }

    private void MouseClick(RaycastHit2D hit2D)
    {
        if (hit2D.collider != null)
        {
            Debug.Log(hit2D.collider.name);
            ClickOnObjects(hit2D);
        }
        else if (tetheredObj)
        {
            ClickOnEmpty();
        }
    }

    private void ClickOnObjects(RaycastHit2D hit2D)
    {

        // Register the first object
        if (tetheredObj == null)
        {
            tetheredObj = hit2D.rigidbody;
        }
        // Tether first object to the second object
        else
        {
            tetheredObj.velocity = Vector2.zero;
            Vector2 dir = hit2D.rigidbody.transform.position - tetheredObj.transform.position;
            tetheredObj.AddForce(dir * throwForce, ForceMode2D.Force);

            currentObjPos = hit2D.rigidbody.transform.position;
            PostLaunch();
        }
    }

    private void ClickOnEmpty()
    {
        if (canTetherGround)
        {
            tetheredObj.velocity = Vector2.zero;
            Vector2 dir = cam.ScreenToWorldPoint(Input.mousePosition) - tetheredObj.transform.position;
            tetheredObj.AddForce(dir * throwForce, ForceMode2D.Force);
        }

        currentObjPos = cam.ScreenToWorldPoint(Input.mousePosition);
        PostLaunch();
    }

    private void PostLaunch()
    {
        isUpdatingLine = true;
        UpdateTetherLine();

        tetheredObj = null;
        //currentObjPos = null;
    }

    private void UpdateTetherLine()
    {
        if (!isUpdatingLine || currentObjPos == null) return;

        Vector3[] linePos = new Vector3[2] { currentObjPos.Value, tetheredObj.transform.position };
        line.SetPositions(linePos);

    }

}
