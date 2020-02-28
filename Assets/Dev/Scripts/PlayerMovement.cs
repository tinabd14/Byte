using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private float space;
    private float speed;

    private bool moving;
    static GameObject cubeToKill;

    private GameObject gameManager;
    private GameObject levelManager;

    private AudioSource flipSound;

    private void Start()
    {
        moving = false;
        space = 0.1f;
        speed = 10f;
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        levelManager = GameObject.FindGameObjectWithTag("LevelController");

        flipSound = gameObject.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!moving && !gameManager.GetComponent<Play>().GetWon())
        {
            ResponseToTheInput();
        }
    }


    void ResponseToTheInput()
    {
        Vector3 position, pivot, direction;
        CalculateFlipParameters(out position, out pivot, out direction);

        if (position != transform.position)
        {
            moving = true;
            transform.position = position;
            StartCoroutine(FlipCube(gameObject.transform.position, gameObject.transform.position + pivot, direction));
        }
    }

    private void CalculateFlipParameters(out Vector3 position, out Vector3 pivot, out Vector3 direction)
    {
        position = transform.position;
        pivot = transform.position;
        direction = new Vector3();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + space);
            pivot = new Vector3(0f, -0.5f, 0.5f);
            direction = Vector3.right;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - space);
            pivot = new Vector3(0f, -0.5f, -0.5f);
            direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            position = new Vector3(gameObject.transform.position.x - space, gameObject.transform.position.y, gameObject.transform.position.z);
            pivot = new Vector3(-0.5f, -0.5f, 0f);
            direction = Vector3.forward;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            position = new Vector3(gameObject.transform.position.x + space, gameObject.transform.position.y, gameObject.transform.position.z);
            pivot = new Vector3(0.5f, -0.5f, 0f);
            direction = Vector3.back;
        }
    }

    public IEnumerator FlipCube(Vector3 currentPos, Vector3 pivot, Vector3 direction)
    {
        for(int i = 0; i < 90/speed; i++)
        {
            flipSound.Play(0);
            transform.RotateAround(pivot, direction, speed);
            OnPass(currentPos);
            yield return null;
        }
        gameManager.GetComponent<Play>().CheckIfWon();
        moving = false;
    }

 
    private void OnPass(Vector3 currentPosition)
    {
        Collider[] colliders = Physics.OverlapBox(currentPosition, transform.localScale / 2, Quaternion.identity);
        gameManager.GetComponent<Play>().KillCubeIfPossible(colliders);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Special Cube"))
        {
            gameManager.GetComponent<Play>().CheckIfWon();

            if (gameManager.GetComponent<Play>().GetWon())
            {
                float t = 0f;
                while (t < 2)
                {
                    t += Time.deltaTime;
                    StartCoroutine(RotateAround());
                }
                StartCoroutine(levelManager.GetComponent<LevelHandler>().LoadNextLevel());
            }
            else if(GridGenerator.cubes.Count > 1)
            {
                StartCoroutine(levelManager.GetComponent<LevelHandler>().Restart(SceneManager.GetActiveScene().name));
            }
        }
    }

    public IEnumerator RotateAround()
    {
        float t = 0f;
        gameObject.transform.rotation = new Quaternion();
;       while (t < speed)
        {
            gameObject.transform.Rotate(Vector3.down, Time.deltaTime);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
