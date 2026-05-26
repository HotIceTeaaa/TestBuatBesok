using UnityEngine;
using UnityEngine.SceneManagement;

public class cutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private inputReader inputReader;
    [SerializeField] private UI ui;
    [SerializeField] private Animator animator;

    private void Start()
    {
        //disable bbrp script jadi user gbs control player
        inputReader.enabled = false;
        ui.enabled = false;

        //beginningCutscene otomatis mulai setelah start()
    }

    //dipanggil animation setelah beginningCutscene selesai
    public void renableScripts()
    {
        inputReader.enabled = true;
        ui.enabled = true;
    }

    public void triggerEndingCutscene()
    {
        //disable inputReader jadi user gbs control player
        inputReader.enabled = false;
        ui.enabled = false;
        animator.SetTrigger("endingTrigger");
    }
}