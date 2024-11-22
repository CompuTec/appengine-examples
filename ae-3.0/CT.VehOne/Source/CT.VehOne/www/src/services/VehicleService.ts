import HttpClient from "computec/appengine/uicore/communication/HttpClient";
import VehicleMasterData from "../models/VehicleMasterData";
import BaseService from "computec/appengine/uicore/services/BaseService";

export default class VehicleService extends BaseService {
	static VEHICLE_ODATA_ENDPOINT = "odata/CTVehOne/VehicleMasterData" as const;


	public async AddVehicle(vehicle: VehicleMasterData){
		const url = `${VehicleService.VEHICLE_ODATA_ENDPOINT}`
		return await HttpClient.postJSON<VehicleMasterData>(vehicle.toJSON(), url, {
			failHandling: true,
			handleBusy: true
		});
	}
}