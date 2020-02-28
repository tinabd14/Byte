using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    private GameObject levelManager;
    private GameObject playerCube;
    private bool won;
    private bool died;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelController");
        playerCube = GameObject.FindGameObjectWithTag("Player");
        won = false;
        died = false;
    }

   
    public void CheckIfWon()
    {
        if (GridGenerator.cubes.Count == 1 && GridGenerator.cubes[0].tag.Equals("Special Cube") && playerCube.GetComponent<Rigidbody>().useGravity == false && isOnTopOfSpecialCube())
        {
            won = true;
            died = false;
            return;
        }
        else if (Physics.OverlapBox(playerCube.transform.position, playerCube.transform.localScale / 2, Quaternion.identity).Length == 1)
        {
            playerCube.GetComponent<BoxCollider>().enabled = false;
            playerCube.GetComponent<Rigidbody>().useGravity = true;
            won = false;
            died = true;
            StartCoroutine(levelManager.GetComponent<LevelHandler>().Restart(SceneManager.GetActiveScene().name));
        }
    }

    private bool isOnTopOfSpecialCube()
    {
        float offset = 0.25f;

        if(Math.Abs(playerCube.transform.position.x - GridGenerator.cubes[0].transform.position.x) < offset)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void KillCubeIfPossible(Collider[] colliders)
    {
        GameObject cubeToKill;
        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                cubeToKill = collider.gameObject;
                if (cubeToKill != playerCube)
                {
                    cubeToKill.GetComponent<Rigidbody>().useGravity = true;
                    GridGenerator.cubes.Remove(cubeToKill);
                    StartCoroutine(RemoveCubeFromScene(cubeToKill));
                    return;
                }
            }
        }
    }

    private IEnumerator RemoveCubeFromScene(GameObject cubeToKill)
    {
        if(!cubeToKill.tag.Equals("Special Cube"))
        {
            yield return new WaitForSeconds(2f);
            Destroy(cubeToKill);
            yield return null;
        }
    }


    public bool GetWon()
    {
        return won;
    }

    public bool GetDied()
    {
        return died;
    }

    public void SetWon(bool won2)
    {
        won = won2;
    }

    public void SetDied(bool died2)
    {
        died = died2;
    }
}
