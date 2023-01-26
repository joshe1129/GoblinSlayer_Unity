using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    #region Variables
    private Vector3 cameraTransformPosition;
    private Vector3 cameraFollowVelocity = Vector3.zero;
    private LayerMask ignoreLayers;
    public static CameraHandler singleton;
    public float lookSpeed = 8f;
    public float followSpeed = 0.1f;
    public float pivotSpeed = 0.03f;

    private float targetPosition;
    public float cameraSphereRadius = 0.2f;
    public float cameraCollisionOffset = 0.2f;
    public float minimunCollisionOffset = 0.2f;

    public float defaultPosition;
    public float lookAngle;
    public float pivotAngle;
    public float minimunPivotAngle = -35;
    public float maximunPivotAngle = 35;
    #endregion

    #region Referencias
    public Transform targetTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;// to rotate the camera around the player
    public Transform myTransform;

    #endregion
    /*
    
    private void Awake()
    {
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        targetTransform = FindObjectOfType<PlayerManager>().transform;
    }
    */
    public void FollowTarget(float delta)
    {
        Vector3 targetPosition = Vector3.SmoothDamp(myTransform.position, targetTransform.position, ref cameraFollowVelocity, delta / followSpeed);//makes more smooth the follow of the camera
        myTransform.position = targetPosition;

        HandleCameraCollision(delta);
    }


    public void HandleCameraRotation(float delta, float mouseXInput, float MouseYInput)
    {
        lookAngle += (mouseXInput * lookSpeed) / delta;
        pivotAngle -= (MouseYInput * pivotSpeed) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minimunPivotAngle, maximunPivotAngle);//for the camera don't pass the angles

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        myTransform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRotation = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRotation;

    }

    private void HandleCameraCollision(float delta)//makes the camera bumps objects and put the camera in the position of the hit
    {
        targetPosition = defaultPosition;
        RaycastHit hit;
        Vector3 direction = cameraTransform.position - cameraPivotTransform.position;
        direction.Normalize();
        if (Physics.SphereCast(cameraPivotTransform.position, cameraSphereRadius, direction, out hit, Mathf.Abs(targetPosition), ignoreLayers))
        {
            float dis = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPosition = -(dis - cameraCollisionOffset);
        }
        if (Mathf.Abs(targetPosition) < minimunCollisionOffset)
        {
            targetPosition = -minimunCollisionOffset;
        }

        cameraTransformPosition.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPosition,delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPosition;
    }

    // Start is called before the first frame update
    void Start()
    {
        singleton = this;
        myTransform = transform;
        defaultPosition = cameraTransform.localPosition.z;
        ignoreLayers = ~(1 << 8 | 1 << 9 | 1 << 10);
        targetTransform = FindObjectOfType<PlayerManager>().transform;
        Cursor.lockState = CursorLockMode.Locked; //lock the curson to the center of the screen
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
