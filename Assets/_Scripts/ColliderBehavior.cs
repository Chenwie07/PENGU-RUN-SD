using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBehavior : StateMachineBehaviour
{
    // Collider properties to affect when in this state of animation
    [Header ("Collider On Entry")]
    [SerializeField] Vector3 _colliderCenter;
    [SerializeField] private float _height;
    [SerializeField] private float _radius;

    private CharacterController _characterController;
    private Vector3 _resetCenter = new Vector3(0, .83f, -.05f);
    private float _resetHeight = 1.8f;
    private float _resetRadius = .2f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _characterController = FindObjectOfType<CharacterController>();
        _characterController.center = _colliderCenter;
        _characterController.height = _height;
        _characterController.radius = _radius; 
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _characterController = FindObjectOfType<CharacterController>();
        _characterController.center = _resetCenter;
        _characterController.height = _resetHeight;
        _characterController.radius = _resetRadius;
    }
}
