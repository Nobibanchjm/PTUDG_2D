using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gamesession : MonoBehaviour
{
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    public static bool islive = true;


    void Awake()
    {
        // 1. Kiểm tra xem có bao nhiêu phiên bản GameSession đang tồn tại trong cảnh (scene).
        int numGameSession = FindObjectsOfType<Gamesession>().Length;
        // 2. Nếu đã có hơn 1 GameSession (khi quay lại một scene khác hoặc reload scene), thì phá hủy đối tượng hiện tại.
        if (numGameSession > 1)
        {
            Destroy(gameObject);// Hủy đối tượng hiện tại vì đã có GameSession khác tồn tại.
        }
        else
        {
            // 3. Nếu không có GameSession khác, đối tượng này sẽ được giữ lại khi tải màn chơi mới.
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        scoreText.text = score.ToString();
    }

    void Update()
    {
        if(!islive)
        {
            score = 0;
            scoreText.text = score.ToString();
        }    
    }

    public void addScore(int addscore)
    {
        if(islive)
        {
            score += addscore;
            scoreText.text = score.ToString();
        }
        else 
        {
            islive = true;
            score = 100;
            scoreText.text = score.ToString();
        }
    }

}
