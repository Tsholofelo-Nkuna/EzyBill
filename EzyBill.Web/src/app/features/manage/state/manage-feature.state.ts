import { CustomersViewState } from "./customers/customers.reducer";

export const managefeatureKey = "Manage";
export class ManageFeatureState{
   customersViewState: CustomersViewState = {customers: [], saveNewCustomerDone: true} 
}
