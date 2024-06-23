import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {
  CurrencyConverterPageComponent
} from './features/currency-converter/pages/currency-converter-page/currency-converter-page.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CurrencyConverterPageComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'frontend';
}
