using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    public static InputManager Instance;
    private InputControls inputControls;

    public Vector2 Move => inputControls.Player.Move.ReadValue<Vector2>();

    public InputManager()
    {
        Instance = this;
        inputControls = new InputControls();
        inputControls.Enable();

        inputControls.Player.Interact.performed += OnInteractPerformed;
        inputControls.Player.FleeBattle.performed += OnFleeBattlePerformed;
    }

    private void OnFleeBattlePerformed(InputAction.CallbackContext obj)
    {
        if(GameManager.Instance.canFlee && GameManager.Instance.inCombat) GameManager.Instance.endCombat = true;
    }

    private void OnInteractPerformed(InputAction.CallbackContext obj)
    { 
        if (GameManager.Instance.selectedEnemy.inRange)
        {
            GameManager.Instance.inCombat = true;
            GameManager.Instance.SetEnemyVariables();
            GameManager.Instance.CameraController(true);
        }
    }

    public void EnableMovement() => inputControls.Player.Move.Enable();
    public void DisableMovement() => inputControls.Player.Move.Disable();
}
