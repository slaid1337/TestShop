using UnityEngine;

public class SceenButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject _pushBtn;

    public void EnablePushButton()
    {
        _pushBtn.SetActive(true);
    }

    public void DisablePushButton()
    {
        _pushBtn.SetActive(false);
    }
}
