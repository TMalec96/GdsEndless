using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementTable : MonoBehaviour
{
    [SerializeField]
    List<Achievement> achievements = new List<Achievement>();
    private int xPostion = -5;
    // Start is called before the first frame update
    void Start()
    {
       foreach(Achievement achievement in achievements)
        {
            achievement.CheckForCompletion();
            if(achievement.Complete == 1)
            {
                //wyświetla zrobione achiv
                print(achievement.name);
                Instantiate(achievement, new Vector2(0 + xPostion, transform.position.y), Quaternion.identity);
            }
            xPostion += 5;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResetAllAchievementsProgress()
    {
        foreach (Achievement achievement in achievements)
        {
            achievement.ResetAchievementsProgress();
            print(achievement.Complete.ToString());
        }
    }
}
