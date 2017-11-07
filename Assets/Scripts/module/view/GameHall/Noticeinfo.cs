using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Noticeinfo : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler {

    Vector3 Pos;

    bool IsJinru = false;
   

    void Start() {
        Pos = transform.localPosition;
    }


    void Update() {        
        if(transform.localPosition.x > 0) {
            transform.localPosition = Pos;
        }else if(transform.localPosition.x <-730) {
            transform.localPosition = new Vector3(-730, Pos.y, Pos.z);
        }
        if(IsJinru == false) {
            return;
        }
        if (Input.GetMouseButton(0)) {
            
            float X = Input.GetAxis("Mouse X");
            if (X == 0) {
                return;
            }
            if (X < 0) {
                if (transform.localPosition.x <= 0 && transform.localPosition.x >= -730) {
                    //transform.localPosition -= new Vector3(150, 0, 0);
                    transform.DOKill();
                    transform.DOLocalMoveX(-730, 1);
                    
                }
            } else {
                if (transform.localPosition.x <= 0 && transform.localPosition.x >= -730) {
                    transform.DOKill();
                    transform.DOLocalMoveX(0, 1);
                }

            }

        }

    }

    public void OnPointerEnter(PointerEventData eventData) {
        IsJinru = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        IsJinru = false;
    }
}
