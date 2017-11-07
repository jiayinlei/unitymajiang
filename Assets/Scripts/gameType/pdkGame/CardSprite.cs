using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// 用于卡片的显示
/// </summary>
public class CardSprite : MonoBehaviour
{
    private Card card;
    //public Image sprite;
    private bool isSelected;

    void Start()
    {
    }

    /// <summary>
    /// sprite所装载的card
    /// </summary>
    public Card Poker
    {
        set
        {
            card = value;
            SetSprite();
        }
        get { return card; }
    }

    /// <summary>
    /// 是否被点击中
    /// </summary>
    public bool Select
    {
        set { isSelected = value; }
        get { return isSelected; }
    }

    /// <summary>
    /// 设置UISprite的显示
    /// </summary>
    void SetSprite()
    {
        if (card.Attribution == CharacterType.Player || card.Attribution == CharacterType.Desk)
        {
            //sprite.name  = card.GetCardName;
            this.gameObject.GetComponent<Image>().sprite = Resources.Load < Sprite > (string.Format("pdkSprite/Cards/{0}", this .card.GetCardIndex));
            this.gameObject.GetComponent<Button >().onClick.AddListener(delegate () {
                OnClick();
            });
        }
        else
        {
            //sprite.name  = "SmallCardBack1";
        }
    }

    /// <summary>
    /// 销毁精灵
    /// </summary>
    public void Destroy()
    {
        //销毁对象
        //Destroy(this.gameObject);
    }

    /// <summary>
    /// 调整位置
    /// </summary>
    public void GoToPosition(GameObject parent, int index)
    {
        
        if (card.Attribution == CharacterType.Player)
        {
            transform.localPosition =
              // parent.transform.FindChild("CardsStartPoint").localPosition + Vector3.right * 25 * index;
              parent.transform.localPosition + Vector3.right * 25 * index;
            if (isSelected)
            {
                //transform.localPosition += Vector3.up * 10;
            }
        }
        else if (card.Attribution == CharacterType.PlayerOne ||
            card.Attribution == CharacterType.PlayerTwo )
        {
            transform.localPosition =
                parent.transform.FindChild("CardsStartPoint").localPosition + Vector3.up * -25 * index;
        }
        else if (card.Attribution == CharacterType.Desk)
        {
            //transform.localPosition =
            //    parent.transform.FindChild("PlacePoint").localPosition + Vector3.right * 25 * index;
            transform.SetParent(parent.transform ,false);
            transform.localPosition =
  // parent.transform.FindChild("CardsStartPoint").localPosition + Vector3.right * 25 * index;
  parent.transform.localPosition + Vector3.right * 25 * index;
        }

    }

    /// <summary>
    /// 卡牌点击
    /// </summary>
    public void OnClick()
    {
        if (card.Attribution == CharacterType.Player)
        {
            if (isSelected)
            {
                transform.localPosition -= Vector3.up * 30;
                isSelected = false;
            }
            else
            {
                transform.localPosition += Vector3.up * 30;
                isSelected = true;
                //card.IsSelect = true;
            }
        }
    }
}
