public class ShowHideSomething : MotionDoing
{
    public override void Doing()
    {
        foreach(var element in _elementsFromSensor)
        {
            element.gameObject.SetActive(!element.gameObject.activeSelf);
        }
    }
}