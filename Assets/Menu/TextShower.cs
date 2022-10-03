using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TextShower : MonoBehaviour
{
    [SerializeField] float timeToShowSimbol = 0.1f;
    [TextArea(minLines: 4, maxLines: 8)]
    [SerializeField] string text = "";
    [SerializeField] string sceneNameToLoad = "";
    Text textComponent;
    AudioSource audioSource;
    //[SerializeField] Image progressBar;
    [SerializeField]
    GameObject[] objectsToDisable;

    AsyncOperation sceneLoading;
    public void GameStart()
    {
        foreach (GameObject obj in objectsToDisable)
        {
            obj.SetActive(false);
        }
        textComponent = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(TextShow());
        sceneLoading = SceneManager.LoadSceneAsync(sceneNameToLoad);
        sceneLoading.allowSceneActivation = false;
    }

    IEnumerator TextShow()
    {
        for (int i = 0; i < text.Length; i++)
        {
            yield return new WaitForSeconds(timeToShowSimbol + UnityEngine.Random.Range(0f, 2f * timeToShowSimbol));
            textComponent.text = textComponent.text + text[i];
            audioSource.Play();
        }

    }

    void Update()
    {
        if (Input.anyKey || Input.touchCount > 0)
            if (sceneLoading != null)
            {
                sceneLoading.allowSceneActivation = true;
            }


    }
}
