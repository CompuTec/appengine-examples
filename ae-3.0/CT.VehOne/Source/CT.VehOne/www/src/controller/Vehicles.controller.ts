import BaseExtendedController from "computec/appengine/common/plugin/BaseExtendedController";
import VehiclesViewModel from "../models/vehiclesView/VehiclesViewModel";
import FilterViewModel from "computec/appengine/common/models/FilterViewModel";
import VehiclesFilterViewModel from "../models/vehiclesView/VehiclesFilterViewModel";
import Formatter from "../helpers/Formatter";
import { IconTabBar$SelectEvent } from "sap/m/IconTabBar";
import { VehicleTypeEnum } from "../models/enums/VehicleTypeEnum";
import Table from "sap/m/Table";
import ODataListBinding from "sap/ui/model/odata/v4/ODataListBinding";

export default class Vehicles extends BaseExtendedController<VehiclesViewModel, FilterViewModel> {
	formatter: Formatter = new Formatter();
	onInit(): void {
		super.onInit();
	}

	// #region VIEW
	protected override createFilterModel() {
		return new VehiclesFilterViewModel();
	}
	protected override createViewModel() {
		return new VehiclesViewModel();
	}
	private _getVehiclesTable = () => this.byId("vehiclesTable") as Table;
	private _getVehiclesTableBidning = () =>
		this._getVehiclesTable().getBinding("items") as ODataListBinding;
	// #endregion

	//#region HANDLERS
	onIconTabBarCarTypeSelection(evt: IconTabBar$SelectEvent) {
		const key = evt.getParameter("key");
		this.getFilterModel().VehicleType = VehicleTypeEnum[key as keyof typeof VehicleTypeEnum];
		this._applyFilter();
	}
	onFilterChange() {
		this._applyFilter();
	}
	// #endregion

	private _applyFilter() {
		const filter = this.getFilterModel().getFilter();
		this._getVehiclesTableBidning().filter(filter);
	}
}
