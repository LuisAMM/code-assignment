import { Injectable, computed, signal } from '@angular/core';
import { Language } from '../enums/language.enum';

export type TranslationKey =
  | 'pageTitle'
  | 'amountLabel'
  | 'amountPlaceholder'
  | 'invalidAmount'
  | 'amountRequired'
  | 'amountTooSmall'
  | 'amountTooLarge'
  | 'submit'
  | 'languageLabel'
  | 'errorOutOfRange'
  | 'errorGeneric'
  | 'closeAction';

type TranslationMap = Record<TranslationKey, string>;

const TRANSLATIONS: Record<Language, TranslationMap> = {
  [Language.En]: {
    pageTitle: 'Currency converter',
    amountLabel: 'Enter an amount',
    amountPlaceholder: '85',
    invalidAmount: 'Invalid amount',
    amountRequired: 'Amount is required',
    amountTooSmall: 'Amount must be greater than {min}',
    amountTooLarge: 'Amount must be less than {max}',
    submit: 'Submit',
    languageLabel: 'Language',
    errorOutOfRange: 'Amount is out of range',
    errorGeneric: 'Something unexpected happened',
    closeAction: 'x'
  },
  [Language.De]: {
    pageTitle: 'Währungsumrechner',
    amountLabel: 'Betrag eingeben',
    amountPlaceholder: '85',
    invalidAmount: 'Ungültiger Betrag',
    amountRequired: 'Betrag ist erforderlich',
    amountTooSmall: 'Der Betrag muss größer als {min} sein',
    amountTooLarge: 'Der Betrag muss kleiner als {max} sein',
    submit: 'Absenden',
    languageLabel: 'Sprache',
    errorOutOfRange: 'Der Betrag liegt außerhalb des gültigen Bereichs',
    errorGeneric: 'Etwas Unerwartetes ist passiert',
    closeAction: 'x'
  }
};

@Injectable({
  providedIn: 'root'
})
export class TranslationService {

  private readonly currentLanguage = signal<Language>(Language.En);

  public readonly language = this.currentLanguage.asReadonly();

  public readonly texts = computed(() => TRANSLATIONS[this.currentLanguage()]);

  public setLanguage(language: Language): void {
    this.currentLanguage.set(language);
  }

  public translate(key: TranslationKey, params?: Record<string, string | number>): string {
    let text = this.texts()[key];
    if (params) {
      for (const [param, value] of Object.entries(params)) {
        text = text.replace(`{${param}}`, String(value));
      }
    }
    return text;
  }
}

