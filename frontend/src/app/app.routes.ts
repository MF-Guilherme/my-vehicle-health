import { Routes } from '@angular/router';
import { VehiclesComponent } from './vehicles/vehicles.component';
import { MaintenanceComponent } from './maintenance/maintenance.component';

export const routes: Routes = [
  { path: '', redirectTo: '/vehicles', pathMatch: 'full' },
  { path: 'vehicles', component: VehiclesComponent },
  { path: 'maintenance', component: MaintenanceComponent }
];