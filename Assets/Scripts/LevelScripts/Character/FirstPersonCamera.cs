using System;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
 
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Collider))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _lookSpeedMouse;
    [SerializeField] private int _jumpHeight;
    [SerializeField] private float _gravity;
    
    private Vector2 _rotation;
    private Collider _collider;
    private CharacterController _characterController;
 
    private void Start()
    {
        _collider = GetComponent<Collider>();
        _characterController = GetComponent<CharacterController>();
    }
 
    public void MouseLook(float2 input)
    {
        var mouseX = input.x * _lookSpeedMouse * Time.deltaTime;
        var mouseY = input.y * _lookSpeedMouse * Time.deltaTime;
 
        _rotation.y += mouseX;
        _rotation.x -= mouseY;
 
        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);
 
        _camera.transform.eulerAngles = new Vector3(_rotation.x, _rotation.y, 0);
 
    }
 
    public void Move(InputData inputData, MoveData moveData)
    {
        var horizontal = inputData.Move.x * moveData.MoveSpeed * Time.deltaTime;
        var vertical = inputData.Move.y * Time.deltaTime;
        var cameraTransform = _camera.transform;
        
        vertical *= inputData.Sprint == 1 ? moveData.MoveSpeed * moveData.SprintBoost : moveData.MoveSpeed;
        
        _characterController.Move((cameraTransform.right * horizontal + cameraTransform.forward * vertical + new Vector3(0, 0, 0)) * Time.deltaTime);
    }
}