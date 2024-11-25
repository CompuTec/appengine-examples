import BaseViewModel from "@computec/uicore/src/computec/appengine/uicore/model/BaseViewModel";
import BaseExtendedController from "computec/appengine/common/plugin/BaseExtendedController";
import VehicleService from "../services/VehicleService";
export default class BaseController<
	ViewModel extends BaseViewModel = BaseViewModel,
	FilterModel extends BaseViewModel = BaseViewModel
> extends BaseExtendedController<ViewModel, FilterModel> {
	public getVehicleService = () => this.getService<VehicleService>("VehicleService");
}
