import { Route$MatchedEvent } from "sap/ui/core/routing/Route";
import VehicleDetailsViewModel from "../models/vehicleDetailsView/VehicleDetailsViewModel";
import BaseController from "./BaseController";
import VehicleService from "../services/VehicleService";
import Component from "../Component";
import Context from "sap/ui/model/odata/v4/Context";
import MessageHelper from "computec/appengine/common/helpers/MessageHelper";
import ODataHelper from "computec/appengine/uicore/helpers/ODataHelper";
import VehicleMasterData from "../models/VehicleMasterData";
import ErrorHelper from "computec/appengine/uicore/helpers/ErrorHelper";
import MessageBox from "sap/m/MessageBox";
import ODataModel from "sap/ui/model/odata/v4/ODataModel";
/**
 * @namespace ct.vehone.controller
 */
export default class VehicleDetails extends BaseController<VehicleDetailsViewModel> {
	private VEHICLE_UPDATE_GROUP_ID = "vehicleUpdateGroup";
	onInit(): void {
		super.onInit();
		this.getRouter().getRoute("vehicleDetails").attachMatched(this.onRouteMatched.bind(this), this);
	}

	onRouteMatched(oEvent: Route$MatchedEvent): void {
		const oArgs = oEvent.getParameter("arguments") as { vehicleCode: string };
		const vehicleCode = oArgs.vehicleCode;
		this._bindVehicle(vehicleCode);
	}

	//#region VIEW
	protected override createViewModel() {
		return new VehicleDetailsViewModel();
	}
	private _bindVehicle(vehicleCode: string) {
		this.getView().bindElement({
			path: VehicleService.getVehicleMasterDataODataPath(vehicleCode),
			parameters: { $expand: "Owners", $$updateGroupId: this.VEHICLE_UPDATE_GROUP_ID },
		});
	}
	private _getViewBindingContext = () =>
		this.getView().getBindingContext(Component.ODATA_MODEL_NAME) as Context;
	private _getCurrentVehicle = () =>
		ODataHelper.getObject<VehicleMasterData>(this.getView(), Component.ODATA_MODEL_NAME);
	private _getViewVehicleModel = () =>
		this.getView().getModel(Component.ODATA_MODEL_NAME) as ODataModel;
	private _refresh = () => this._getViewVehicleModel().refresh();
	//#endregion VIEW

	//#region EVENT HANDLERS
	async onDeletePressed() {
		const deleteConfirmation = await MessageHelper.simpleConfirm(
			this.translate("vehicleDeleteConfirmation", [this._getCurrentVehicle().Code]),
			this.translate("delete")
		);

		if (!deleteConfirmation) return;
		try {
			await this._deleteVehicle();
			this.navTo(
				"home",
				{
					refresh: true,
				},
				false
			);
		} catch (error) {
			const err = ErrorHelper.getError(error);
			MessageBox.error(this.translate("vehicleDeleteFailure", [err]));
		}
	}
	onEditPressed = () => (this.getViewModel().editMode = true);

	async onCancelPressed() {
		if (this._getViewBindingContext().hasPendingChanges()) {
			const cancelConfirmation = await MessageHelper.simpleConfirm(
				this.translate("vehicleCancelConfirmation"),
				this.translate("cancel")
			);
			if (!cancelConfirmation) return;
			this.getViewModel().busy = true;
			await this._getViewBindingContext().resetChanges();
		}
		this.getViewModel().editMode = false;
		this.getViewModel().busy = false;
	}
	async onSavePressed() {
		this.getViewModel().busy = true;
		try {
			if (this._getViewBindingContext().hasPendingChanges()) {
				await this._saveVehicle();
				if (this._getViewBindingContext().hasPendingChanges())
					throw new Error(this.translate("vehicleSaveFailure", ["Pending changes remains"]));
			}
			this.getViewModel().editMode = false;
			this._refresh();
		} catch (error) {
			const err = ErrorHelper.getError(error);
			MessageBox.error(this.translate("vehicleSaveFailure", [err]));
		} finally {
			this.getViewModel().busy = false;
		}
	}
	//#endregion EVENT HANDLERS

	//#region PRIVATE METHODS
	private _deleteVehicle = () => this._getViewBindingContext().delete("$auto");

	private _saveVehicle = () =>
		this._getViewVehicleModel().submitBatch(this.VEHICLE_UPDATE_GROUP_ID);

	//#endregion PRIVATE METHODS
}
