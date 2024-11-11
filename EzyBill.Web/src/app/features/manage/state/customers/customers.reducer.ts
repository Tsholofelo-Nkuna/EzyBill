import { createReducer, on } from "@ngrx/store";
import { ICustomerDto } from "../../../../shared/interfaces/customer-dto";
import { addNewCustomer, addNewCustomerComplete, fecthCustomersComplete, fetchCustomers, updateCustomer, updateCustomerComplete } from "./customers.actions";




export type CustomersViewBusyIndicator = {
    customerFetchDone?: boolean,
    customerUpdateDone?: boolean,
    saveNewCustomerDone?: boolean
};

export type CustomersViewState = {customers: ICustomerDto[], totalRecordCount?:number} & CustomersViewBusyIndicator;

let initState: CustomersViewState = {customers: [], saveNewCustomerDone:true, totalRecordCount:0};
export const customerReducer = createReducer(initState,
  on(fecthCustomersComplete, (state, {response}) =>  {
    return  ({...state, customers: [...response.data], customerFetchDone:true, totalRecordCount: response.totalRecordCount})
  }),
  on(fetchCustomers, (state) => ({...state, customerFetchDone: false})),
  on(updateCustomer, state => ({...state, customerUpdateDone: false})),
  on(updateCustomerComplete, state => ({...state, customerUpdateDone: true})),
  on(addNewCustomer, state => ({...state, saveNewCustomerDone: false})),
  on(addNewCustomerComplete, state =>{
   return ({...state, saveNewCustomerDone: true});
   }
  )
);
