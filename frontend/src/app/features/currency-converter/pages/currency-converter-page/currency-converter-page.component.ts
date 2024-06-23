import { Component } from '@angular/core';
import {CurrencyConverterComponent} from '../../components/currency-converter/currency-converter.component';

@Component({
  selector: 'app-currency-converter-page',
  standalone: true,
  imports: [
    CurrencyConverterComponent
  ],
  templateUrl: './currency-converter-page.component.html',
  styleUrl: './currency-converter-page.component.scss'
})
export class CurrencyConverterPageComponent {

}
