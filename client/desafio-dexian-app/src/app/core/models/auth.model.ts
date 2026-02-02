export interface LoginRequest {
  sNome: string;
  sSenha: string;
}

export interface LoginResponse {
  token: string;
  nome: string;
  expiration: Date;
}

