import { Component, inject, signal, ViewEncapsulation } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { EscolaService } from '../../../core/services/escola.service';
import { Escola } from '../../../core/models';

interface DialogData {
  escola?: Escola;
}

@Component({
  selector: 'app-escola-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './escola-dialog.component.html',
  styleUrl: './escola-dialog.component.scss',
  encapsulation: ViewEncapsulation.None
})
export class EscolaDialogComponent {
  private fb = inject(FormBuilder);
  private dialogRef = inject(MatDialogRef<EscolaDialogComponent>);
  private data = inject<DialogData>(MAT_DIALOG_DATA);
  private escolaService = inject(EscolaService);
  private snackBar = inject(MatSnackBar);

  isLoading = signal(false);
  isEditMode = !!this.data.escola;

  form: FormGroup = this.fb.group({
    sDescricao: [this.data.escola?.sDescricao || '', [Validators.required, Validators.minLength(3)]]
  });

  onSubmit(): void {
    if (this.form.invalid) return;

    this.isLoading.set(true);
    const formData = this.form.value;

    const request = this.isEditMode
      ? this.escolaService.update(this.data.escola!.iCodEscola, formData)
      : this.escolaService.create(formData);

    request.subscribe({
      next: () => {
        this.snackBar.open(
          this.isEditMode ? 'Escola atualizada com sucesso' : 'Escola criada com sucesso',
          'Fechar',
          { duration: 3000 }
        );
        this.dialogRef.close(true);
      },
      error: () => {
        this.isLoading.set(false);
        this.snackBar.open('Erro ao salvar escola', 'Fechar', { duration: 3000 });
      }
    });
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}

