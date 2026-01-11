import { ApiError } from '../models/api-error.model';
import { AppError } from '../models/app-error.model';

export class ApiErrorMapper {

  static map(error: any): AppError {

    const apiError: ApiError | undefined = error?.response?.data;

    if (!apiError) {
      return {
        message: 'Unexpected error occurred',
        status: 0,
        type: 'UNKNOWN'
      };
    }
    
    if (apiError.status >= 400 && apiError.status < 500) {
      return {
        message: apiError.detail || 'Invalid request',
        status: apiError.status,
        type: 'CLIENT'
      };
    }

    if (apiError.status >= 500) {
      return {
        message: 'Service temporarily unavailable',
        status: apiError.status,
        type: 'SERVER'
      };
    }

    return {
      message: 'Unexpected error occurred',
      status: apiError.status,
      type: 'UNKNOWN'
    };
  }
}
