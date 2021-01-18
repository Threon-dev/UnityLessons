using UnityEngine;
using UnityEngine.EventSystems;
[RequireComponent(typeof(CharacterController))]
public class PointClickMovement : MonoBehaviour
{
    
    [SerializeField] 
    private Transform target;

    private ControllerColliderHit _contact;
    private Animator _animator;
    private CharacterController _charController;

    private float _curSpeed = 0f;
    private Vector3 _targetPos = Vector3.one;

    public float deceleration = 25.0f;
    public float targetBuffer = 1.5f;
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;
    bool hitGround = false;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _charController = GetComponent<CharacterController>();
        _vertSpeed = minFall;
    }
    private void Update()
    {
        Vector3 movement = Vector3.zero;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;

            if (Physics.Raycast(ray, out mouseHit))
            {
                GameObject hitObject = mouseHit.transform.gameObject;

                if (hitObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    _targetPos = mouseHit.point;
                    _curSpeed = moveSpeed;
                }
            }
        }
        if (_targetPos != Vector3.one)
        {
            if (_curSpeed > moveSpeed * .5f)
            {
                Vector3 adjustedPos = new Vector3(_targetPos.x, transform.position.y, _targetPos.z);
                Quaternion targetRot = Quaternion.LookRotation(adjustedPos - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            }

            movement = _curSpeed * Vector3.forward;
            movement = transform.TransformDirection(movement);

            if (Vector3.Distance(_targetPos, transform.position) < targetBuffer)
            {
                _curSpeed -= deceleration * Time.deltaTime;

                if (_curSpeed <= 0)
                {
                    _targetPos = Vector3.one;
                }
            }
        }
        _animator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _contact = hit;
    }
}
