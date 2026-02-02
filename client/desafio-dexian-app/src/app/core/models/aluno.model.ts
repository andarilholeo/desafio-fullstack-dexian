export interface Aluno {
  iCodAluno: number;
  sNome: string;
  dNascimento: Date;
  sCPF: string;
  sEndereco: string;
  sCelular: string;
  iCodEscola: number;
}

export interface CreateAluno {
  sNome: string;
  dNascimento: Date;
  sCPF: string;
  sEndereco: string;
  sCelular: string;
  iCodEscola: number;
}

export interface UpdateAluno {
  sNome: string;
  dNascimento: Date;
  sCPF: string;
  sEndereco: string;
  sCelular: string;
  iCodEscola: number;
}

