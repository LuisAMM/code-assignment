import {Component, OnInit} from '@angular/core';
import {MatError, MatFormField, MatInput, MatLabel} from '@angular/material/input';
import {FormBuilder, FormControl, ReactiveFormsModule, Validators} from '@angular/forms';
import {CurrencyConverterService} from '../../services/currency-converter.service';
import {take} from 'rxjs';
import {HttpErrorResponse} from '@angular/common/http';
import {CurrencyError} from '../../dtos/currency-error.dto';
import {ErrorType} from '../../enums/error-type.enum';
import {MatSnackBar} from '@angular/material/snack-bar';
import {NgIf} from '@angular/common';
import {MatButton} from '@angular/material/button';

@Component({
  selector: 'app-currency-converter',
  standalone: true,
  imports: [
    MatInput,
    NgIf,
    MatButton,
    MatFormField,
    MatError,
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

  currencyConverterForm = this.formBuilder.group({
    amount: new FormControl<number>(0, {
      validators: [Validators.min(this.minAmount), Validators.max(this.maxAmount), Validators.required],
      nonNullable: true
    })
  });

  constructor(private readonly formBuilder: FormBuilder, private readonly converterService: CurrencyConverterService, private readonly snackBar: MatSnackBar) {
  }

  ngOnInit(): void {
    this.currencyConverterForm.controls.amount.valueChanges.subscribe({
      next: (value) => {
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
      this.errorMessage = `Amount must be greater than ${this.minAmount}`;
    } else if (control.hasError('max')) {
      this.errorMessage = `Amount must be less than ${this.maxAmount}`;
    } else if (control.hasError('required')) {
      this.errorMessage = 'Amount is required';
    } else {
      this.errorMessage = '';
    }
  }

  onSubmit(): void {
    if (this.currencyConverterForm.invalid) {
      return;
    }

    const amount = this.currencyConverterForm.controls.amount.value;
    this.converterService.getCurrencyInDollars(amount).pipe(take(1)).subscribe({
      next: (result) => {
        this.resultAmount = result.currency;
      },
      error: (error: HttpErrorResponse) => {
        console.log("Error: ", error)
        if (error.status === 400) {
          const errorResult = error.error as CurrencyError;
          switch (errorResult.errorType) {
            case ErrorType.OutOfRange:
              this.snackBar.open('Amount is out of range', 'x', {duration: 5000});
              break;
            case ErrorType.Generic:
            default:
              this.snackBar.open('Something unexpected happened', 'x', {duration: 5000});
              break;
          }
        } else {
          this.snackBar.open('Something unexpected happened', 'x', {duration: 5000});
        }
      }
    });
  }
}
