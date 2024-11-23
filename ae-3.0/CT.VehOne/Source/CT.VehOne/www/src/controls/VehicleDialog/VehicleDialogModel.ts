import BaseViewModel from "computec/appengine/uicore/model/BaseViewModel";
import { VehicleTypeEnum } from "ct/vehone/models/enums/VehicleTypeEnum";
import VehicleMasterData from "ct/vehone/models/VehicleMasterData";
export default class VehicleDialogModel extends BaseViewModel {
	constructor() {
		super();
		this._initEnums();
		this.Vehicle = new VehicleMasterData();
		this.Vehicle.U_Type = VehicleTypeEnum.Car;
		this.Vehicle.U_ManufacturingDate = null;
		this.Vehicle.U_RegistrationDate = null;
	}
	
	public set Vehicle(value: VehicleMasterData) {
		this.setValue("/Vehicle", value);
	}
	public get Vehicle() {
		return this.getValue<VehicleMasterData>("/Vehicle");
	}

	public get CarTypesEnum() {
		return this.getValue<string[]>("/CarTypesEnum");
	}
	public set CarTypesEnum(value: string[]) {
		this.setValue("/CarTypesEnum", value);
	}

	private _initEnums() {
		this.CarTypesEnum = Object.keys(VehicleTypeEnum).map(key => key);
	}
}
