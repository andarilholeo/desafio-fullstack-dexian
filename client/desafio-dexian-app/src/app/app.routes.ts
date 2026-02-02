import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('./features/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: '',
    loadComponent: () => import('./shared/components/layout/layout.component').then(m => m.LayoutComponent),
    canActivate: [authGuard],
    children: [
      {
        path: 'alunos',
        loadComponent: () => import('./features/alunos/alunos.component').then(m => m.AlunosComponent)
      },
      {
        path: 'escolas',
        loadComponent: () => import('./features/escolas/escolas.component').then(m => m.EscolasComponent)
      },
      {
        path: '',
        redirectTo: 'alunos',
        pathMatch: 'full'
      }
    ]
  },
  {
    path: '**',
    redirectTo: 'login'
  }
];
