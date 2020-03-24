using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    //config
    [SerializeField] float _rcsThrust = 2f;
    [SerializeField] float _mainThrust;

    //game objects
    Rigidbody _rigidBodyRocket;
    AudioSource _mainEngineAudio;
    bool _touchDown = false;
    int _collisions = 0;
    bool _mainEngineToggle;
    float _rotationSpeed = 0;

    void Start()
    {
        _rigidBodyRocket = GetComponent<Rigidbody>();
        _mainEngineAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }



    private void ProcessInput()
    {
        Thrusters();
        Rotate();

    }

    private void Rotate()
    {
        bool thrusterOverload = (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D));
        if (thrusterOverload)
        {

        }
        else if (_touchDown) {
            //when touching ground turn off relativity
            if (Input.GetKey(KeyCode.A))
            {
                _rigidBodyRocket.freezeRotation = true;
                transform.Rotate(Vector3.forward * 10 * Time.deltaTime);
                _rigidBodyRocket.freezeRotation = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rigidBodyRocket.freezeRotation = true;
                transform.Rotate(Vector3.back * 10 * Time.deltaTime);
                _rigidBodyRocket.freezeRotation = false;
            }
            
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                _rigidBodyRocket.freezeRotation = true;
                _rotationSpeed += (_rcsThrust * Time.deltaTime);
                _rigidBodyRocket.freezeRotation = false;
            }
            if (Input.GetKey(KeyCode.D))
            {
                _rigidBodyRocket.freezeRotation = true;
                _rotationSpeed -= (_rcsThrust * Time.deltaTime);
                _rigidBodyRocket.freezeRotation = false;
            }
            transform.Rotate(Vector3.forward * _rotationSpeed);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        _rotationSpeed = 0;
        _touchDown = true;
        _collisions = 1;
        if (!collision.gameObject.CompareTag("friendly")) 
        {
            print("BAD!!!");
        }
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        _collisions -= 1;
        if (_collisions == 0) {
            _touchDown = false;
        }
    }
    private void Thrusters()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (!_mainEngineAudio.isPlaying)
            {
                _mainEngineAudio.Play();
                _mainEngineToggle = true;
            }
            _rigidBodyRocket.AddRelativeForce(Vector3.up * _mainThrust * Time.deltaTime);
        }
        else if (_mainEngineAudio.isPlaying)
        {
            _mainEngineAudio.Stop();
        }

    }
}
