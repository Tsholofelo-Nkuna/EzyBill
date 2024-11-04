import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomersComponent } from './components/customers/customers.component';
import { InvoicesComponent } from './components/invoices/invoices.component';
import { PaymentsComponent } from './components/payments/payments.component';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../../shared/shared.module';
import { EffectsModule } from '@ngrx/effects';
import { CustomersEffect } from './state/customers/customers.effect';
import { StoreModule } from '@ngrx/store';
import { customerReducer } from './state/customers/customers.reducer';
import { managefeatureKey } from './state/manage-feature.state';

let routes: Routes = [
  {
    path: "customers",
    component: CustomersComponent,
    title: 'Customers'
  },
  {
     path: "invoices",
     component: InvoicesComponent,
     title: 'Invoices'
  },
   {
     path: "payments",
     component: PaymentsComponent,
     title: 'Payments'
  }
]

@NgModule({
  declarations: [
    CustomersComponent,
    InvoicesComponent,
    PaymentsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    SharedModule,
    EffectsModule.forFeature([CustomersEffect]),
    StoreModule.forFeature(managefeatureKey, {customersViewState:customerReducer})
  ],
})
export class ManageModule { }
