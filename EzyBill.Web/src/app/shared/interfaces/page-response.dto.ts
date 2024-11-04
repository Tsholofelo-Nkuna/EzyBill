 //public class PageReponseDto<TCollection>: ResponseDto<TCollection> where TCollection : IEnumerable<object>
 //   {
 //       public int TotalRecordCount { get; set; }

import { IResponseDto } from "./response.dto";

 //   }
export interface IPageResponseDto<TData> extends IResponseDto<Array<TData>>{
   totalRecordCount: number;
}
