using UnityEngine;

public class DragComponent : MonoBehaviour
{
    public void ConfigureDragComponent(BaseElementInScene elementInScene)
    {
        //instantiate element and take to son to this eleent
        var element = Instantiate(elementInScene, transform);
        element.transform.localPosition = Vector3.zero;
    }
}