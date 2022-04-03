import { LitElement, html, customElement, property } from "lit-element";


// declare new interface for the list property, containing a list of id, appName, and envs

interface List {
    id: number;
    appName: string;
    envs: Envs;
}
// add interface for envs with id, name, url, and appModelId

export interface Envs {
    id: number;
    name: string;
    url: string;
    appModelId: number;
}

@customElement("list-apps")
export class ListApps extends LitElement {
// add list property as an array of objects
@property({ type: Array })
private list: List[] = [];

    render() {
        return html`
        <div class="list-apps">
            <h1>List of apps</h1>
            <ul>
                ${this.list.map(
                    (item: List) => html`
                    <li>
                        <a href="/app/${item.id}">${item.appName}</a>
                    </li>
                    `
                )}

        `;
    }
    private async getList() {
        // get list of apps  with fetch
        const res = await fetch("http://localhost:5185/api/AppBuild");
        const data = await res.json();
        this.list = data;
    }
    constructor() {
        super();
        this.getList();
    }
    }
    declare global {
        interface HTMLElementTagNameMap {
            "list-apps": ListApps;
        }
    }
