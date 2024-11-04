import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ApiService } from './api.service';
import { ICustomerDto } from '../interfaces/customer-dto';
import { Observable, of } from 'rxjs';
import { IResponseDto } from '../interfaces/response.dto';
import { IPagingPageQueryDto } from '../interfaces/paging-page-query.dto';
import { IPageResponseDto } from '../interfaces/page-response.dto';

@Injectable({
  providedIn: 'root'
})
export class CustomersService extends ApiService{

  constructor(){
    super('api/customer'); 
  }

  getCustomers(pageQuery: IPagingPageQueryDto<ICustomerDto>): Observable<IPageResponseDto<ICustomerDto>>{
    return this.httpClient.post<IPageResponseDto<ICustomerDto>>(`${this.url}/GetCustomers`, pageQuery);
  }

  saveNewCustomer(customer: ICustomerDto): Observable<IResponseDto<boolean>>{
    return this.httpClient.post<IResponseDto<boolean>>(`${this.url}/Add`, [customer]);
  }

  editCustomer(customer: ICustomerDto): Observable<IResponseDto<boolean>>{
     return this.httpClient.post<IResponseDto<boolean>>(`${this.url}/Update`, [customer]);
  }

  deleteCustomer(id: string): Observable<IResponseDto<boolean>>{
     return this.httpClient.post<IResponseDto<boolean>>(`${this.url}/Delete`, [id]);
  }
}
