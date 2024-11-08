import { createAction, props } from "@ngrx/store";
import { ICustomerDto } from "../../../../shared/interfaces/customer-dto";
import { IPagingPageQueryDto } from "../../../../shared/interfaces/paging-page-query.dto";
import { IPageResponseDto } from "../../../../shared/interfaces/page-response.dto";
import { IResponseDto } from "../../../../shared/interfaces/response.dto";

export const fetchCustomers = createAction("[CustomersCompontent] Fetch Customers", props<{pageQuery: IPagingPageQueryDto<ICustomerDto>}>());
export const fecthCustomersComplete = createAction("[CustomersComponent] Fetch Customers Complete", props<{response: IPageResponseDto<ICustomerDto>}>());
export const updateCustomer = createAction("[CustomerComponent] Update Customer", props<{customer:ICustomerDto}>());
export const updateCustomerComplete = createAction("[CustomerComponent] Update Customer Complete", props<{response: IResponseDto<boolean>}>());
export const addNewCustomer = createAction("[CustomerComponent] Add New Customer", props<{customer: ICustomerDto}>());
export const addNewCustomerComplete = createAction("[CustomerComponent] Add New Customer Complete", props<{response: IResponseDto<boolean>}>());
export const deleteCustomer = createAction("[CustomerComponent] Delete Customer", props<{customerIdentifiers: Array<string> }>());
export const deleteCustomerComplete = createAction("[CustomerComponent] Delete Customer Complete", props<{response: IResponseDto<boolean>}>());
