import { ApplicationConfig } from '@angular/core';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { providePrimeNG } from 'primeng/config';
import nora from '@primeng/themes/nora';

export const appConfig: ApplicationConfig = {
    providers: [
        provideAnimationsAsync(),
        providePrimeNG({
            theme: {
                preset: nora,
                options: {
                  darkModeSelector: '.my-app-dark'
              }
            }
        })
    ]
};