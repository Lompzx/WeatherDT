export interface AppError {
  message: string;
  status: number;
  type: 'CLIENT' | 'SERVER' | 'UNKNOWN';
}
