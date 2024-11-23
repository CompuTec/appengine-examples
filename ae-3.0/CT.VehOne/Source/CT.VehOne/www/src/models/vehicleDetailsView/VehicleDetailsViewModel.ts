import BaseViewModel from "computec/appengine/uicore/model/BaseViewModel";
import { VehicleTypeEnum } from "../enums/VehicleTypeEnum";

export default class VehicleDetailsViewModel extends BaseViewModel {

	constructor() {
		super();
		this._initEnums();
		this.editMode = false;
	}
	get editMode() {
		return this.getValue<boolean>("/editMode");
	}
	set editMode(value: boolean) {
		this.setValue("/editMode", value);
	}
	get busy() {
		return this.getValue<boolean>("/busy");
	}
	set busy(value: boolean) {
		this.setValue("/busy", value);
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