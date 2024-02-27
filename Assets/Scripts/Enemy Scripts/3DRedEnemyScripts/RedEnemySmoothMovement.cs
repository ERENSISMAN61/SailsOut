using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine.InputSystem.HID;
using Cinemachine.Utility;

[RequireComponent(typeof(NavMeshAgent))]
public class redEnemySmoothMovement : MonoBehaviour
{



    [Header("Random Destination")]  ///////////
    //[SerializeField]
    //private Transform centerPoint;
    //[SerializeField]
    //private float range;
    //private Vector3 startPoint;
    //private Vector3 destinationPoint;
    [SerializeField]
    private float Radius = 80;
    [SerializeField]
    private bool Debug_Bool;

    Vector3 Next_Position;



    [Header("")]
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    private LayerMask FloorLayer;
    [SerializeField]
    private bool UsePathSmoothing;
    [Header("Path Smoothing")]
    [SerializeField]
    private float SmoothingLength = 0.25f;
    [SerializeField]
    private int SmoothingSections = 10;
    [SerializeField]
    [Range(-1, 1)]
    private float SmoothingFactor = 0;
    private NavMeshAgent Agent;
    private NavMeshPath CurrentPath;
    public Vector3[] PathLocations = new Vector3[0];
    [SerializeField]
    private int PathIndex = 0;

    [Header("Movement Configuration")]
    [SerializeField]
    [Range(0, 0.99f)]
    private float Smoothing = 0.25f;
    [SerializeField]
    private float TargetLerpSpeed = 1;

    [SerializeField]
    private Vector3 TargetDirection;
    private float LerpTime = 0;
    [SerializeField]
    private Vector3 MovementVector;

    private Vector3 InfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

    public bool isTargetEnemy = false;
    private GameObject Player;
    private Vector3 playerPosition;

    public bool didCatch = false;

    [Header("Pursuit Parameters")]
    [SerializeField] private float pursuitSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        CurrentPath = new NavMeshPath();

        Camera = Camera.main;
    }
    private void Start()
    {
        isTargetEnemy = false;
        Next_Position = transform.position;     ///////////
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {

        //  Debug.Log("isTargetEnemy:   "+isTargetEnemy);

        
        playerPosition = Player.transform.position;

        PathLocations = new Vector3[0];


        Agent.SetDestination(playerPosition);

        Next_Position = transform.position;
        PathIndex = 0;

        Pursue(playerPosition);



        MoveAgent();
    }
    private void OnDrawGizmos()
    {
        if (Debug_Bool)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawLine(transform.position, Next_Position); ///////////
        }
    }

    //Aşağıdaki Pursue fonksiyonu, daha gerçekçi bir rotasyon sağlamak için kullanılmıştır.
    //Oyuncu gemisini takip ederken azcık sağ sol yaparak ilerliyordu fakat bu fonksiyonla onu ortadan kaldırdık.
    private void Pursue(Vector3 targetPosition)
    {
        Vector3 targetDirection = (targetPosition - transform.position).normalized;
        float step = rotationSpeed * Time.deltaTime;

        // Rotate towards the player
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);

    }

    public void SetDestinationPlus(Vector3 Position)
    {
        Agent.ResetPath();
        NavMesh.CalculatePath(transform.position, Position, Agent.areaMask, CurrentPath);
        Vector3[] corners = CurrentPath.corners;

        if (corners.Length > 2)
        {
            BezierCurve[] curves = new BezierCurve[corners.Length - 1];

            SmoothCurves(curves, corners);

            PathLocations = GetPathLocations(curves);

            PathIndex = 0;
        }
        else
        {
            PathLocations = corners;
            PathIndex = 0;
        }
    }

    //public void SetAgentDestination(Vector3 Position)
    //{
    //    PathIndex = int.MaxValue;
    //    Agent.SetDestination(Position);
    //    PathLocations = Agent.path.corners;
    //}



    private void MoveAgent()
    {
        if (PathIndex >= PathLocations.Length)
        {
            return;
        }

        if (Vector3.Distance(transform.position, PathLocations[PathIndex] + (Agent.baseOffset * Vector3.up)) <= Agent.radius)
        {
            PathIndex++;
            LerpTime = 0;

            if (PathIndex >= PathLocations.Length)
            {
                return;
            }
        }

        MovementVector = (PathLocations[PathIndex] + (Agent.baseOffset * Vector3.up) - transform.position).normalized;

        TargetDirection = Vector3.Lerp(
            TargetDirection,
            MovementVector,
            Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - Smoothing))
        );

        Vector3 lookDirection = MovementVector;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lookDirection),
                Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - Smoothing))
            );
        }

        Agent.Move(TargetDirection * Agent.speed * Time.deltaTime);

        LerpTime += Time.deltaTime;
    }

    private void HandleInput()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Ray ray = Camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, FloorLayer))
            {
                //if(Vector3.Distance(hit.point, transform.position)>=50)
                //{
                SetDestinationPlus(hit.point);
                //}
                //if (UsePathSmoothing)
                //{

                //    SetDestinationPlus(hit.point);
                //}
                //else
                //{
                //    SetAgentDestination(hit.point);
                //}
            }
        }
    }

    //private void RandomDestination()   ///////////
    //{

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Debug.Log("Space pressed");
    //        Vector3 point;
    //        if (RandomPoint(centerPoint.position, range, out point)) //pass in our centre point and radius of area
    //        {
    //            Debug.Log("Random point found");
    //            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
    //            SetDestinationPlus(point);
    //        }

    //    }

    //}
    private Vector3 RandomPointGenerator(Vector3 StartPoint, float Radius)
    {
        Vector3 Dir = Random.insideUnitSphere * Radius;
        Dir += StartPoint;
        NavMeshHit Hit_;
        Vector3 FinalPos = Vector3.zero;
        if (NavMesh.SamplePosition(Dir, out Hit_, Radius, 1))
        {
            FinalPos = Hit_.position;
        }
        return FinalPos;

    }

    //bool RandomPoint(Vector3 center, float range, out Vector3 result) ///////////
    //{

    //    Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
    //    NavMeshHit hit;
    //    if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
    //    {
    //        //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
    //        //or add a for loop like in the documentation
    //        result = hit.position;
    //        return true;
    //    }

    //    result = Vector3.zero;
    //    return false;
    //}
    private Vector3[] GetPathLocations(BezierCurve[] Curves)
    {
        Vector3[] pathLocations = new Vector3[Curves.Length * SmoothingSections];

        int index = 0;
        for (int i = 0; i < Curves.Length; i++)
        {
            Vector3[] segments = Curves[i].GetSegments(SmoothingSections);
            for (int j = 0; j < segments.Length; j++)
            {
                pathLocations[index] = segments[j];
                index++;
            }
        }

        pathLocations = PostProcessPath(Curves, pathLocations);

        return pathLocations;
    }

    private Vector3[] PostProcessPath(BezierCurve[] Curves, Vector3[] Path)
    {
        Vector3[] path = RemoveOversmoothing(Curves, Path);

        path = RemoveTooClosePoints(path);

        path = SamplePathPositions(path);

        return path;
    }

    private Vector3[] SamplePathPositions(Vector3[] Path)
    {
        for (int i = 0; i < Path.Length; i++)
        {
            if (NavMesh.SamplePosition(Path[i], out NavMeshHit hit, Agent.radius * 1.5f, Agent.areaMask))
            {
                Path[i] = hit.position;
            }
            else
            {
                Debug.LogWarning($"No NavMesh point close to {Path[i]}. Check your smoothing settings!");
                Path[i] = InfinityVector;
            }
        }

        return Path.Except(new Vector3[] { InfinityVector }).ToArray();
    }

    private Vector3[] RemoveTooClosePoints(Vector3[] Path)
    {
        if (Path.Length <= 2)
        {
            return Path;
        }

        int lastIndex = 0;
        int index = 1;
        for (int i = 0; i < Path.Length - 1; i++)
        {
            if (Vector3.Distance(Path[index], Path[lastIndex]) <= Agent.radius)
            {
                Path[index] = InfinityVector;
            }
            else
            {
                lastIndex = index;
            }
            index++;
        }

        return Path.Except(new Vector3[] { InfinityVector }).ToArray();
    }

    private Vector3[] RemoveOversmoothing(BezierCurve[] Curves, Vector3[] Path)
    {
        if (Path.Length <= 2)
        {
            return Path;
        }

        int index = 1;
        int lastIndex = 0;
        for (int i = 0; i < Curves.Length; i++)
        {
            Vector3 targetDirection = (Curves[i].EndPosition - Curves[i].StartPosition).normalized;

            for (int j = 0; j < SmoothingSections - 1; j++)
            {
                Vector3 segmentDirection = (Path[index] - Path[lastIndex]).normalized;
                float dot = Vector3.Dot(targetDirection, segmentDirection);
                //      Debug.Log($"Target Direction: {targetDirection}. segment direction: {segmentDirection} = dot {dot} with index {index} & lastIndex {lastIndex}");
                if (dot <= SmoothingFactor)
                {
                    Path[index] = InfinityVector;
                }
                else
                {
                    lastIndex = index;
                }

                index++;
            }

            index++;
        }

        Path[Path.Length - 1] = Curves[Curves.Length - 1].EndPosition;

        Vector3[] TrimmedPath = Path.Except(new Vector3[] { InfinityVector }).ToArray();

        //   Debug.Log($"Original Smoothed Path: {Path.Length}. Trimmed Path: {TrimmedPath.Length}");

        return TrimmedPath;
    }

    private void SmoothCurves(BezierCurve[] Curves, Vector3[] Corners)
    {
        for (int i = 0; i < Curves.Length; i++)
        {
            if (Curves[i] == null)
            {
                Curves[i] = new BezierCurve();
            }

            Vector3 position = Corners[i];
            Vector3 lastPosition = i == 0 ? Corners[i] : Corners[i - 1];
            Vector3 nextPosition = Corners[i + 1];

            Vector3 lastDirection = (position - lastPosition).normalized;
            Vector3 nextDirection = (nextPosition - position).normalized;

            Vector3 startTangent = (lastDirection + nextDirection) * SmoothingLength;
            Vector3 endTangent = (nextDirection + lastDirection) * -1 * SmoothingLength;

            Curves[i].Points[0] = position; // Start Position (P0)
            Curves[i].Points[1] = position + startTangent; // Start Tangent (P1)
            Curves[i].Points[2] = nextPosition + endTangent; // End Tangent (P2)
            Curves[i].Points[3] = nextPosition; // End Position (P3)
        }


        // Apply look-ahead for first curve and retroactively apply the end tangent
        {
            Vector3 nextDirection = (Curves[1].EndPosition - Curves[1].StartPosition).normalized;
            Vector3 lastDirection = (Curves[0].EndPosition - Curves[0].StartPosition).normalized;

            Curves[0].Points[2] = Curves[0].Points[3] +
                (nextDirection + lastDirection) * -1 * SmoothingLength;
        }
    }
}
