using UnityEngine;
using UnityEngine.InputSystem;

public class Character : MonoBehaviour
{
    private const float GRAVITY_ACCELERATION = 9.81f;

    private ControlScheme _controlScheme;

    private Vector2 _movementDirection = Vector2.zero;

    private

    // Start is called before the first frame update
    void Start()
    {
        _controlScheme = new ControlScheme();
        _controlScheme.Enable();
        _controlScheme.Yeet.MovementX.performed += OnMovementX;
        _controlScheme.Yeet.MovementX.canceled += OnMovementX;

        _controlScheme.Yeet.MovementY.performed += OnMovementY;
        _controlScheme.Yeet.MovementY.canceled += OnMovementY;

        _controlScheme.Yeet.Action.performed += OnAction;
        _controlScheme.Yeet.SelectNextCharacter.performed += OnSelectNextCharacter;
    }

    void OnDestroy()
    {
        _controlScheme.Yeet.MovementX.performed -= OnMovementX;
        _controlScheme.Yeet.MovementX.canceled -= OnMovementX;

        _controlScheme.Yeet.MovementY.performed -= OnMovementY;
        _controlScheme.Yeet.MovementY.canceled -= OnMovementY;

        _controlScheme.Yeet.Action.performed -= OnAction;
        _controlScheme.Yeet.SelectNextCharacter.performed -= OnSelectNextCharacter;

        _controlScheme.Disable();
        _controlScheme = null;
    }

    // Update is called once per frame
    void Update() { }

    void FixedUpdate()
    {
        Debug.Log($"Movement: {_movementDirection}");
    }

    private float ApplyGravity(float currentVerticalVelocity)
    {
        return 0f;
    }

    public void SetEnabled(bool f)
    {

    }

    private void OnAction(InputAction.CallbackContext obj)
    {
        Debug.Log("ACTION!");
    }

    private void OnSelectNextCharacter(InputAction.CallbackContext obj)
    {
        Debug.Log("SWITCH!");
    }

    private void OnMovementX(InputAction.CallbackContext obj) => _movementDirection.x = obj.ReadValue<float>();
    private void OnMovementY(InputAction.CallbackContext obj) => _movementDirection.y = obj.ReadValue<float>();
}
