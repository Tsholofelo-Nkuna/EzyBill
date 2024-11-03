import { IProductDto } from "./product-dto";

export interface IProductLineItemDto extends IProductDto {
  qty : number;
}
