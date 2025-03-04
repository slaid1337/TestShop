using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private float _moveSpeed;
    private Rigidbody rb;
    private InputSystem_Actions _input;
    
    [SerializeField] private float sensitivity = 1f;
    private float yRotationLimit = 90f;

    [SerializeField] private Transform _hand;

    Vector2 rotation = Vector2.zero;

    private MoveableObject _moveableObject;

    [Inject] private SceenButtonsController _buttonsController;

    private void Start()
    {
        _input = new InputSystem_Actions();
        _input.Enable();

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Move();

        RotateCamera();

        if (_input.Player.Attack.IsPressed() && !_moveableObject)
        {
            print("Ssssssss");
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.PositiveInfinity))
            {
                if (hit.transform.TryGetComponent<MoveableObject>(out MoveableObject moveableObject))
                {
                    _moveableObject = moveableObject;
                    _moveableObject.Attach(_hand);
                    _buttonsController.EnablePushButton();
                }
            }
        }

        if (_input.Player.Interact.IsPressed() && _moveableObject)
        {
            _moveableObject.Push(_camera.transform.forward * 1000f);
            _moveableObject = null;
            _buttonsController.DisablePushButton();
        }
    }

    private void Move()
    {
        Vector2 move = _input.Player.Move.ReadValue<Vector2>();

        float moveHorizontal = move.x;
        float moveVertical = move.y;

        Vector3 forward = _camera.transform.TransformDirection(Vector3.forward);
        Vector3 right = _camera.transform.TransformDirection(Vector3.right);

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized;

        Vector3 moveDirection = (right * moveHorizontal + forward * moveVertical).normalized;

        rb.velocity = new Vector3(moveDirection.x * _moveSpeed, rb.velocity.y, moveDirection.z * _moveSpeed);
    }

    private void RotateCamera()
    {
        if (EventSystem.current.IsPointerOverUIObject()) return;

        Vector2 lookDelta = _input.Player.Look.ReadValue<Vector2>();

        rotation.x += lookDelta.x * sensitivity;
        rotation.y += lookDelta.y * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        _camera.transform.localRotation = xQuat * yQuat;
    }
}
