import UIComponent from "computec/appengine/uicore/UIComponent";

/**
 * @namespace ct.vehone
 */
export default class Component extends UIComponent {
	public static metadata = {
		manifest: "json",
	};
	init() {
		super.init();
		this.initRouter();
		try {
			this.attachSLOdataModel("odata/CTVehOne/", "CustomModel");
		} catch (error) {
			console.error(error);
		}
	}
}
