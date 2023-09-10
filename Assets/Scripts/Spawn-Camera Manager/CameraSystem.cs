using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;



public class CameraSystem : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private bool useEdgeScrolling = false;
    [SerializeField] private bool useDragPan = false;
    [SerializeField] private float fieldOfViewMax = 50;
    [SerializeField] private float fieldOfViewMin = 10;
    [SerializeField] private float followOffsetMin = 5f;
    [SerializeField] private float followOffsetMax = 50f;
    [SerializeField] private float followOffsetMinY = 10f;
    [SerializeField] private float followOffsetMaxY = 50f;

    private bool dragPanMoveActive;
    private bool dragPanRotateActive;

    private Vector2 lastMousePosition;
    private float targetFieldOfView = 50;
    private Vector3 followOffset;

    [SerializeField] private float zoomSpeed = 10f;
    private Vector3 rotationOffset;
    private float minOffsetY;
    private float maxOffsetY;
    private float rotateAngle;
    private float rotateAngleY;
    private CameraControls cameraActions;

    private void Awake()
    {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        rotationOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
        cameraActions = new CameraControls();
    }

    private void OnEnable()
    {

        cameraActions.Camera.RotateCamera.performed += RotateCamera;
        cameraActions.Camera.RotateCamera.performed += RotateCameraYZ;
        cameraActions.Camera.Enable();
    }

    private void OnDisable()
    {
        cameraActions.Camera.RotateCamera.performed -= RotateCamera;
        cameraActions.Camera.RotateCamera.performed -= RotateCameraYZ;
        cameraActions.Camera.Disable();
    }

    private void Start()
    {
        //       cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
        //Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }
    private void Update()
    {
        HandleCameraMovement();

        if (useEdgeScrolling)
        {
            HandleCameraMovementEdgeScrolling();
        }

        if (useDragPan)
        {
            HandleCameraMovementDragPan();
        }

        HandleCameraRotation();
        HandleCameraRotatingDragPan();

       // HandleCameraZoom_FieldOfView();
        //HandleCameraZoom_MoveForward();
        HandleCameraZoom_LowerY();

    }

    private void HandleCameraMovement()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 100f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        int edgeScrollSize = 20;

        if (Input.mousePosition.x < edgeScrollSize)
        {
            inputDir.x = -1f;
        }
        if (Input.mousePosition.y < edgeScrollSize)
        {
            inputDir.z = -1f;
        }
        if (Input.mousePosition.x > Screen.width - edgeScrollSize)
        {
            inputDir.x = +1f;
        }
        if (Input.mousePosition.y > Screen.height - edgeScrollSize)
        {
            inputDir.z = +1f;
        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 100f;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private void HandleCameraMovementDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(0))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;

        }
        if (Input.GetMouseButtonUp(0))
        {
            dragPanMoveActive = false;
        }

        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;

            float dragPanSpeed = 0.5f;
            inputDir.x = mouseMovementDelta.x * dragPanSpeed;
            inputDir.z = mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;


        }

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

        float moveSpeed = 50f;
        transform.position -= moveDir * moveSpeed * Time.deltaTime;
    }


    private void HandleCameraRotatingDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);



        if (Input.GetMouseButtonDown(1))
        {
            dragPanRotateActive = true;
            lastMousePosition = Input.mousePosition;

            float minY = 301+ cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z;
            if (cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y <=380)
            {
                minOffsetY= 80;
            }
            else
            {
                minOffsetY =  cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y - minY;
            }
            float maxY = -cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z -1;

            maxOffsetY = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y +maxY;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragPanRotateActive = false;
        }

        if (dragPanRotateActive)
        {

            Vector2 mouseRotationDelta = (Vector2)Input.mousePosition - lastMousePosition;

            float dragPanSpeed = 0.5f;
            inputDir.z = mouseRotationDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;

            float rotationSpeed = 70f;
            Vector3 moveDir = transform.forward * inputDir.z;

            rotationOffset.z = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z -moveDir.z;
            followOffset.y = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y - moveDir.z;

            rotateAngle = Mathf.Clamp(rotationOffset.z, -300f, -1f);
            rotateAngleY = Mathf.Clamp(followOffset.y, minOffsetY, maxOffsetY);
            if (cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y>81)
            {
                cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z =
         Mathf.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z, rotateAngle, Time.deltaTime * rotationSpeed);
            }

            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y =
Mathf.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y, rotateAngleY, Time.deltaTime * 70f);

        }


    }

    private void RotateCamera(InputAction.CallbackContext obj)
    {
        if (!Mouse.current.middleButton.isPressed && !Mouse.current.rightButton.isPressed)
            return;

        float inputValue = obj.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, inputValue * 0.1f + transform.rotation.eulerAngles.y, 0f);
    }
    private void RotateCameraYZ(InputAction.CallbackContext obj)
    {
        //if (!Mouse.current.middleButton.isPressed && !Mouse.current.rightButton.isPressed)
        //    return;

        //float inputValue = obj.ReadValue<Vector2>().y;
        //transform.rotation = Quaternion.Euler(0f, inputValue * 0.1f + transform.rotation.eulerAngles.y, 0f);
    }

    private void HandleCameraRotation()
    {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        float rotateSpeed = 50f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }



    private void HandleCameraZoom_MoveForward()
    {
        Vector3 zoomDir = followOffset.normalized;

        float zoomAmount = 30f;
        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset -= zoomDir * zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset += zoomDir * zoomAmount;
        }

        if (followOffset.magnitude < followOffsetMin)
        {
            followOffset = zoomDir * followOffsetMin;
        }

        if (followOffset.magnitude > followOffsetMax)
        {
            followOffset = zoomDir * followOffsetMax;
        }

        float zoomSpeed = 10f;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
            Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
    }

    private void HandleCameraZoom_FieldOfView()
    {
        if (cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y > 170)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                targetFieldOfView -= 5;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                targetFieldOfView += 5;
            }

            targetFieldOfView = Mathf.Clamp(targetFieldOfView, 70, 120);

            float zoomSpeed = 10f;
            cinemachineVirtualCamera.m_Lens.FieldOfView =
                Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);

        }
    }
    private void HandleCameraZoom_LowerY()
    {
        float zoomAmount = 30f;

        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset.y -= zoomAmount;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            followOffset.y += zoomAmount;
        }
        if (!dragPanRotateActive)
        {
            followOffset.y = Mathf.Clamp(followOffset.y, followOffsetMinY, followOffsetMaxY);

            float zoomSpeed = 10f;



            cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y =
            Mathf.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.y, followOffset.y, Time.deltaTime * zoomSpeed);

            // cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z = Mathf.SmoothDamp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset.z, transform.position.z,r, 10f );

        }

    }

}
