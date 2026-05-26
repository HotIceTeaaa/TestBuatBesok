
using UnityEngine;

public class Apples : MonoBehaviour
{
    private float timer;
    private bool isDestroyed;
    public enum Type
    {
        Dash,
        Jump
    }

    public Type type = Type.Jump;

    private Vector3 initPos;
    private Vector3 exitPos;

    void Start()
    {
        resetTimer();
        initPos = transform.position;
        exitPos = new Vector3(initPos.x, initPos.y - 200, initPos.z);
    }

    void Update()
    {
        decrementTimer();

        if(timer < 0f)
        {
            activateApple();
            resetTimer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerController playerControllerscript = other.gameObject.GetComponent<playerController>();

            switch (type)
            {
                case Type.Jump:
                    playerControllerscript.canJump = true;
                    break;
                case Type.Dash:
                    playerControllerscript.canDash = true;
                    break;
            }

            deactivateApple();
        }
    }

    private void resetTimer()
    {
        timer = 2.0f;
    }

    private void decrementTimer()
    {
        if (isDestroyed)
        {
            timer -= Time.deltaTime;
        }
    }

    private void activateApple()
    {
        isDestroyed = false;
        transform.position = initPos;
    }
    private void deactivateApple()
    {
        isDestroyed = true;
        transform.position = exitPos;
    }
}
