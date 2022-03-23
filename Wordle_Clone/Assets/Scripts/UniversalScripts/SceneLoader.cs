using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] Animator cross_fade;

    private void OnEnable()
    {
        GameEvents.onReadytoLoadScene += SceneLoad;
    }

    public void SceneLoad(int sceneIndex)
    {
        StartCoroutine(PlayTransition(sceneIndex));
    }

    IEnumerator PlayTransition(int sceneIndex)
    {
        cross_fade.SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneIndex);
    }

    private void OnDisable()
    {
        GameEvents.onReadytoLoadScene -= SceneLoad;
    }
}
