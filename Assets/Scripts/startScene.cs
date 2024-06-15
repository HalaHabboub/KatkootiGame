using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startScene : MonoBehaviour
{
    private Animator animStart;
    public string nextSceneName; // Name of the next scene to load

    // Start is called before the first frame update
    void Start()
    {
        animStart = this.GetComponent<Animator>();
        StartCoroutine(PlayAnimationsAndLoadScene());
    }

    // Coroutine to play animations in a loop
    private IEnumerator PlayAnimationsAndLoadScene()
    {
        int loopCount = 0;

        while (loopCount < 2)
        {
            // Play "start" animation
            animStart.SetBool("start", true);
            yield return new WaitForSeconds(animStart.GetCurrentAnimatorStateInfo(0).length);
            animStart.SetBool("start", false);

            // Play "fear" animation
            animStart.SetBool("fear", true);
            yield return new WaitForSeconds(animStart.GetCurrentAnimatorStateInfo(0).length);
            animStart.SetBool("fear", false);

            // Play "spin" animation
            animStart.SetBool("spin", true);
            yield return new WaitForSeconds(animStart.GetCurrentAnimatorStateInfo(0).length);
            animStart.SetBool("spin", false);

            loopCount++;
        }

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
