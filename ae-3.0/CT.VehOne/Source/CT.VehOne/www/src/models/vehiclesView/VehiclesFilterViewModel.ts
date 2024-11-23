import FilterProperty from "computec/appengine/common/models/FilterProperty";
import FilterViewModel from "computec/appengine/common/models/FilterViewModel";
import FilterOperator from "sap/ui/model/FilterOperator";
import { VehicleTypeDBEnum } from "../enums/VehicleTypeDBEnum";

export default class VehiclesFilterViewModel extends FilterViewModel {
	getFilterProperties(): (string | FilterProperty)[] {
		return [
			new FilterProperty("VehicleType", "U_Type", FilterOperator.EQ),
			new FilterProperty("U_Model", FilterOperator.Contains),
			new FilterProperty("U_Color", FilterOperator.Contains),
			new FilterProperty("U_EnginePower", FilterOperator.EQ),
			new FilterProperty("U_EngineCapacity", FilterOperator.EQ),
			new FilterProperty("ManufacturingDateFrom", "U_ManufacturingDate", FilterOperator.GE),
			new FilterProperty("ManufacturingDateTo", "U_ManufacturingDate", FilterOperator.LE),
			new FilterProperty("RegistrationDateFrom", "U_RegistrationDate", FilterOperator.GE),
			new FilterProperty("RegistrationDateTo", "U_RegistrationDate", FilterOperator.LE),
			new FilterProperty("U_VIN", FilterOperator.Contains),
			new FilterProperty("U_RegistrationNumber", FilterOperator.Contains),
			new FilterProperty("U_BuyBy", FilterOperator.Contains),
			new FilterProperty("U_InvNr", FilterOperator.Contains),
		];
	}

	constructor() {
		super();
		this.resetFilter();
	}

	resetFilter() {
		this.U_Model = "";
		this.U_Color = "";
		this.U_EnginePower = null;
		this.U_EngineCapacity = null;
		this.ManufacturingDateFrom = null;
		this.ManufacturingDateTo = null;
		this.RegistrationDateFrom = null;
		this.RegistrationDateTo = null;
		this.U_VIN = "";
		this.U_RegistrationNumber = "";
		this.U_BuyBy = "";
		this.U_InvNr = "";
	}

	get U_Model() {
		return this.getValue<string>("/U_Model");
	}
	set U_Model(value: string) {
		this.setValue("/U_Model", value);
	}

	public get U_Color() {
		return this.getValue<string>("/U_Color");
	}
	public set U_Color(value: string) {
		this.setValue("/U_Color", value);
	}

	get VehicleType() {
		return this.getValue<VehicleTypeDBEnum>("/VehicleType");
	}
	set VehicleType(value: VehicleTypeDBEnum) {
		this.setValue("/VehicleType", value);
	}

	get U_EnginePower() {
		return this.getValue<number | null>("/U_EnginePower");
	}
	set U_EnginePower(value: number | null) {
		this.setValue("/U_EnginePower", value);
	}

	get U_EngineCapacity() {
		return this.getValue<number | null>("/U_EngineCapacity");
	}
	set U_EngineCapacity(value: number | null) {
		this.setValue("/U_EngineCapacity", value);
	}

	get U_VIN() {
		return this.getValue<string>("/U_VIN");
	}
	set U_VIN(value: string) {
		this.setValue("/U_VIN", value);
	}

	get U_RegistrationNumber() {
		return this.getValue<string>("/U_RegistrationNumber");
	}
	set U_RegistrationNumber(value: string) {
		this.setValue("/U_RegistrationNumber", value);
	}

	get U_BuyBy() {
		return this.getValue<string>("/U_BuyBy");
	}
	set U_BuyBy(value: string) {
		this.setValue("/U_BuyBy", value);
	}

	get U_InvNr() {
		return this.getValue<string>("/U_InvNr");
	}
	set U_InvNr(value: string) {
		this.setValue("/U_InvNr", value);
	}

	public get RegistrationDateFrom() {
		return this.getValue<Date | null>("/RegistrationDateFrom");
	}
	public set RegistrationDateFrom(value: Date | null) {
		this.setValue("/RegistrationDateFrom", value);
	}
	public get RegistrationDateTo() {
		return this.getValue<Date | null>("/RegistrationDateTo");
	}
	public set RegistrationDateTo(value: Date | null) {
		this.setValue("/RegistrationDateTo", value);
	}
	public get ManufacturingDateFrom() {
		return this.getValue<Date | null>("/ManufacturingDateFrom");
	}
	public set ManufacturingDateFrom(value: Date | null) {
		this.setValue("/ManufacturingDateFrom", value);
	}
	public get ManufacturingDateTo() {
		return this.getValue<Date | null>("/ManufacturingDateTo");
	}
	public set ManufacturingDateTo(value: Date | null) {
		this.setValue("/ManufacturingDateTo", value);
	}
}