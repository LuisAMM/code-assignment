import { Component } from '@angular/core';
import {CurrencyConverterComponent} from '../../components/currency-converter/currency-converter.component';
import {TranslationService} from '../../services/translation.service';

@Component({
    selector: 'app-currency-converter-page',
    imports: [
        CurrencyConverterComponent
    ],
    templateUrl: './currency-converter-page.component.html',
    styleUrl: './currency-converter-page.component.scss'
})
export class CurrencyConverterPageComponent {
  constructor(readonly translationService: TranslationService) {
  }
}
