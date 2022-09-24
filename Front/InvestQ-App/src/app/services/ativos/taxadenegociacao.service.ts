import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TipoDeAtivo } from '@app/models/Enum/TipoDeAtivo.enum';
import { Guid } from 'guid-typescript';
import { Observable, take } from 'rxjs';
import { TaxaDeNegociacao } from '@app/models/TaxaDeNegociacao';
import { environment } from '@environments/environment';

@Injectable()

export class TaxadenegociacaoService {
  baseURL = environment.apiURL + 'api/taxadenegociacao';
  //baseURL = 'https://localhost:5001/api/taxadenegociacao';

  constructor(private http: HttpClient) { }

  public getAllTaxasDeNegociacoes(): Observable<TaxaDeNegociacao[]> {
    return this.http.get<TaxaDeNegociacao[]>(this.baseURL);
  }

  public getTaxaDeNegociacaoById(id: Guid): Observable<TaxaDeNegociacao> {
    return this
            .http
            .get<TaxaDeNegociacao>(`${this.baseURL}/${id}`)
            .pipe(take(1));
  }

  public post(taxaDeNegociacao: TaxaDeNegociacao): Observable<TaxaDeNegociacao> {
    return this.http
      .post<TaxaDeNegociacao>(this.baseURL, taxaDeNegociacao)
      .pipe(take(1));
  }

  public deleteTaxaDeNegociacao(id: Guid): Observable<any> {
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
