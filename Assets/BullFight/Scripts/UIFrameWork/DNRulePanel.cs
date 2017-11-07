using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DNRulePanel : BasePanel {
    private void Start() {
        transform.FindChild("DoTween/CloseBtn").GetComponent<Button>().onClick.AddListener(()=> {
            DNUIManager.Instance.PopPanel();

        });
    }
}
