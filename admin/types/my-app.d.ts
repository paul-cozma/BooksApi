import { LitElement } from 'lit-element';
import "./router/index";
import "./components/ui/top-menu";
/**
 * An example element.
 *
 * @slot - This element has a slot
 * @csspart button - The button
 */
export declare class MyApp extends LitElement {
    createRenderRoot(): this;
    static styles: import("lit-element").CSSResult;
    /**
     * The name to say "Hello" to.
     */
    name: string;
    /**
     * The number of times the button has been clicked.
     */
    count: number;
    render(): import("lit-element").TemplateResult;
    foo(): string;
}
declare global {
    interface HTMLElementTagNameMap {
        'my-app': MyApp;
    }
}
