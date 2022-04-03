
// create the top menu component

import { LitElement, customElement, html } from "lit-element";

@customElement("top-menu")
export class TopMenu extends LitElement {
    render() {
        return html`
        <div class="top-menu">
            <div class="top-menu-item">
            <a href="/">Home</a>
            </div>
            <div class="top-menu-item">
            <a href="/list">List</a>
            </div>
        </div>
        `;
    }
    }
    declare global {
        interface HTMLElementTagNameMap {
            "top-menu": TopMenu;
        }
    }
