import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html'
})
export class LoginComponent {
  username = 'author';
  password = 'Passw0rd!';
  busy = false;
  error: string | null = null;

  constructor(private auth: AuthService, private router: Router) {}

  submit() {
    this.error = null;
    this.busy = true;
    this.auth.login(this.username, this.password).subscribe({
      next: (res) => {
        this.auth.setToken(res.access_token);
        this.router.navigateByUrl('/terms');
      },
      error: () => {
        this.error = 'Login failed';
        this.busy = false;
      }
    });
  }
}
