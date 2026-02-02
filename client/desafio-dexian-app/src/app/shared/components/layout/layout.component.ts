import { Component, inject, signal, ViewEncapsulation, HostListener, OnInit, PLATFORM_ID, AfterViewInit } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatMenuModule } from '@angular/material/menu';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    MatButtonModule,
    MatDividerModule,
    MatTooltipModule,
    MatMenuModule
  ],
  templateUrl: './layout.component.html',
  styleUrl: './layout.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class LayoutComponent implements OnInit, AfterViewInit {
  private authService = inject(AuthService);
  private router = inject(Router);
  private platformId = inject(PLATFORM_ID);

  isSidenavOpen = signal(true);
  isMobile = signal(false);
  isReady = signal(false); // Controla quando o componente está pronto após hidratação
  userName = this.authService.getUserName();

  private readonly MOBILE_BREAKPOINT = 768;

  ngOnInit(): void {
    // Verificar autenticação no cliente após hidratação do SSR
    if (isPlatformBrowser(this.platformId)) {
      if (!this.authService.isAuthenticated()) {
        this.router.navigate(['/login']);
        return;
      }
      this.checkScreenSize();
    }
  }

  ngAfterViewInit(): void {
    // Marcar como pronto após a view ser inicializada (hidratação completa)
    if (isPlatformBrowser(this.platformId)) {
      // Usar setTimeout para evitar ExpressionChangedAfterItHasBeenCheckedError
      setTimeout(() => {
        this.isReady.set(true);
      }, 0);
    }
  }

  @HostListener('window:resize')
  onResize(): void {
    this.checkScreenSize();
  }

  private checkScreenSize(): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const isMobileNow = window.innerWidth <= this.MOBILE_BREAKPOINT;
    this.isMobile.set(isMobileNow);

    // Em mobile, sidenav fica fechado por padrão
    if (isMobileNow) {
      this.isSidenavOpen.set(false);
    }
  }

  toggleSidenav(): void {
    this.isSidenavOpen.update(v => !v);
  }

  logout(): void {
    this.authService.logout();
  }
}

