import { BaseView } from "./base-view";
export declare class AppList extends BaseView {
    render(): import("lit-element").TemplateResult;
}
declare global {
    interface HTMLElementTagNameMap {
        "app-list": AppList;
    }
}
