// scaffold basic app-home with lit element

import { customElement, html } from "lit-element";
import { BaseView } from "./base-view";

@customElement("app-home")
export class AppHome extends BaseView {
    render() {
        return html`
        <h1>Hello, ma dudes!</h1>
        `;
    }
    }

declare global {
    interface HTMLElementTagNameMap {
        "app-home": AppHome;
    }
}

