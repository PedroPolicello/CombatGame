using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private GameObject playerBody;
    public Animator animator;
    public Transform particleSpawnPosition;
    private CharacterController characterController;
    private bool inShop;

    private void Awake() => characterController = GetComponent<CharacterController>();

    private void FixedUpdate()
    {
        Vector3 moveDirection = new Vector3(InputManager.Instance.Move.x, 0, InputManager.Instance.Move.y);
        if(moveDirection.x != 0 || moveDirection.z != 0) animator.SetBool("isWalking", true);
        else animator.SetBool("isWalking", false);
        characterController.SimpleMove(moveDirection * moveSpeed);
        RotatePlayerAccordingToInput(moveDirection);
    }

    private void RotatePlayerAccordingToInput(Vector3 moveDirection)
    {
        Vector3 pointToLookAt;
        pointToLookAt.x = moveDirection.x;
        pointToLookAt.y = 0;
        pointToLookAt.z = moveDirection.z;

        Quaternion currentRotation = playerBody.transform.rotation;

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotarion = Quaternion.LookRotation(pointToLookAt);

            playerBody.transform.rotation = Quaternion.Slerp(currentRotation, targetRotarion, moveSpeed * Time.deltaTime);
        }
    }
}
