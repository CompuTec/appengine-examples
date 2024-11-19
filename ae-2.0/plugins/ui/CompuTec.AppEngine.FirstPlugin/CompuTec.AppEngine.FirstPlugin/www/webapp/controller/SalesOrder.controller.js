sap.ui.define([
	"computec/appengine/core/BaseController",
	"sap/ui/core/Fragment",
	"sap/ui/model/json/JSONModel",
	"computec/appengine/ui/model/http/Http",
	"sap/ui/model/Filter",
	"sap/ui/model/FilterOperator"
],
	/**
	 * 
	 * @param {typeof computec.appengine.core.BaseController} BaseController 
	 * @param {typeof sap.ui.core.Fragment} Fragment 
	 * @param {typeof sap.ui.model.json.JSONModel} JSONModel
	 * @param {typeof computec.appengine.ui.model.http.Http} Http
	 * @param {typeof sap.ui.model.Filter} Filter
	 * @param {typeof sap.ui.model.FilterOperator} FilterOperator
	 *
	 * @returns 
	 */
	function (BaseController, Fragment, JSONModel, Http, Filter, FilterOperator) {
		"use strict";

		return BaseController.extend("computec.appengine.firstPlugin.controller.SalesOrder", {
			_attachmentsAddDialog: null,
			onInit: function () {
				BaseController.prototype.onInit.call(this);
				this.setPageName("Sales Orders");
			},
			onAttachmentsButtonPress: async function (oEvent) {
				/** @type {sap.m.GenericTag} */
				const oGenericTag = oEvent.getSource();

				const nAtcEntry = this.getCustomDataForElement(oGenericTag, "AtcEntry");
				const nDocEntry = this.getCustomDataForElement(oGenericTag, "DocEntry");


				const result = await this.getAttachmentsByDocEntry(nAtcEntry);
				const data = {
					Attachmnets: result.value,
					DocEntry: nDocEntry,
					AtcEntry: nAtcEntry
				};
				this.onOpenDialog(data);
			},

			onSalesOrderRefresh: function () {
				this.byId('salesOrdersTable').getModel("AE").refresh();
			},

			onOpenDialog: async function (data) {
				const oView = this.getView();

				if (!this._attachmentsDialog) {
					this._attachmentsDialog = await Fragment.load({
						id: oView.getId(),
						name: "computec.appengine.firstplugin.view.SalesOrderAttachmentsDialog",
						controller: this
					});
					oView.addDependent(this._attachmentsDialog);
				}

				this._attachmentsDialog.setModel(new JSONModel(data), "AT");
				this._attachmentsDialog.open();
			},
			onAttachmentsDialogCloseFragment: function () {
				this._attachmentsDialog.close();
			},
			onAttachmentsDialogDownloadInNewTab: function (oEvent) {
				const oSource = oEvent.getSource();
				const AbsEntry = this.getCustomDataForElement(oSource, "AbsEntry");
				const Line = this.getCustomDataForElement(oSource, "Line");
				const sUrl = `${window.location.origin}/api/Attachments/GetAttachmentByCustomKey/ORDR/DocEntry/${AbsEntry}/null/${Line}`;
				window.open(sUrl, '_blank');
			},



			//#region ADD ATTACHMENTS DIALOG
			onAttachmentDialogAddAttachment: async function (oEvent) {
				await this.onOpenAddAttachmentDialog();
			},
			onOpenAddAttachmentDialog: async function (data) {
				const oView = this.getView();
				if (!this._attachmentsAddDialog) {
					this._attachmentsAddDialog = await Fragment.load({
						id: oView.getId(),
						name: "computec.appengine.firstplugin.view.SalesOrderAttachmentsDialogAdd",
						controller: this
					});
					oView.addDependent(this._attachmentsAddDialog);
				}
				this._attachmentsAddDialog.open();
			},
			onAddAttachmentSubmit: async function () {
				/** @type {sap.ui.unified.FileUploader} */
				const oFileUploader = this.byId("FileUploader");
				let domRef = oFileUploader.getFocusDomRef(),
					file = domRef.files[0];
				if (!file) {
					alert("No File Uploaded!");
					return;
				}
				const fromData = new FormData();
				fromData.append("file", file);
				const sUrl = `${window.location.origin}/api/Attachments/SetAttachment/false/false`;

				try {
					const response = await fetch(sUrl, {
						method: 'POST',
						body: fromData
					});
					console.log(response);
					const oATModel = this._attachmentsDialog.getModel("AT");
					const DocEntry = oATModel.getProperty("/DocEntry");
					await this._addAttachmentToSalesOrder(DocEntry, file.name);
					this._refreshAttachments();
					this.onSalesOrderRefresh();
				} catch (oError) {
					console.log(oError);
				} finally {
					this.onAddAttachmentDialogClose();
				}
			},
			onAddAttachmentDialogClose: function () {
				this._attachmentsAddDialog.close();
			},
			//#endregion

			// #region INTERNAL
			getCustomDataForElement: function (oElement, sCustomDataCode) {
				let oCustomData = oElement.getCustomData().find(x => x.getKey() === sCustomDataCode);
				if (oCustomData)
					return oCustomData.getValue();
				return null;
			},
			findElementByCustomId: function (oDialog, sCustomId) {
				const oCtr = oDialog.findElements(true).find(
					el => {
						let sElId = this.getCustomDataForElement(el, "id");
						return sElId == sCustomId;
					}
				);
				return oCtr;
			},
			getAttachmentsByDocEntry: function (sDocNum) {
				const sUrl = encodeURIComponent(`odata/CustomViews/Views.CustomWithParameters(Id='FirstPlugin:Attachments',Parameters=["AbsEntry=${sDocNum}"],paramType=Default.ParamType'Custom')`);
				return this._get(sUrl);
			},

			_refreshAttachments: async function () {
				const oATModel = this._attachmentsDialog.getModel("AT");
				const nAtcEntry = oATModel.getProperty("/AtcEntry");

				const result = await this.getAttachmentsByDocEntry(nAtcEntry);

				oATModel.setProperty("/Attachmnets", result.value);
				oATModel.refresh();
			},

			_addAttachmentToSalesOrder: async function (nSalesOrderDocEntry, sFileName) {
				const sUrl = `api/FirstPlugin/SalesOrder/AddAttachment`;
				const aNameParts = sFileName.split(".");
				let sExtension = null;
				if (aNameParts.length > 1)
					sExtension = aNameParts.pop();

				const sName = aNameParts.join(".");

				const oParams = {
					"DocEntry": nSalesOrderDocEntry,
					"FileName": sName,
					"FileExtension": sExtension
				};

				try {
					const res = await this._post(JSON.stringify(oParams), sUrl);
					return;
				} catch (error) {
					throw error;
				}
			},

			_get: function (sUrl) {
				return new Promise((resolve, reject) => {
					Http.request({
						method: 'GET',
						withAuth: true,
						url: sUrl,
						done: resolve,
						fail: reject
					});
				});
			},

			_post: function (sData, sUrl) {
				return new Promise((resolve, reject) => {
					Http.request({
						method: 'POST',
						withAuth: true,
						url: sUrl,
						data: sData,
						done: resolve,
						fail: reject
					});
				});
			},

			//#endregion
			onFilter: function (oEvent) {
				/** @type {Array<sap.ui.model.Filter>} */
				const aFilter = [];
				const sQuery = oEvent.getParameter("query");
				if (typeof sQuery === 'string' && sQuery.length > 0) {
					aFilter.push(new Filter({
						path: 'CardCode',
						operator: FilterOperator.EQ,
						value1: sQuery
					}));
					aFilter.push(new Filter({
						path: 'CardName',
						operator: FilterOperator.Contains,
						value1: sQuery
					}));
				}
				const filter = new Filter({
					filters: aFilter,
					and: false
				});

				const sFilter = this.getStaticFilterExpression(filter);

				var oList = this.byId("salesOrdersTable");
				oList.getBinding("items").changeParameters({
					$filter: sFilter
				});

			},

			/**
				 * Method that returns static filter expression
				 * @param {sap.ui.model.Filter} oFilter this filter will be used to generate static filter expression
				 * @returns {string} static filter expression
				 */
			getStaticFilterExpression: function (oFilter) {
				// @ts-ignore
				var aFilters = oFilter.aFilters;
				var sFilterCurrent;
				var sFilterChilds;
				var sFilter;
				// @ts-ignore
				var sOperator = oFilter.bAnd ? 'and' : 'or';
				// @ts-ignore
				if (oFilter.sPath && oFilter.sPath.length > 0) {
					// @ts-ignore
					switch (oFilter.sOperator) {
						case "EQ":
							// @ts-ignore
							let value = oFilter.oValue1;
							if (typeof (value) === 'number') {
								sFilterCurrent = oFilter.sPath + " eq " + value + "";
							} else if (value.substring(0, 6) === 'Enums.')
								sFilterCurrent = oFilter.sPath + " eq " + value + "";
							else
								sFilterCurrent = oFilter.sPath + " eq '" + value + "'";
							break;
						case "Contains":
							// @ts-ignore
							sFilterCurrent = "contains(" + oFilter.sPath + ", '" + oFilter.oValue1 + "')";
							break;
						default:
							break;
					}
				}
				if (aFilters && aFilters.length > 0) {
					sFilterChilds = "";
					for (var fi = 0; fi < aFilters.length; fi++) {
						var oChildFilter = aFilters[fi];
						sFilterChilds = sFilterChilds + this.getStaticFilterExpression(oChildFilter);
						if (fi < aFilters.length - 1) {
							sFilterChilds = sFilterChilds + " " + sOperator + " ";
						}
					}
				}
				if (sFilterCurrent || sFilterChilds) {
					sFilter = "";
					if (sFilterCurrent && sFilterCurrent.length > 0) {
						sFilter = sFilter + sFilterCurrent + " ";
						if (sFilterChilds && sFilterChilds.length > 0) {
							sFilter = sFilter + " " + sOperator + " ";
						}
					}
					if (sFilterChilds && sFilterChilds.length > 0) {
						sFilter = sFilter + "(" + sFilterChilds + ")";
					}
				}
				return sFilter;
			},

		});
	});




















