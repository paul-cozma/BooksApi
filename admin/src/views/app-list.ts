import { html, customElement } from "lit-element";
import { BaseView } from "./base-view";

import  "../components/list-apps";
@customElement("app-list")
export class AppList extends BaseView {
    render() {
        return html`
        <h1>Hello, list!</h1>
        <list-apps></list-apps>
        `;
    }
}

declare global {
    interface HTMLElementTagNameMap {
        "app-list": AppList;
    }
}
