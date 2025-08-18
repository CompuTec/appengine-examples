var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class View {
        /**
          * Event handler when initializing the current view.
          * @param {SDKEnv} oEnv the current sdk environment
          * @param {Event} oEvent the view init event
          */
        onInit(oEnv, oEvent) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("OnInit invoked");
            });
        }
        /**
         * Event handler when loading data to the current view. It is asynchronously triggered after onInit.
         * @param {SDKEnv} oEnv the current sdk environment
         * @param {Event} oEvent the view dataLoad event
         */
        onDataLoad(oEnv, oEvent) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("onDataLoad invoked");
            });
        }
        /**
         * Event handler when exiting from the current view.
         * @param {SDKEnv} oEnv the current sdk environment
         * @param {Event} oEvent the view exit event
         */
        onExit(oEnv, oEvent) {
            return __awaiter(this, void 0, void 0, function* () {
                console.log("onExit invoked");
            });
        }
        /**
         * Event handler on clicking the customized button.
         * @param {SDKEnv} oEnv the current sdk environment
         * @param {Event} oEvent the button click event
         */
        onButtonClick(oEnv, oEvent) {
            return __awaiter(this, void 0, void 0, function* () {
                try {
                    const pluginId = `&/ct.vehone`;
                    const pluginDefaultPath = `,home`;
                    const pluginName = "CT.VehOne";
                    let pluginPath = `${pluginId}${pluginDefaultPath}`;
                    yield oEnv.open(`#webclient-CompuTecWebClientStart-CompuTecWebClientStart${pluginPath}/${pluginName}`);
                }
                catch (error) {
                    console.error(error);
                    oEnv.showMessageBox("Error" /* MessageBoxType.Error */, `Could not open plugin due to an error. Check console for more details.`);
                }
            });
        }
    }
    exports.default = View;
});
//# sourceMappingURL=View.js.map