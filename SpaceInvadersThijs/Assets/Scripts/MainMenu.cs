using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // public variables
    public GameObject menuPanel;
    public GameObject CreditsPanel;
    public AudioClip clickSound;

    // private variables
    private enum Buttons {START, COOP, CREDITS, RETURN, EXIT}
    private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = gameObject.AddComponent<AudioSource>();
        sound.clip = clickSound;
    }

    // OnClick StartGame Button
    public void StartGame()
    {
        StartCoroutine(ButtonLogic(Buttons.START));
    }

    // OnClick StartGame Button
    public void StartGameCOOP()
    {
        StartCoroutine(ButtonLogic(Buttons.COOP));
    }

    // OnClick Credits Button
    public void Credits()
    {
        StartCoroutine(ButtonLogic(Buttons.CREDITS));
    }

    // OnClick Return Button
    public void ReturnToMenu()
    {
        StartCoroutine(ButtonLogic(Buttons.RETURN));
    }

    // OnClick Exit Button
    public void ExitGame()
    {
        StartCoroutine(ButtonLogic(Buttons.EXIT));
    }

    // the logic is executed after the click sound has been played
    IEnumerator ButtonLogic(Buttons pPressedButton)
    {
        sound.Play();
        yield return new WaitForSeconds(sound.clip.length);
        switch (pPressedButton)
        {
            case Buttons.START:
                {
                    SceneManager.LoadScene(1);
                    break;
                }
            case Buttons.COOP:
                {
                    SceneManager.LoadScene(2);
                    break;
                }
            case Buttons.CREDITS:
                {
                    CreditsPanel.SetActive(true);
                    menuPanel.SetActive(false);
                    break;
                }
            case Buttons.RETURN:
                {
                    menuPanel.SetActive(true);
                    CreditsPanel.SetActive(false);
                    break;
                }
            case Buttons.EXIT:
                {
                    Application.Quit();
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
