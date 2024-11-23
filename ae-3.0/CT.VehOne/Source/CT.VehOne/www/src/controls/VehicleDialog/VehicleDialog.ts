/* eslint-disable no-unused-vars */
import Dialog from "sap/m/Dialog";
import BaseDialog from "computec/appengine/common/controls/BaseDialog";
import FragmentHelper from "computec/appengine/common/helpers/FragmentHelper";
import { VehicleDialog$ConfirmEvent } from "./VehicleDialog.events";
import VehicleMasterData from "ct/vehone/models/VehicleMasterData";
import ValidationHelper from "computec/appengine/common/helpers/ValidationHelper";
import VehicleDialogModel from "./VehicleDialogModel";

/**
 * @namespace ct.vehone.controls.VehicleDialog
 */
export default class VehicleDialog extends BaseDialog {
	FRAGMENT = "ct.vehone.controls.VehicleDialog.VehicleDialog" as const;
	private _model: VehicleDialogModel = new VehicleDialogModel();

	constructor(idOrSettings?: string | $VehicleDialogSettings);
	constructor(id?: string, settings?: $VehicleDialogSettings);
	constructor(id?: string, settings?: $VehicleDialogSettings) {
		super(id, settings);
	}

	static readonly metadata = {
		properties: {
			vahicle: {
				type: "object", //VehicleMasterData
			},
		},
		events: {
			confirm: {
				parameters: {
					vehicle: { type: "object" }, //VehicleMasterData
				},
			},
			cancel: {},
		},
	};

	private async _open() {
		this.dialog = await FragmentHelper.getFragment<Dialog>(this.FRAGMENT, { controller: this });
		this.dialog.setEscapeHandler((p) => {
			this._close();
			this.fireCancel();
			p.resolve();
		});
		this.addDependent(this.dialog);
		this.addStandardEnterHandler(this._findInputs(this.dialog));
		this.dialog.setModel(this._model.JSONModel);
		ValidationHelper.addStandardValidationHandling(this.dialog, this._model);
		this.dialog.setBusyIndicatorDelay(0);

		this.dialog.open();
	}

	public async open() {
		await this._open();
		return new Promise<VehicleMasterData | void>((resolve) => {
			this.attachConfirm((oEvent: VehicleDialog$ConfirmEvent) => {
				resolve(oEvent.getParameter("vehicle"));
			});
			this.attachCancel(() => resolve());
		});
	}


	public getDialogModel() {
		return this._model;
	}

	public onConfirmPress() {
		if (!this._isValid()) return;
		this.fireConfirm({ vehicle: this._model.Vehicle });
		this._close();
	}
	private _isValid() {
		ValidationHelper.triggerValidation(this.dialog);
		if (this._model.HasErrors) {
			return false;
		}
		return true;
	}

	public onCancelPress() {
		this.fireCancel();
		this._close();
	}
}
