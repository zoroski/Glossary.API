import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { TermsTableComponent  } from './terms/terms-table.component'; 


export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'terms', component: TermsTableComponent  },
  { path: '', pathMatch: 'full', redirectTo: 'login' },
  { path: '**', redirectTo: 'login' }
];