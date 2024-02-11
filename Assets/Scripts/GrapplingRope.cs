using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [Header("General Refernces")]
    public GrapplingGun grapplingGun;
    public LineRenderer lineRenderer;

    [Header("General Settings")]
    [SerializeField] private int pointNumber = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings")]
    public AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float startWaveSize = 2;
    float waveSize = 0;

    [Header("Rope Progression")]
    public AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    float moveTime = 0;

    [HideInInspector] public bool isGrappling = true;

    bool strightLine = true;

    private void OnEnable()
    {
        moveTime = 0;
        lineRenderer.positionCount = pointNumber;
        waveSize = startWaveSize;
        strightLine = false;

        LinePointsToFirePoint();

        lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
        isGrappling = false;
    }


    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < pointNumber; i++)
        {
            lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    private void Update()
    {
        moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if (!strightLine)
        {
            //check if the line has hit a point where it can attach
            if (lineRenderer.GetPosition(pointNumber - 1).x == grapplingGun.grapplePoint.x)
            {
                //make the line straighten if true
                strightLine = true;
            }
            else
            {
                //make the line wavy if not
                DrawRopeWaves();
            }
        }
        else
        {
            if (!isGrappling)
            {
                grapplingGun.Grapple();
                isGrappling = true;
            }
            if (waveSize > 0)
            {
                //decrease the size of the waves
                waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {
                waveSize = 0;
                
                //if the wave size is 0, it is a straight line so points are removed

                if (lineRenderer.positionCount != 2) 
                { 
                    lineRenderer.positionCount = 2; 
                }

                DrawRopeNoWaves();
            }
        }
    }

    /// <summary>
    /// draws the waves of the line renderer between the fire point and grapple point
    /// </summary>
    void DrawRopeWaves()
    {
        for (int i = 0; i < pointNumber; i++)
        {
            float delta = (float)i / ((float)pointNumber - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized * ropeAnimationCurve.Evaluate(delta) * waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position, grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition, ropeProgressionCurve.Evaluate(moveTime) * ropeProgressionSpeed);

            lineRenderer.SetPosition(i, currentPosition);
        }
    }

    /// <summary>
    /// draws the line with no waves between the fire point and grapple point
    /// </summary>
    void DrawRopeNoWaves()
    {
        lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
        lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
    }
}