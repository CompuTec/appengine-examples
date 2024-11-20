import UIComponent from "computec/appengine/uicore/UIComponent";

/**
 * @namespace ct.vehone
 */
export default class Component extends UIComponent {
	public static metadata = {
		manifest: "json"
	}
	public init() {
		super.init();
		UIComponent.prototype.onInit.call(this);

		this.getRouter().initialize();
		this.attachSLOdataModel("odata/CTVehOne", "CustomModel");
	}
}