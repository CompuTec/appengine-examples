import UIComponent from "computec/appengine/uicore/UIComponent";

/**
 * @namespace ct.vehone
 */
export default class Component extends UIComponent {
	static ODATA_MODEL_NAME = "CustomModel";
	public static metadata = {
		manifest: "json",
	};
	init() {
		super.init();
		this.initRouter();
		try {
			this.attachSLOdataModel("odata/CTVehOne/", Component.ODATA_MODEL_NAME);
		} catch (error) {
			console.error(error);
		}
	}
}
