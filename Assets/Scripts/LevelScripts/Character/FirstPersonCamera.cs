using System;
using System.Linq;
using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;
 
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Collider))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _lookSpeedMouse;
    
    private Vector2 _rotation;
    private CharacterController _characterController;
    private Transform _cameraTransform;
 
    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = _camera.transform;
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
        
        vertical *= inputData.Sprint == 1 ? moveData.MoveSpeed * moveData.SprintBoost : moveData.MoveSpeed;

        var direction = transform.right * horizontal + transform.forward * vertical;
        direction = _cameraTransform.TransformDirection(direction);
        direction = new Vector3(direction.x, -9.8f, direction.z);
        
        _characterController.Move(direction * Time.deltaTime);
    }
}