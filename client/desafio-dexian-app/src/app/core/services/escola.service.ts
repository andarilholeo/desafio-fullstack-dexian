import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Escola, CreateEscola, UpdateEscola } from '../models';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EscolaService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/escolas`;

  getAll(): Observable<Escola[]> {
    return this.http.get<Escola[]>(this.baseUrl);
  }

  getById(id: number): Observable<Escola> {
    return this.http.get<Escola>(`${this.baseUrl}/${id}`);
  }

  create(escola: CreateEscola): Observable<Escola> {
    return this.http.post<Escola>(this.baseUrl, escola);
  }

  update(id: number, escola: UpdateEscola): Observable<Escola> {
    return this.http.put<Escola>(`${this.baseUrl}/${id}`, escola);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}

