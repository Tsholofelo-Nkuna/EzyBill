import { IProductLineItemDto } from "./product-line-item-dto";

export enum PaymentStatus
    {
        Unpaid = 0,
        Paid = 1
    }
export interface IInvoiceDto {
        id: string;
        customerName: string;
        billingAddressLine1: string;
        billingAddressLine2: string;
        InvoicedProducts: Array<IProductLineItemDto>;
        PaymentStatus: PaymentStatus;
        amountDue: number;
}
