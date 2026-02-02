import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Aluno, CreateAluno, UpdateAluno } from '../models';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AlunoService {
  private http = inject(HttpClient);
  private readonly baseUrl = `${environment.apiUrl}/alunos`;

  getAll(): Observable<Aluno[]> {
    return this.http.get<Aluno[]>(this.baseUrl);
  }

  getById(id: number): Observable<Aluno> {
    return this.http.get<Aluno>(`${this.baseUrl}/${id}`);
  }

  getByEscolaId(escolaId: number): Observable<Aluno[]> {
    return this.http.get<Aluno[]>(`${this.baseUrl}/escola/${escolaId}`);
  }

  create(aluno: CreateAluno): Observable<Aluno> {
    return this.http.post<Aluno>(this.baseUrl, aluno);
  }

  update(id: number, aluno: UpdateAluno): Observable<Aluno> {
    return this.http.put<Aluno>(`${this.baseUrl}/${id}`, aluno);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}

