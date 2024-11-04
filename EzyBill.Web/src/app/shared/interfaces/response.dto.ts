export interface IResponseDto<TData>
{
  errors: Array<string>;
  data: TData;
  message: string;
}
