import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private api = 'https://localhost:5001';

  constructor(private http: HttpClient, private router: Router) {}

  login(username: string, password: string) {
    return this.http.post<{ access_token: string }>(`${this.api}/login`, { username, password });
  }

  setToken(token: string) {
    localStorage.setItem('access_token', token);
  }

  get token(): string | null {
    return localStorage.getItem('access_token');
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  logout() {
    localStorage.removeItem('access_token');
    this.router.navigateByUrl('/login');
  }
}
