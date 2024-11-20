import AEBaseController from "computec/appengine/uicore/plugin/BaseController";

/**
 * @namespace ct.vehone.controller
 */
export default class Home extends AEBaseController {
	onInit(): void {
		super.onInit();
		this.setPageName("homePageTitle");
	}
}
