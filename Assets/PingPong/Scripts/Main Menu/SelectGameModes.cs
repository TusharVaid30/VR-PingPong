using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

public class SelectGameModes : MonoBehaviour
{
    [SerializeField] private InputDeviceCharacteristics controllerCharacteristics;
    [SerializeField] private float oldYPosition;
    [SerializeField] private float newYPosition;
    [SerializeField] private BoxCollider[] gameModeButtons;
    [SerializeField] private Image[] gameModesIcons;
    [SerializeField] private GameObject[] buttons;
    [SerializeField] private Animator image;

    private InputDevice targetDevice;

    public void HoverOverMode(Outline button)
    {
        button.effectColor = Color.gray;
    }

    public void HoverExitMode(Outline button)
    {
        button.effectColor = Color.white;
    }
    
    public void OpenGameModesMenu()
    {
        LeanTween.moveLocalY(gameObject, newYPosition, 20f * Time.deltaTime).setEaseInQuad();
        for (int i = 0; i < 2; i++)
        {
            gameModeButtons[i].enabled = true;
            gameModesIcons[i].GetComponent<Animator>().Play("Fade In Icons", -1, 0f);
            buttons[i].GetComponent<BoxCollider>().enabled = false;
            buttons[i + 1].GetComponent<BoxCollider>().enabled = false;
            buttons[i].GetComponent<Animator>().Play("Fade Text", -1, 0f);
            buttons[i + 1].GetComponent<Animator>().Play("Fade Text", -1, 0f);
        }
    }

    public void CloseGameModesMenu()
    {
        LeanTween.moveLocalY(gameObject, oldYPosition, 20f * Time.deltaTime).setEaseInQuad();
        for (int i = 0; i < 2; i++)
        {
            gameModeButtons[i].enabled = false;
            gameModesIcons[i].GetComponent<Animator>().Play("Fade Icons", -1, 0f);
            buttons[i].GetComponent<BoxCollider>().enabled = true;
            buttons[i + 1].GetComponent<BoxCollider>().enabled = true;
            buttons[i].GetComponent<Animator>().Play("Fade In Text", -1, 0f);
            buttons[i + 1].GetComponent<Animator>().Play("Fade In Text", -1, 0f);
        }
    }
    
    private void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        
        if (devices.Count > 0)
            targetDevice = devices[0];
    }

    public void ChangeScene(string name)
    {
        image.Play("Fade In Image", -1, 0f);
        StartCoroutine(OpenScene("Mode " + name));
    }

    private IEnumerator OpenScene(string name)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Calibration");
        PlayerPrefs.SetString("Scene", name);
    }
}
