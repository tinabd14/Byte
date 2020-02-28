using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private TMPro.TextMeshProUGUI levelName;
    private List<GameObject> levelButtons;


    private void Start()
    {
        if(scrollViewContent != null)
        {
            ShowLevels(scrollViewContent);
        }
        else if(levelName != null)
        {
            StartCoroutine(DisappearLevelText());
        }
        
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(1f);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int totalSceneCount = SceneManager.sceneCountInBuildSettings;

        if (currentSceneIndex == totalSceneCount - 1)
        {
            SceneManager.LoadScene("Level Chooser");
        }
        else
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        yield return null;
    }

    void ShowLevels(GameObject content)
    {
        int totalLevelCount = SceneManager.sceneCountInBuildSettings +10;
        for (int i = 0; i < totalLevelCount; i++)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab, content.transform);
            levelButton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = (i + 1).ToString();
            levelButton.name = string.Concat("Level ", levelButton.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
        }

    }

    public void StartLevel(string levelName)
    {
        if(levelName == "")
        {
            levelName = EventSystem.current.currentSelectedGameObject.name;
        }
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator Restart(string levelName)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(levelName);
        yield return null;
    }

    public IEnumerator DisappearLevelText()
    {
        while (levelName.color.a != 0)
        {
            levelName.color = new Color(levelName.color.r, levelName.color.g, levelName.color.b, levelName.color.a - Time.deltaTime / 2);
            yield return null;
        }
    }
}
