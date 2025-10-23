import { HttpInterceptorFn } from '@angular/common/http';
import { ToastService } from '../services/toast-service';
import { Router } from '@angular/router';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const toast = inject(ToastService);
  const router = inject(Router);

  return next(req).pipe(
    catchError(err => {
      if (err) {
        switch (err.status) {
          case 400:
            if (err.error.errors) {
              const modelStateErrors = [];
              for (const key in err.error.errors) {
                if (err.error.errors[key]) {
                  modelStateErrors.push(err.error.errors[key])
                }
              }
              throw modelStateErrors.flat();
            } else {
              toast.error(err.error, err.status)
            }
            break;
          case 401:
            toast.error('Unauthorized');
            break;
          case 404:
            router.navigateByUrl('/not-found');
            break;
          case 500:
            const navigationExtras = { state: { error: err.error } };
            router.navigateByUrl('/server-error', navigationExtras);
            break;
          default:
            toast.error('Something went wrong');
            break;
        }
      }
      return throwError(() => err);
    })
  );
};
