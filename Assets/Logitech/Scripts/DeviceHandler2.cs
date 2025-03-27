using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeviceHandler2 : MonoBehaviour {

    //public GameObject LeftHand;
    public GameObject RightHand;
    public MeshRenderer[] rens;
    
    private void Awake() {
        InputDevices.deviceConnected += DeviceConnected;
        InputDevices.deviceDisconnected += DeviceDisconnected;
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        foreach (InputDevice device in devices) {
            DeviceConnected(device);
        }
    }

    private void Start() {
      ShowRens(false);  
    }

    private void OnDestroy() {
        InputDevices.deviceConnected -= DeviceConnected;
    }

    private void DeviceDisconnected(InputDevice device) {
        Debug.Log($"Device disconnected: {device.name}");
        bool mxInkDisconnected = device.name.ToLower().Contains("logitech");
        if (mxInkDisconnected) {
            Debug.Log("*** MX Ink Disconnected ***");

            //LeftHand.SetActive(false);
            RightHand.SetActive(false);
            ShowRens(false);
        }
    }

    private void DeviceConnected(InputDevice device) {
        Debug.Log($"Device connected: {device.name}");
        bool mxInkConnected = device.name.ToLower().Contains("logitech");
        if (mxInkConnected) {
            Debug.Log("*** MX Ink Connected ***");

            //bool isOnRightHand = (device.characteristics & InputDeviceCharacteristics.Right) != 0;
            //LeftHand.SetActive(!isOnRightHand);
            RightHand.SetActive(true); //isOnRightHand);
            ShowRens(true);

            /*
            MxInkHandler MxInkStylus = FindFirstObjectByType<MxInkHandler>();
            if (MxInkStylus) {
                MxInkStylus.SetHandedness(true); //isOnRightHand);
                LineDrawing lineDrawing = FindFirstObjectByType<LineDrawing>();
                if (lineDrawing)
                {
                    lineDrawing.Stylus = MxInkStylus;
                }
            }
            */
        }
    }

    public void ShowRens(bool b) {
        for (int i=0; i<rens.Length; i++) {
            rens[i].enabled = b;
        }
    }

}
