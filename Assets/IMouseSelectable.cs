using UnityEngine;

public interface IMouseSelectable
{
    public void IndirectMouseEnter();
    public void IndirectMouseClickedWhileSelected(IMouseSelectable returnInfo);
    public void IndirectMouseExit();
    /// <summary>
    /// 
    /// </summary>
    /// <returns>True ako je selektovan</returns>
    public bool IndirectMouseOver();
    public GameObject GetGameObject();
}
