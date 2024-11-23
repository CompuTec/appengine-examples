import HttpClient from "computec/appengine/uicore/communication/HttpClient";
import VehicleMasterData from "../models/VehicleMasterData";
import BaseService from "computec/appengine/uicore/services/BaseService";
import Component from "../Component";

export default class VehicleService extends BaseService {
	static VEHICLE_MASTER_DATA = "VehicleMasterData" as const;
	static VEHICLE_ODATA_ENDPOINT = "odata/CTVehOne/VehicleMasterData" as const;

	public static getVehicleMasterDataODataPath = (code?: string) =>
		`${Component.ODATA_MODEL_NAME}>/${VehicleService.VEHICLE_MASTER_DATA}${
			code ? `('${code}')` : ""
		}`;

	public async AddVehicle(vehicle: VehicleMasterData) {
		const url = `${VehicleService.VEHICLE_ODATA_ENDPOINT}`;
		return await HttpClient.postJSON<VehicleMasterData>(vehicle.toJSON(), url, {
			failHandling: true,
			handleBusy: true,
		});
	}
}
