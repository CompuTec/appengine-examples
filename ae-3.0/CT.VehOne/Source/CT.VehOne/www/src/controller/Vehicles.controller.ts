import VehiclesViewModel from "../models/vehiclesView/VehiclesViewModel";
import VehiclesFilterViewModel from "../models/vehiclesView/VehiclesFilterViewModel";
import Formatter from "../helpers/Formatter";
import { IconTabBar$SelectEvent } from "sap/m/IconTabBar";
import { VehicleTypeDBEnum } from "../models/enums/VehicleTypeDBEnum";
import Table from "sap/m/Table";
import ODataListBinding from "sap/ui/model/odata/v4/ODataListBinding";
import VehicleDialog from "../controls/VehicleDialog/VehicleDialog";
import BaseController from "./BaseController";
import MessageBox from "sap/m/MessageBox";
import ErrorHelper from "computec/appengine/uicore/helpers/ErrorHelper";
import ODataHelper from "computec/appengine/uicore/helpers/ODataHelper";
import VehicleMasterData from "../models/VehicleMasterData";
import { ListItemBase$PressEvent } from "sap/m/ListItemBase";
import { Route$MatchedEvent } from "sap/ui/core/routing/Route";
export default class Vehicles extends BaseController<VehiclesViewModel, VehiclesFilterViewModel> {
	formatter: Formatter = new Formatter();
	onInit(): void {
		super.onInit();
		this.getRouter().getRoute("home").attachMatched(this.onRouteMatched.bind(this), this);
	}

	onRouteMatched(oEvent: Route$MatchedEvent): void {
		const parameters = oEvent.getParameter("arguments") as { refresh?: boolean };
		if (parameters.refresh) this._refreshVehiclesTable();
	}

	// #region VIEW
	protected override createFilterModel() {
		return new VehiclesFilterViewModel();
	}
	protected override createViewModel() {
		return new VehiclesViewModel();
	}
	private _getVehiclesTable = () => this.byId("vehiclesTable") as Table;
	private _getVehiclesTableBinding = () =>
		this._getVehiclesTable().getBinding("items") as ODataListBinding;
	private _refreshVehiclesTable = () => this._getVehiclesTableBinding()?.refresh();
	// #endregion

	//#region HANDLERS
	onIconTabBarCarTypeSelection(evt: IconTabBar$SelectEvent) {
		const key = evt.getParameter("key");
		this.getFilterModel().VehicleType = VehicleTypeDBEnum[key as keyof typeof VehicleTypeDBEnum];
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
			MessageBox.error(this.translate("vehiclesAddingFailure", [ErrorHelper.getError(error)]));
		}
	}
	onTableFilterClear() {
		this.getFilterModel().resetFilter();
		this._applyFilter();
	}
	onItemPress(oEvent: ListItemBase$PressEvent) {
		const vehicleCode = ODataHelper.getObject<VehicleMasterData>(oEvent.getSource()).Code;
		this.navTo("vehicleDetails", { vehicleCode });
	}
	// #endregion

	private async _addVehicle() {
		const dialog = new VehicleDialog();
		this.getView().addDependent(dialog);
		const result = await dialog.open();
		if (!result) return;
		const service = await this.getVehicleService();
		await service.AddVehicle(result);
		this._refreshVehiclesTable();
	}

	private _applyFilter() {
		const filter = this.getFilterModel().getQueryFilter();
		this._getVehiclesTableBinding().changeParameters({
			$filter: filter,
		});
	}
}
