using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]private float speed;
        private float moveSpeed;
        [SerializeField]private float walkSpeed = 5;
        [SerializeField]private float moveForce = 20;
        [SerializeField]private float playerHeight = 1.6f;
        [SerializeField]private float moveControl = 1;

        [SerializeField]private Vector3 camOffset;

        [Header("Slope Handling")]
        [SerializeField, Tooltip("If the angle of a slope exceeds this number than script wont detect it as a slope.")] private float maxSlopeAngle = 45f;
        private RaycastHit slopeHit;
        [SerializeField] private float exitSlopeTime = 0.2f;

        [Header("References")]
        [SerializeField]private Rigidbody rb;
        [SerializeField]private Horde inputActions;
        private Transform cam;

        void Awake()
        {
            inputActions = new Horde();
            inputActions.Player.Enable();

            cam = Camera.main.transform;

            moveSpeed = walkSpeed;
        }

        void Update()
        {
            speed = GetMovementSpeed();
            Rotate();
            SpeedControl();
        }

        void FixedUpdate()
        {
            Move();
            cam.position = new Vector3(transform.position.x, cam.position.y, transform.position.z) + camOffset;
        }

        private void SpeedControl()
        {
            //If player is on slope it will set 3 axis instead of 2 becose player is faster on slopes
            if (IsOnSlope())
            {
                //Check if player is moving faster than it should
                if (rb.linearVelocity.magnitude > moveSpeed)
                {
                    //Sets player velocity
                    rb.linearVelocity = rb.linearVelocity.normalized * moveSpeed;
                }
            }
            else
            {
                //Gets current velocity without up/down velocity axis
                Vector3 curVel = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

                //Check if player is moving faster than he should
                if (curVel.magnitude > moveSpeed)
                {
                    //Create new velocity that is set to the max speed 
                    Vector3 fixedVel = curVel.normalized * moveSpeed;
                    //Sets the new velocity
                    rb.linearVelocity = new Vector3(fixedVel.x, rb.linearVelocity.y, fixedVel.z);
                }
            }
        }

        public float GetMovementSpeed()
        {
            return new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        }

        #region Slope Methods
        public bool IsOnSlope()
        {
            //Shoots Raycast down to detect slope(hopefully)
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.3f))
            {
                //Gets angle of the slope
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                //Returns bool based on specified parameters
                return angle < maxSlopeAngle && angle != 0f;
            }

            return false;
        }

        private float GetSlopeAngle()
        {
            //Shoots Raycast down to detect slope(hopefully)
            if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.3f))
            {
                //Gets angle of the slope and returns it
                float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                return angle;
            }
            //Lets hope code never gets here
            else return 0f;
        }

        public Vector3 GetSlopeMoveDirection()
        {
            //Finds the direction the player should move when standing on a slope (points uphill)
            return Vector3.ProjectOnPlane(Vector3.up, slopeHit.normal).normalized;
        }

        public Vector3 GetSlopeMoveDirection(Vector3 inputDir)
        {
            //Finds the direction the player should move when standing on a slope (points uphill)
            return Vector3.ProjectOnPlane(inputDir, slopeHit.normal).normalized;
        }

        public void ExitSlope()
        {
            Invoke(nameof(ResetExitSlope), exitSlopeTime);
        }

        private void ResetExitSlope()
        {
        }
        #endregion

        private void Rotate()
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaycastHit hit;
        
            if (Physics.Raycast(ray, out hit, 100))
            {
                hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, Vector3.Distance(cam.position, hit.point))));
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z);
            }
            
        }

        private void Move()
        {
            Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

            if (IsOnSlope())
            {
                rb.AddForce(Vector3.down * 20f);

                rb.AddForce(GetSlopeMoveDirection(Vector3.forward) * moveSpeed * moveControl * 55f * inputVector.y, ForceMode.Force);
                rb.AddForce(Vector3.right * moveSpeed * 55 * moveControl * inputVector.x, ForceMode.Force);

                if (rb.linearVelocity.y > 0 && inputVector != Vector2.zero)
                {
                    rb.AddForce(Vector3.down * 90f * Mathf.Abs(GetSlopeAngle() / 20), ForceMode.Force);
                }
                else if (rb.linearVelocity.y < 0 && inputVector != Vector2.zero)
                {
                    rb.AddForce(Vector3.down * 90f * Mathf.Abs(GetSlopeAngle() / 10), ForceMode.Force);
                }
            }
            else
            {
                rb.AddForce(Vector3.forward * inputActions.Player.Move.ReadValue<Vector2>().y * moveForce + Vector3.right * inputActions.Player.Move.ReadValue<Vector2>().x * moveForce, ForceMode.Force);
            }
        }
    }
}
