import { Component, inject, signal, OnInit, ViewEncapsulation, PLATFORM_ID } from '@angular/core';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCardModule } from '@angular/material/card';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatTooltipModule } from '@angular/material/tooltip';
import { EscolaService } from '../../core/services/escola.service';
import { Escola } from '../../core/models';
import { EscolaDialogComponent } from './escola-dialog/escola-dialog.component';
import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-escolas',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatDialogModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatTooltipModule
  ],
  templateUrl: './escolas.component.html',
  styleUrl: './escolas.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class EscolasComponent implements OnInit {
  private escolaService = inject(EscolaService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  private platformId = inject(PLATFORM_ID);

  escolas = signal<Escola[]>([]);
  filteredEscolas = signal<Escola[]>([]);
  isLoading = signal(true);
  searchTerm = '';

  displayedColumns = ['iCodEscola', 'sDescricao', 'actions'];

  ngOnInit(): void {
    // Só carregar dados no browser (não no SSR)
    if (isPlatformBrowser(this.platformId)) {
      this.loadEscolas();
    } else {
      this.isLoading.set(false);
    }
  }

  loadEscolas(): void {
    this.isLoading.set(true);
    this.escolaService.getAll().subscribe({
      next: (escolas) => {
        this.escolas.set(escolas);
        this.applyFilter();
        this.isLoading.set(false);
      },
      error: () => {
        this.snackBar.open('Erro ao carregar escolas', 'Fechar', { duration: 3000 });
        this.isLoading.set(false);
      }
    });
  }

  applyFilter(): void {
    const term = this.searchTerm.toLowerCase();
    const filtered = this.escolas().filter(e =>
      e.sDescricao.toLowerCase().includes(term)
    );
    this.filteredEscolas.set(filtered);
  }

  openDialog(escola?: Escola): void {
    const dialogRef = this.dialog.open(EscolaDialogComponent, {
      width: '400px',
      data: { escola }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) this.loadEscolas();
    });
  }

  confirmDelete(escola: Escola): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: { title: 'Excluir Escola', message: `Deseja excluir a escola "${escola.sDescricao}"?` }
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.escolaService.delete(escola.iCodEscola).subscribe({
          next: () => {
            this.snackBar.open('Escola excluída com sucesso', 'Fechar', { duration: 3000 });
            this.loadEscolas();
          },
          error: () => this.snackBar.open('Erro ao excluir escola', 'Fechar', { duration: 3000 })
        });
      }
    });
  }
}

