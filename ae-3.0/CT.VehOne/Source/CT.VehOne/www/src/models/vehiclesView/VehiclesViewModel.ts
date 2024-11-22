import BaseViewModel from "computec/appengine/uicore/model/BaseViewModel";
import { VehicleTypeEnum } from "../enums/VehicleTypeEnum";

export default class VehiclesViewModel extends BaseViewModel {

	constructor() {
		super();
		this._initEnums();
	}

	private _initEnums() {
		const types: string[] = ['all'];
		types.push(...Object.keys(VehicleTypeEnum).map(key => key));
		this.CarTypesEnum = types;
	}
	public get busy() {
		return this.getValue<boolean>("/busy");
	}
	public set busy(value: boolean) {
		this.setValue("/busy", value);
	}

	public get CarTypesEnum() {
		return this.getValue<string[]>("/CarTypesEnum");
	}
	public set CarTypesEnum(value: string[]) {
		this.setValue("/CarTypesEnum", value);
	}
	
	
}