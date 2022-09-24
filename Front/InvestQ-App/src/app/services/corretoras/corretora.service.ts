import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take, catchError, throwError, empty, of } from 'rxjs';
import { Corretora } from '@app/models/Corretora';
import { environment } from '@environments/environment';
import { Guid } from 'guid-typescript';

@Injectable(
  //{providedIn: 'root'}
)
export class CorretoraService {
  baseURL = environment.apiURL + 'api/corretora';

  constructor(private http: HttpClient) { }

  // Headers
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  public getAllCorretoras(): Observable<Corretora[]> {
    return this.http.get<Corretora[]>(this.baseURL);
  }

  public getCorretoraByDescricao(descricao: string): Observable<Corretora> {
    return this.http.get<Corretora>(`${this.baseURL}/${descricao}/descricao`);
  }

  public getCorretoraById(id: Guid): Observable<Corretora> {
    return this.http.get<Corretora>(`${this.baseURL}/${id}`);
  }

  public post(corretora: Corretora): Observable<Corretora> {
    return this.http
      .post<Corretora>(this.baseURL, corretora, this.httpOptions)
      .pipe(
            take(1),
            catchError(this.handleError)
            );
  }

  public put(corretora: Corretora): Observable<Corretora> {
    return this.http
      .put<Corretora>(`${this.baseURL}/${corretora.id}`, corretora)
      .pipe(take(1));
  }

  public deleteCorretora(id: Guid): Observable<any> {
    return this.http
      .delete(`${this.baseURL}/${id}`)
      .pipe(take(1));
  }

  public postUpload(corretoraId: Guid, file: File): Observable<Corretora> {
    const fileToUpload = file[0] as File;

    const formData = new FormData();

    formData.append('file', fileToUpload);

    return this.http
      .post<Corretora>(`${this.baseURL}/upload-image/${corretoraId}`, formData)
      .pipe(take(1));
  }

  // Manipulação de erros
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    let _error = [];

    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    //console.log(errorMessage);
    console.log(error);
    console.log(error.error);

    _error.push(errorMessage);
    _error.push(error.error);

    //error.error.forEach(function(item) {
    //  _error.push(item.message);
    //});

    return throwError(_error);
  };

}
