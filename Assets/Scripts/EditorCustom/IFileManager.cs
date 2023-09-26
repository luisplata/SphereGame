using System.Collections.Generic;

internal interface IFileManager : IRule
{
    string CreateJson(List<DragComponent> listOfDrags);
    void SaveMap(string result);
}