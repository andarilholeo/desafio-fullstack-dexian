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
import { AlunoService } from '../../core/services/aluno.service';
import { EscolaService } from '../../core/services/escola.service';
import { Aluno, Escola } from '../../core/models';
import { AlunoDialogComponent } from './aluno-dialog/aluno-dialog.component';
import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-alunos',
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
  templateUrl: './alunos.component.html',
  styleUrl: './alunos.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class AlunosComponent implements OnInit {
  private alunoService = inject(AlunoService);
  private escolaService = inject(EscolaService);
  private dialog = inject(MatDialog);
  private snackBar = inject(MatSnackBar);
  private platformId = inject(PLATFORM_ID);

  alunos = signal<Aluno[]>([]);
  filteredAlunos = signal<Aluno[]>([]);
  escolas = signal<Escola[]>([]);
  isLoading = signal(true);
  searchTerm = '';

  displayedColumns = ['iCodAluno', 'sNome', 'sCPF', 'dNascimento', 'sCelular', 'escola', 'actions'];

  ngOnInit(): void {
    // Só carregar dados no browser (não no SSR)
    if (isPlatformBrowser(this.platformId)) {
      this.loadData();
    } else {
      this.isLoading.set(false);
    }
  }

  loadData(): void {
    this.isLoading.set(true);
    this.escolaService.getAll().subscribe({
      next: (escolas) => {
        this.escolas.set(escolas);
        this.loadAlunos();
      },
      error: () => {
        this.snackBar.open('Erro ao carregar escolas', 'Fechar', { duration: 3000 });
        this.isLoading.set(false);
      }
    });
  }

  loadAlunos(): void {
    this.alunoService.getAll().subscribe({
      next: (alunos) => {
        this.alunos.set(alunos);
        this.applyFilter();
        this.isLoading.set(false);
      },
      error: () => {
        this.snackBar.open('Erro ao carregar alunos', 'Fechar', { duration: 3000 });
        this.isLoading.set(false);
      }
    });
  }

  applyFilter(): void {
    const term = this.searchTerm.toLowerCase().replace(/[.\-]/g, '');
    const filtered = this.alunos().filter(a =>
      (a.sNome?.toLowerCase() || '').includes(term) ||
      (a.sCPF?.replace(/[.\-]/g, '') || '').includes(term)
    );
    this.filteredAlunos.set(filtered);
  }

  getEscolaName(escolaId: number): string {
    return this.escolas().find(e => e.iCodEscola === escolaId)?.sDescricao || '-';
  }

  formatCPF(cpf: string | null | undefined): string {
    if (!cpf) return '-';
    const cleaned = cpf.replace(/\D/g, '');
    return cleaned.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
  }

  formatPhone(phone: string | null | undefined): string {
    if (!phone) return '-';
    const cleaned = phone.replace(/\D/g, '');
    return cleaned.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
  }

  formatDate(date: Date | string): string {
    const d = new Date(date);
    return d.toLocaleDateString('pt-BR');
  }

  openDialog(aluno?: Aluno): void {
    const dialogRef = this.dialog.open(AlunoDialogComponent, {
      width: '500px',
      data: { aluno, escolas: this.escolas() }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) this.loadAlunos();
    });
  }

  confirmDelete(aluno: Aluno): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '350px',
      data: { title: 'Excluir Aluno', message: `Deseja excluir o aluno "${aluno.sNome}"?` }
    });

    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.alunoService.delete(aluno.iCodAluno).subscribe({
          next: () => {
            this.snackBar.open('Aluno excluído com sucesso', 'Fechar', { duration: 3000 });
            this.loadAlunos();
          },
          error: () => this.snackBar.open('Erro ao excluir aluno', 'Fechar', { duration: 3000 })
        });
      }
    });
  }
}

