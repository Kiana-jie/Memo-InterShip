using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;  // ����UI���

public class MainMenuManager : MonoBehaviour
{
    

    public void StartGame()
    {
        SceneManager.LoadScene("Chapter-1");
    }
    // Start is called before the first frame update
    public void ExitGame()
    {
        // ����ڱ༭���У��˳�����ģʽ
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
               // ����ǹ����汾���˳���Ϸ
               Application.Quit();
#endif
    }
    // ��ʾ����˵�����
    
}
