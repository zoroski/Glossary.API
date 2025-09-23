import { Component } from '@angular/core';
import { RouterOutlet, RouterLink } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  template: `
    <nav style="padding: 1rem; background: #efefef">
      <a routerLink="/login">Login</a> |
      <a routerLink="/terms">Terms</a>

    </nav>
    <router-outlet></router-outlet>
    <button class="btn btn-outline-danger" (click)="logout()">Logout</button>
  `
})
export class AppComponent {
   logout() {

    localStorage.removeItem('access_token');

    window.location.href = '/login';
  }
}