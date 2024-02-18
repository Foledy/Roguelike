using Unity.Mathematics;
using UnityEngine;
 
[RequireComponent(typeof(CharacterController))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _lookSpeedMouse;
    [SerializeField] private int _jumpHeight;
    [SerializeField] private float _gravity;
 
    private Vector2 _rotation;
    private CharacterController _characterController;
    private float _velocity = 0f;
 
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
 
    public void MouseLook(float2 input)
    {
        float mouseX = input.x * _lookSpeedMouse * Time.deltaTime;
        float mouseY = input.y * _lookSpeedMouse * Time.deltaTime;
 
        _rotation.y += mouseX;
        _rotation.x -= mouseY;
 
        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);
 
        _camera.transform.eulerAngles = new Vector3(_rotation.x, _rotation.y, 0);
 
    }
 
    public void Move(InputData inputData, MoveData moveData)
    {
        float horizontal = inputData.Move.x * moveData.MoveSpeed * Time.deltaTime;
        float vertical = inputData.Move.y * Time.deltaTime;
 
        if (_characterController.isGrounded)
        {
            _velocity = 0;
        }
 
        _velocity += inputData.Jump == 1 ? Mathf.Sqrt(_jumpHeight * _gravity) : -_gravity * Time.deltaTime;
        vertical *= inputData.Sprint == 1 ? moveData.SprintBoost : moveData.MoveSpeed;
 
        _characterController.Move((_camera.transform.right * horizontal + _camera.transform.forward * vertical + new Vector3(0, _velocity, 0)) * Time.deltaTime);
    }
}