import { VehicleTypeEnum } from "./enums/VehicleTypeEnum";
import { VehiclePreviousOwners } from "./VehiclePreviousOwners";

export default class VehicleMasterData {
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
}
