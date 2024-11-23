import DateHelper from "computec/appengine/common/helpers/DateHelper";
import { VehicleTypeEnum } from "./enums/VehicleTypeEnum";
import { VehiclePreviousOwners } from "./VehiclePreviousOwners";
import BaseBusinessObject from "computec/appengine/common/models/BaseBusinessObject";

export default class VehicleMasterData extends BaseBusinessObject {
	Code: string;
	U_Type: VehicleTypeEnum;
	U_Model: string;
	U_Color: string;
	U_EnginePower: number;
	U_EngineCapacity: number;
	U_ManufacturingDate: Date;
	U_RegistrationDate: Date;
	U_VIN: string;
	U_RegistrationNumber: string;
	Owners: VehiclePreviousOwners;
	U_BuyBy: string;
	U_InvNr: number;


	toJSON(): Record<string, unknown> {
		return {
			...super.toJSON(),
			U_ManufacturingDate: DateHelper.toISOString(this.U_ManufacturingDate),
			U_RegistrationDate: DateHelper.toISOString(this.U_RegistrationDate),
		};
	}
}
