import { createReducer, on } from "@ngrx/store";
import { ICustomerDto } from "../../../../shared/interfaces/customer-dto";
import { fecthCustomersComplete } from "./customers.actions";

export class CustomersViewState{
  customers: ICustomerDto[] = [];
}

let initState = new CustomersViewState();
export const customerReducer = createReducer(initState,
  on(fecthCustomersComplete, (state, {response}) =>  {
  return  ({...state, customers: [...response.data]})
    })
);
