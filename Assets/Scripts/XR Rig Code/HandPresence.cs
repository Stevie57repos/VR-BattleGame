using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handmodelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnHandModel;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize(); 
    }
    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        //Filtering device list for objects that match the right controller variables
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        // function to list the devices in our input device list       
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        // setting target device to the first input device
        if (devices.Count > 0)
        {

            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find corresponding controller");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnHandModel = Instantiate(handmodelPrefab, transform);

            handAnimator = spawnHandModel.GetComponent<Animator>();
        }
    }
    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (showController)
            {
                spawnHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
