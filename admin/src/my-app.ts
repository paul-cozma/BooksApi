import { LitElement, html, customElement, property, css } from 'lit-element'
import "./router/index";
import "./components/ui/top-menu";
/**
 * An example element.
 *
 * @slot - This element has a slot
 * @csspart button - The button
 */
@customElement('my-app')
export class MyApp extends LitElement {
    createRenderRoot() {
        return this;
      }
  static styles = css`
    :host {
      display: block;
      border: solid 1px gray;
      padding: 16px;
      max-width: 800px;
    }
  `

  /**
   * The name to say "Hello" to.
   */
  @property()
  name = 'World'

  /**
   * The number of times the button has been clicked.
   */
  @property({ type: Number })
  count = 0

  render() {
    return html`
      <top-menu></top-menu>
      <div id="app"></div>
    `
  }



  foo(): string {
    return 'foo'
  }
}

declare global {
  interface HTMLElementTagNameMap {
    'my-app': MyApp
  }
}