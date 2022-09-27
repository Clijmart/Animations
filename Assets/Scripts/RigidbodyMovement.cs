using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyMovement : MonoBehaviour
{
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    private bool isGrounded;

    [SerializeField] private Animator animator;

    [SerializeField] private LayerMask FloorMask;
    [SerializeField] private Transform FeetTransform;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody PlayerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce;
    [SerializeField] private float CameraAngle;

    private void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (PlayerMovementInput.x != 0 || PlayerMovementInput.z != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if (Physics.CheckSphere(FeetTransform.position, 0.1f, FloorMask))
        {
            isGrounded = true;
            print("Not Jumping");
            animator.SetBool("IsJumping", false);
        }
        else
        {
            isGrounded = false;
            print("Jumping");
            animator.SetBool("IsJumping", true);
        }


        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), 0f);

        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        PlayerBody.velocity = new Vector3(MoveVector.x, PlayerBody.velocity.y, MoveVector.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            }
        }
    }

    private void MovePlayerCamera()
    {
        xRot -= PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot + CameraAngle, 0f, 0f);
    }


}

