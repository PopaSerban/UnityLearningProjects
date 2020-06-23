using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungryWarning : MonoBehaviour
{
    [SerializeField] private GameObject _dialogBox;
    private bool isCoroutineRunning;

    public void ToggleDialogueBox()
    {   
        if(_dialogBox.gameObject.activeSelf)
            return;
        //_dialogBox.gameObject.SetActive(true);
        if(!isCoroutineRunning)
            StartCoroutine(EnableDialogueBoxCoroutine());
    }
    public void ToggleCloseDialogueBox()
    {
        StopCoroutine(EnableDialogueBoxCoroutine());
    }


    private IEnumerator EnableDialogueBoxCoroutine()
    {
        isCoroutineRunning = true;

        yield return new WaitForSeconds(5.5f);
        _dialogBox.gameObject.SetActive(true);
        StartCoroutine(DisableDialogueBoxCoroutine());

    }

    private IEnumerator DisableDialogueBoxCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        _dialogBox.gameObject.SetActive(false);

        isCoroutineRunning = false;
    }

}
