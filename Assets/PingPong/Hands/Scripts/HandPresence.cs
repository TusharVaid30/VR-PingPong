using System;
using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;

    private InputDevice targetDevice;

    private Animator hand;

    private Transform ball;
    
    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball").transform;
        
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
            targetDevice = devices[0];

        hand = GetComponent<Animator>();
    }

    private void Update()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.grip, out float trigger);
        if (trigger > .1f)
            if (Vector3.Distance(transform.position, ball.position) > .1f)
                hand.SetFloat("Hold", -trigger);
            else
                hand.SetFloat("Hold", trigger);
        else
            hand.SetFloat("Hold", 0f);
    }
}
