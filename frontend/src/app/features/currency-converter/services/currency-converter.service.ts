import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CurrencyResultDto} from '../dtos/currency-result.dto';
import {environment} from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CurrencyConverterService {

  private readonly fullPath = environment.apiUrl + 'currency'
  constructor(private readonly httpClient: HttpClient) { }

  public getCurrencyInDollars(currency: number): Observable<CurrencyResultDto> {
    let params = new HttpParams().append('amount', currency);
    return this.httpClient.get<CurrencyResultDto>(this.fullPath, {params});
  }
}
