using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("Scripts Ref")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera")]
    public Camera mainCamera;

    [Header("Transform Ref")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref")]
    public SpringJoint2D springJoint;
    public Rigidbody2D rigidBody;

    [Header("Rotation")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistance = 20;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    private void Start()
    {
        grappleRope.enabled = false;
        springJoint.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) //sets a grapple point when primary mouse is clicked
        {
            SetGrapplePoint();
        }
        else if (Input.GetKey(KeyCode.Mouse0)) //stops rotation if primary mouse button is held down
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }

            if (launchToPoint && grappleRope.isGrappling) //moves the player
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistance = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistance;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) //releases the grapple when mouse released
        {
            grappleRope.enabled = false;
            springJoint.enabled = false;
            rigidBody.gravityScale = 1;
        }
        else //if no buttons are pressed the gun pivot should rotate to the mouse position
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePos.x + "," + mousePos.y);
            RotateGun(mousePos, true);
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        //find the rotation angle

        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;

        if (rotateOverTime && allowRotationOverTime) //rotates the gun pivot over time
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else //looks at mouse cursor constantly
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 distanceVector = mainCamera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
            {
                if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistance || !hasMaxDistance)
                {
                    grapplePoint = _hit.point;
                    grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                    grappleRope.enabled = true;
                }
            }
        }
    }

    public void Grapple()
    {
        springJoint.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            springJoint.distance = targetDistance;
            springJoint.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                springJoint.autoConfigureDistance = true;
                springJoint.frequency = 0;
            }

            springJoint.connectedAnchor = grapplePoint;
            springJoint.enabled = true;
        }
        else
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    springJoint.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    springJoint.distance = distanceVector.magnitude;
                    springJoint.frequency = launchSpeed;
                    springJoint.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    rigidBody.gravityScale = 0;
                    rigidBody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(firePoint.position, maxDistance);
        }
    }

}