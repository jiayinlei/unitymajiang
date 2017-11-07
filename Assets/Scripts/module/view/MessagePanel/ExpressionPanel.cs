using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ExpressionPanel : MonoBehaviour
{
    public static Sprite[] expressionSprite;
    private GameObject expresssionParent;
    public GameObject expressionPrefab;
    
    // Use this for initialization
    void Start()
    {
        expresssionParent = GameObject.Find("expressionContent");
        expressionSprite = Resources.LoadAll<Sprite>("UIPicture/Emoji");
        this.CreatExpression();
    }
    public void CreatExpression()
    {
      //  TooL.destroyAllChildren(this.expresssionParent);
        for (int i = 0; i < expressionSprite.Length; i++)
        {
            GameObject obj = TooL.clone(expressionPrefab, expresssionParent);
            Expression script = obj.GetComponent<Expression>();
            if (script)
            {
                script.InitView(expressionSprite[i],i);
            }
        }
    }
}
