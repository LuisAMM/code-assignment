import {Component, OnInit} from '@angular/core';
import {MatError, MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {MatSelect, MatOption} from '@angular/material/select';
import {FormBuilder, FormControl, ReactiveFormsModule, Validators} from '@angular/forms';
import {CurrencyConverterService} from '../../services/currency-converter.service';
import {TranslationService} from '../../services/translation.service';
import {take} from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import {CurrencyError} from '../../dtos/currency-error.dto';
import {ErrorType} from '../../enums/error-type.enum';
import {Language} from '../../enums/language.enum';
import {MatSnackBar} from '@angular/material/snack-bar';

import {MatButton} from '@angular/material/button';

@Component({
    selector: 'app-currency-converter',
    imports: [
    MatInput,
    MatButton,
    MatFormField,
    MatError,
    MatSelect,
    MatOption,
    ReactiveFormsModule,
    MatLabel
],
    templateUrl: './currency-converter.component.html',
    styleUrl: './currency-converter.component.scss'
})
export class CurrencyConverterComponent implements OnInit {

  resultAmount = '';
  errorMessage = '';
  maxAmount = 999999999.99;
  minAmount = 0;

  readonly languageOptions = [
    {value: Language.En, label: 'English'},
    {value: Language.De, label: 'Deutsch'}
  ];

  currencyConverterForm = this.formBuilder.group({
    amount: new FormControl<number>(0, {
      validators: [Validators.min(this.minAmount), Validators.max(this.maxAmount), Validators.required],
      nonNullable: true
    }),
    language: new FormControl<Language>(Language.En, {
      nonNullable: true
    })
  });

  constructor(private readonly formBuilder: FormBuilder,
              private readonly converterService: CurrencyConverterService,
              private readonly snackBar: MatSnackBar,
              readonly translationService: TranslationService) {
  }

  ngOnInit(): void {
    this.currencyConverterForm.controls.amount.valueChanges.subscribe({
      next: () => {
        if (this.resultAmount.length > 0) {
          this.resultAmount = '';
        }
        this.setErrorMessage();
      }
    });

    this.currencyConverterForm.controls.language.valueChanges.subscribe({
      next: (language) => {
        this.translationService.setLanguage(language);
        if (this.resultAmount.length > 0) {
          this.resultAmount = '';
        }
        this.setErrorMessage();
      }
    });
  }

  setErrorMessage() {
    const control = this.currencyConverterForm.controls.amount;
    if (control.hasError('min')) {
      this.errorMessage = this.translationService.translate('amountTooSmall', {min: this.minAmount});
    } else if (control.hasError('max')) {
      this.errorMessage = this.translationService.translate('amountTooLarge', {max: this.maxAmount});
    } else if (control.hasError('required')) {
      this.errorMessage = this.translationService.translate('amountRequired');
    } else {
      this.errorMessage = '';
    }
  }

  onSubmit(): void {
    if (this.currencyConverterForm.invalid) {
      return;
    }

    const amount = this.currencyConverterForm.controls.amount.value;
    const language = this.currencyConverterForm.controls.language.value;
    this.converterService.getCurrencyInDollars(amount, language).pipe(take(1)).subscribe({
      next: (result) => {
        this.resultAmount = result.currency;
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error: ", error)
        const closeAction = this.translationService.translate('closeAction');
        if (error.status === 400) {
          const errorResult = error.error as CurrencyError;
          switch (errorResult.errorType) {
            case ErrorType.OutOfRange:
              this.snackBar.open(this.translationService.translate('errorOutOfRange'), closeAction, {duration: 5000});
              break;
            case ErrorType.Generic:
            default:
              this.snackBar.open(this.translationService.translate('errorGeneric'), closeAction, {duration: 5000});
              break;
          }
        } else {
          this.snackBar.open(this.translationService.translate('errorGeneric'), closeAction, {duration: 5000});
        }
      }
    });
  }
}
