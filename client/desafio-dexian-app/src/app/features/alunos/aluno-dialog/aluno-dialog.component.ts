import { Component, inject, signal, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { NgxMaskDirective } from 'ngx-mask';
import { AlunoService } from '../../../core/services/aluno.service';
import { Aluno, Escola } from '../../../core/models';

interface DialogData {
  aluno?: Aluno;
  escolas: Escola[];
}

@Component({
  selector: 'app-aluno-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    NgxMaskDirective
  ],
  templateUrl: './aluno-dialog.component.html',
  styleUrl: './aluno-dialog.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class AlunoDialogComponent {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<AlunoDialogComponent>);
  private data = inject<DialogData>(MAT_DIALOG_DATA);
  private alunoService = inject(AlunoService);
  private snackBar = inject(MatSnackBar);

  isLoading = signal(false);
  isEditMode = !!this.data.aluno;
  escolas = this.data.escolas;

  form: FormGroup = this.fb.group({
    sNome: [this.data.aluno?.sNome || '', [Validators.required, Validators.minLength(3)]],
    sCPF: [this.data.aluno?.sCPF || '', [Validators.required, Validators.minLength(11)]],
    dNascimento: [this.data.aluno?.dNascimento ? new Date(this.data.aluno.dNascimento) : null, Validators.required],
    sEndereco: [this.data.aluno?.sEndereco || '', Validators.required],
    sCelular: [this.data.aluno?.sCelular || '', [Validators.required, Validators.minLength(11)]],
    iCodEscola: [this.data.aluno?.iCodEscola || null, Validators.required]
  });

  onSubmit(): void {
    if (this.form.invalid) return;

    this.isLoading.set(true);
    const formData = this.form.value;

    const request = this.isEditMode
      ? this.alunoService.update(this.data.aluno!.iCodAluno, formData)
      : this.alunoService.create(formData);

    request.subscribe({
      next: () => {
        this.snackBar.open(
          this.isEditMode ? 'Aluno atualizado com sucesso' : 'Aluno criado com sucesso',
          'Fechar',
          { duration: 3000 }
        );
        this.dialogRef.close(true);
      },
      error: () => {
        this.isLoading.set(false);
        this.snackBar.open('Erro ao salvar aluno', 'Fechar', { duration: 3000 });
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}

