import VehiclesViewModel from "../models/vehiclesView/VehiclesViewModel";
import FilterViewModel from "computec/appengine/common/models/FilterViewModel";
import VehiclesFilterViewModel from "../models/vehiclesView/VehiclesFilterViewModel";
import Formatter from "../helpers/Formatter";
import { IconTabBar$SelectEvent } from "sap/m/IconTabBar";
import { VehicleTypeEnum } from "../models/enums/VehicleTypeEnum";
import Table from "sap/m/Table";
import ODataListBinding from "sap/ui/model/odata/v4/ODataListBinding";
import VehicleDialog from "../controls/VehicleDialog/VehicleDialog";
import BaseController from "./BaseController";
import MessageBox from "sap/m/MessageBox";
import ErrorHelper from "computec/appengine/uicore/helpers/ErrorHelper";

export default class Vehicles extends BaseController<VehiclesViewModel, FilterViewModel> {
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
	private _refreshVehiclesTable = () => this._getVehiclesTableBidning().refresh();
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
	onRefresh() {
		this._refreshVehiclesTable();
	}
	async onAddButtonPress() {
		try {
			await this._addVehicle();
		} catch (error) {
			MessageBox.error(this.translate("vehiclesAddingFailure",[ErrorHelper.getError(error)]))
		}
	}
	// #endregion

	private async _addVehicle() {
		const dialog = new VehicleDialog();
		this.getView().addDependent(dialog);
		const result = await dialog.open();
		if (!result) return
		const service = await this.getVehicleService();
		await service.AddVehicle(result);
		this._refreshVehiclesTable();
	}

	private _applyFilter() {
		const filter = this.getFilterModel().getFilter();
		this._getVehiclesTableBidning().filter(filter);
	}
}
