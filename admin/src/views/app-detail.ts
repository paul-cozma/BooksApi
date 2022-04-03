import { LitElement, html, customElement, property } from "lit-element";
import { Envs } from '../components/list-apps'
// define interface for app

interface AppResponse {
    id: number;
    appName: string;
    description: string;
    envs: Envs[];
}
interface Registry {
    name: string;
    tags: string[];
}

@customElement("app-detail")
export class AppDetail extends LitElement {
    @property({ type: Object })
    location: any;
    @property({ type: Object })
    app = {} as AppResponse;

    @property({ type: Object })
    registry: Registry = {tags: ['']} as Registry;
    render() {
        return html`
        <h1>${this.app.appName}!</h1>
        <h3>Docker image ${this.registry.name}</h3>
        <ul>
            ${this.registry.tags.map((tag:any) => html`
            <li>${tag}</li>
            `)
            }
        </ul>

        `;
        }
        constructor() {
            super();
            console.log(this.location);
        }
        private async getApp(params:any) {
            // fetch app with params.id
            const res = await fetch(`http://localhost:5185/api/AppBuild/${params}`);
            const data = await res.json();
            this.app = data;
            this.getAppRegistry(data.id);
        }
        private async getAppRegistry(id:any) {
            const res = await fetch(`http://localhost:5185/api/AppBuild/${id}/registry`);
            const data = await res.json();
            this.registry = data;
        }
        updated(changedProperties:any) {
            console.log(this.location, changedProperties);
            let isLocation = changedProperties.has("location");
            console.log(isLocation);
            if (isLocation) {
                console.log(this.location.params);
                this.getApp(this.location.params.id);
            }
        
    }
        
    }
    declare global {
        interface HTMLElementTagNameMap {
            "app-detail": AppDetail;
        }
    }
