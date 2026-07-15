import { Component } from '@angular/core';
import {
  CurrencyConverterPageComponent
} from './features/currency-converter/pages/currency-converter-page/currency-converter-page.component';

@Component({
    selector: 'app-root',
    imports: [CurrencyConverterPageComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'frontend';
}
