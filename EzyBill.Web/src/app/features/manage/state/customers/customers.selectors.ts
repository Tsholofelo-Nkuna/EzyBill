import { createFeatureSelector, createSelector } from "@ngrx/store";

import { ManageFeatureState, managefeatureKey } from "../manage-feature.state";


const manageFeatureSelector =  createFeatureSelector<ManageFeatureState>(managefeatureKey);
export const customerPageBusyIndicatorSelector = createSelector(manageFeatureSelector, state =>{
    return {
      customerFetchDone: state.customersViewState.customerFetchDone,
      customerUpdateDone:state.customersViewState.customerUpdateDone,
      saveNewCustomerDone:state.customersViewState.saveNewCustomerDone
    };
});
export const selectCustomers = createSelector(manageFeatureSelector, state => {
  return state.customersViewState.customers
 });

 export const selectTotalRecordCount = createSelector(manageFeatureSelector, state => state.customersViewState.totalRecordCount);
