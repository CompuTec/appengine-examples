import FormatterHelper from "computec/appengine/uicore/helpers/FormatterHelper";

export default class Formatter {
	public translate = (text: string, data?: any[]) => FormatterHelper.getText(text, data);

	public translateSilent(text: string) {
		try {
			return FormatterHelper.getText(text);
		} catch {
			return text;
		}
	}
}
