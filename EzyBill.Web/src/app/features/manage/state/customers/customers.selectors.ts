import { createFeatureSelector, createSelector } from "@ngrx/store";

import { ManageFeatureState, managefeatureKey } from "../manage-feature.state";


const manageFeatureSelector =  createFeatureSelector<ManageFeatureState>(managefeatureKey);
export const selectCustomers = createSelector(manageFeatureSelector, state => {
  return state.customersViewState.customers
 });

