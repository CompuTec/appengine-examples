/* eslint-disable no-unused-vars */
import { $ControlSettings } from "sap/ui/core/Control";
import {
	VehicleDialog$CancelEvent,
	VehicleDialog$ConfirmEvent,
	VehicleDialog$ConfirmParameters,
} from "./VehicleDialog.events";
import VehicleMasterData from "ct/vehone/models/VehicleMasterData";

declare module "./VehicleDialog" {
	interface $VehicleDialogSettings extends $ControlSettings {
		vehicle?: VehicleMasterData;
		confirm?: (event: VehicleDialog$ConfirmEvent) => void;
		cancel?: (event: VehicleDialog$ConfirmEvent) => void;
	}

	export default interface IVehicleDialog {
		getVehicle(): VehicleMasterData?;
		setVehicle(value: VehicleMasterData?): this;

		// event: confirm
		attachConfirm(fn: (event: VehicleDialog$ConfirmEvent) => void, listener?: object): this;
		attachConfirm<CustomDataType extends object>(data: CustomDataType, fn: (event: VehicleDialog$ConfirmEvent, data: CustomDataType) => void, listener?: object): this;
		detachConfirm(fn: (event: VehicleDialog$ConfirmEvent) => void, listener?: object): this;
		fireConfirm(parameters?: VehicleDialog$ConfirmParameters): this;

		// event: cancel
		attachCancel(fn: (event: VehicleDialog$CancelEvent) => void, listener?: object): this;
		attachCancel<CustomDataType extends object>(data: CustomDataType, fn: (event: VehicleDialog$CancelEvent, data: CustomDataType) => void, listener?: object): this;
		detachCancel(fn: (event: VehicleDialog$CancelEvent) => void, listener?: object): this;
		fireCancel(parameters?: void): this;
	}
}
