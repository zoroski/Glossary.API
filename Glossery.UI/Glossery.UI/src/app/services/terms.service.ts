import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { envApiBase } from '../app.config';
import { Observable } from 'rxjs';

export type Status = 'Draft' | 'Published' | 'Archived';
export interface TermDto { id: string; name: string; definition: string; status: Status; }

@Injectable({ providedIn: 'root' })
export class TermsService {
  private base = `${envApiBase}/Term`;
  constructor(private http: HttpClient) {}

  list(): Observable<TermDto[]> {
    return this.http.get<TermDto[]>(this.base);
  }
  create(name: string, definition: string): Observable<string> {
    return this.http.post<string>(`${this.base}/createterm`, { name, definition });
  }
  publish(id: string) { return this.http.post<void>(`${this.base}/${id}/publish`, {}); }
  archive(id: string) { return this.http.post<void>(`${this.base}/${id}/archive`, {}); }
  delete(id: string)  { return this.http.delete<void>(`${this.base}/${id}/delete`); }
}
