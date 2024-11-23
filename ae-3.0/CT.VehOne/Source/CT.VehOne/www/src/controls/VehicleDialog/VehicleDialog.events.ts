import VehicleMasterData from "ct/vehone/models/VehicleMasterData";
import Event from "sap/ui/base/Event";

export interface VehicleDialog$ConfirmParameters {
	vehicle: VehicleMasterData;
}

// eslint-disable-next-line @typescript-eslint/no-empty-interface
export interface VehicleDialog$CancelParameters {}

export type VehicleDialog$ConfirmEvent = Event<VehicleDialog$ConfirmParameters>;
export type VehicleDialog$CancelEvent = Event<VehicleDialog$CancelParameters>;
