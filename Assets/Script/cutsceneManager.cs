using UnityEngine;

public class cutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private inputReader inputReader;
    [SerializeField] private Animator animator;

    private void Start()
    {
        //disable inputReader jadi user gbs control player
        inputReader.enabled = false;

        //beginningCutscene otomatis mulai setelah start()
    }

    //dipanggil animation setelah beginningCutscene selesai
    public void renableInputReader()
    {
        inputReader.enabled = true;
    }

    public void triggerEndingCutscene()
    {
        //disable inputReader jadi user gbs control player
        inputReader.enabled = false;
        animator.SetTrigger("endingTrigger");
    }
}