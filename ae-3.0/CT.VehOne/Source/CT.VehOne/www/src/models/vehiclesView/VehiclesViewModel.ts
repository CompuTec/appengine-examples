import BaseViewModel from "computec/appengine/uicore/model/BaseViewModel";

export default class VehiclesViewModel extends BaseViewModel {

	public get busy() {
		return this.getValue<boolean>("/busy");
	}
	public set busy(value: boolean) {
		this.setValue("/busy", value);
	}
	
	
}