namespace CT.VehOne.BL.Services;

public interface IWorkingWithSapService
{
    bool CreateItemMasterData(string itemCode, string itemName,bool batchManagement);
    CompuTec.BaseLayer.DI.Items? GetItem(string itemCode);
}