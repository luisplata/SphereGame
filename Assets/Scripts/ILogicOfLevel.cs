public interface ILogicOfLevel
{
    int GetCurrentLayer();
    void ChangeLayer(int layerDestiny);
    void SetCurrentEnd(PointToEnd pointToEnd);
    void ResetGame();
}