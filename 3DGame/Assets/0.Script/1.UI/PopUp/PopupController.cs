using UnityEngine;

public class PopupController : MonoBehaviour
{
    [SerializeField] ToastPopup toastPopup;
    [SerializeField] ListPopup listpopup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F3))
        {
            toastPopup.OnPopup("빛나는 방어구 A를 습득했습니다.");
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            // 앞에 아이템 이름, 갯수
            listpopup.On("방어구 A");
        }
    }
}
