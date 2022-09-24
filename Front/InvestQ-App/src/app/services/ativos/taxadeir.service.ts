import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, take } from 'rxjs';

import { Guid } from 'guid-typescript';

import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';
import { TaxaDeIR } from '@app/models/TaxaDeIR';
import { environment } from '@environments/environment';

@Injectable()

export class TaxadeirService {
  baseURL = environment.apiURL + 'api/taxadeir';
  //baseURL = 'https://localhost:5001/api/taxadeir';

  constructor(private http: HttpClient) { }

  public getAllTaxasDeIR(): Observable<TaxaDeIR[]> {
    return this.http.get<TaxaDeIR[]>(this.baseURL);
  }

  public getTaxaDeIRById(id: Guid): Observable<TaxaDeIR> {
    return this
            .http
            .get<TaxaDeIR>(`${this.baseURL}/${id}`)
            .pipe(take(1));
  }

  public post(taxaDeIR: TaxaDeIR): Observable<TaxaDeIR> {
    return this.http
      .post<TaxaDeIR>(this.baseURL, taxaDeIR)
      .pipe(take(1));
  }

  public deleteTaxaDeIR(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  getTipoDeAtivo() {
    return [
      {valor: TipoDeAtivo.NaoInformada, desc: 'NaoInformada' },
      {valor: TipoDeAtivo.Caixa, desc: 'Caixa' },
      {valor: TipoDeAtivo.Acao, desc: 'Acao' },
      {valor: TipoDeAtivo.FundoImobiliario, desc: 'FundoImobiliario' },
      {valor: TipoDeAtivo.TesouroDireto, desc: 'TesouroDireto' }
    ];
  }

}
