import { Injectable, inject } from "@angular/core";
import { CustomersService } from "../../../../shared/services/customers.service";
import { Actions, createEffect, ofType } from "@ngrx/effects";
import { addNewCustomer, addNewCustomerComplete, fecthCustomersComplete, fetchCustomers, updateCustomer, updateCustomerComplete } from "./customers.actions";
import { map, switchMap } from "rxjs";

@Injectable()
export class CustomersEffect{

  private $actions = inject(Actions);
  private _customerService = inject(CustomersService);
 

  $fetchCustomersEffect = createEffect(() => this.$actions.pipe(
    ofType(fetchCustomers),
    switchMap(action => {
      return this._customerService.getCustomers(action.pageQuery)
      .pipe(map(response => fecthCustomersComplete({response})));
    })
  ));

  $addNewCustomerEffect = createEffect(() => this.$actions.pipe(
    ofType(addNewCustomer),
    switchMap(action => {
      return this._customerService
      .saveNewCustomer(action.customer)
      .pipe(map(response => addNewCustomerComplete({response})));
    })
  ));

  $updateCustomerEffect = createEffect(() => this.$actions.pipe(
    ofType(updateCustomer),
    switchMap(action => {
      return this._customerService.editCustomer(action.customer)
      .pipe(map(response => updateCustomerComplete({response})))
    })
  ));
}
