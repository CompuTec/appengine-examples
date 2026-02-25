import { SDKEnv } from "sbo/ui/core/SDKEnv";
import Event from "sbo/ui/base/Event";
import { MessageBoxAction, MessageBoxType } from "sbo/m/MessageBox";
import Controller from "sbo/ui/core/mvc/Controller";
import { StringJoin } from "./utility";
import MyData from "../model/models";

export default class View implements Controller {

	/**
	  * Event handler when initializing the current view.
	  * @param {SDKEnv} oEnv the current sdk environment
	  * @param {Event} oEvent the view init event
	  */
	public async onInit(oEnv: SDKEnv, oEvent: Event): Promise<void> {
		console.log("OnInit invoked");
	}
	/**
	 * Event handler when loading data to the current view. It is asynchronously triggered after onInit.
	 * @param {SDKEnv} oEnv the current sdk environment
	 * @param {Event} oEvent the view dataLoad event
	 */
	public async onDataLoad(oEnv: SDKEnv, oEvent: Event): Promise<void> {
		console.log("onDataLoad invoked");
	}

	/**
	 * Event handler when exiting from the current view.
	 * @param {SDKEnv} oEnv the current sdk environment
	 * @param {Event} oEvent the view exit event
	 */
	public async onExit(oEnv: SDKEnv, oEvent: Event): Promise<void> {
		console.log("onExit invoked");
	}


	/**
	 * Event handler on clicking the customized button.
	 * @param {SDKEnv} oEnv the current sdk environment
	 * @param {Event} oEvent the button click event
	 */
	public async onButtonClick(oEnv: SDKEnv, oEvent: Event): Promise<void> {
		try {
			const pluginId = `&/ct.vehone`;
			const pluginDefaultPath = `,home`;
			const pluginName = "CT.VehOne";
			let pluginPath = `${pluginId}${pluginDefaultPath}`;
			await oEnv.open(`#webclient-CompuTecWebClientStart-CompuTecWebClientStart${pluginPath}/${pluginName}`);
		} catch (error) {
			console.error(error);
			oEnv.showMessageBox(MessageBoxType.Error, `Could not open plugin due to an error. Check console for more details.`);
		}
	}
}

